using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Core.Handlers.Interfaces;
using Core.Models;
using Core.Models.Dto;
using WpfGui.MvvmTools;
using WpfGui.WindowBoxes;

namespace WpfGui
{
    public class MainViewModel     
    {
        private readonly IUserHandler _userHandler;
        public MainViewModel(IUserHandler userHandler) {
           _userHandler = userHandler;
           CreateUser = new RelayCommand<object>(CanCreateUser, ExecuteCreateUserAsync);
           RemoveUser = new RelayCommand<UserDto>(CanRemoveUser, ExecuteRemoveUserAsync);           
           UpdateUser = new RelayCommand<UserDto>(CanUpdateUser, ExecuteUpdateUserAsync);
        }

        public async Task LoadDataAsync() {
            Users.Clear();
            var collectionUsers = await _userHandler.GetAllUsersAsync();
            foreach (var user in collectionUsers) {
                Users.Add(user);
            }
            
        }
        public ObservableCollection<UserDto> Users { get; set; } = new();

        public ICommand CreateUser{ get; set; }
        public ICommand UpdateUser { get; set; }
        public ICommand RemoveUser { get; set; }

        private bool CanCreateUser() {
            return true;
        }
        private async Task ExecuteCreateUserAsync(object _) {
            var newUserDto = new NewUserDto();
            var createWindow = new UpdateWindow(newUserDto);
            var needOpenWindow = true;
            while (needOpenWindow) {
                createWindow.ShowDialog();
                if (createWindow.IsProcessing) {
                    try {
                        await _userHandler.AddUserAsync(newUserDto);
                        createWindow.ForceClose();
                        await LoadDataAsync();
                    }
                    catch (Exception ex) {
                        createWindow.AddError(ex.Message);
                    }
                }
                else {
                    createWindow.ForceClose();
                }

                needOpenWindow = createWindow.IsProcessing;
                createWindow.IsProcessing = false;
            }

        }

        private bool CanUpdateUser() {
            return true;
        }
        private async Task ExecuteRemoveUserAsync(UserDto user) {
            var confirmWindow = new ConfirmDeleteWindow();
            var needOpenWindow = true;
            while (needOpenWindow) {
                confirmWindow.ShowDialog();
                if (confirmWindow.IsProcessing) {
                    try {
                        await _userHandler.DeleteUserAsync(user.Id);
                        await LoadDataAsync();
                        confirmWindow.ForceClose();
                    }
                    catch (Exception ex) {
                        confirmWindow.AddError(ex.Message);
                    }
                }
                else {
                    confirmWindow.ForceClose();
                }

                needOpenWindow = confirmWindow.IsProcessing;
                confirmWindow.IsProcessing = false;
            }
        }

        private bool CanRemoveUser() {
            return true;
        }
        private async Task ExecuteUpdateUserAsync(UserDto user) {
            var newUserDto = new NewUserDto() {
                Login = user.Login,
                LastName = user.LastName,
                FirstName = user.FirstName,
            };
            var updateWindow = new UpdateWindow(newUserDto, user.Id);
            var needOpenWindow = true;
            while (needOpenWindow) {
                updateWindow.ShowDialog();
                if (updateWindow.IsProcessing) {
                    try {
                        await _userHandler.UpdateUserAsync(user.Id, newUserDto);
                        await LoadDataAsync();
                        updateWindow.ForceClose();
                    }
                    catch (Exception ex) {
                        updateWindow.AddError(ex.Message);
                    }
                }
                else {
                    updateWindow.ForceClose();
                }

                needOpenWindow = updateWindow.IsProcessing;
                updateWindow.IsProcessing = false;
            }
        }
    }
}
