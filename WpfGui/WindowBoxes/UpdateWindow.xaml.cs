using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Core.Models;
using Core.Models.Dto;

namespace WpfGui.WindowBoxes
{
    /// <summary>
    /// Окно для сохранения/удаления пользователя
    /// </summary>
    public partial class UpdateWindow : Window
    {
        public bool IsProcessing { get; set; }
        public NewUserDto NewUserDto { get; set; }

        public UpdateWindow(NewUserDto newUserDto, Guid? id = null)
        {
            InitializeComponent();
            NewUserDto = newUserDto;
            if (id != null) {
                IdStackPanel.Visibility = Visibility.Visible;
                IdTextBox.Text = id.ToString();
            }
            LoginTextBox.Text = newUserDto.Login;
            FirstNameTextBox.Text = newUserDto.FirstName;
            LastNameTextBox.Text = newUserDto.LastName;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e) {            
            NewUserDto.Login = LoginTextBox.Text;
            NewUserDto.FirstName = FirstNameTextBox.Text;
            NewUserDto.LastName = LastNameTextBox.Text;
            IsProcessing = true;
            DialogResult = true;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
            ForceClose();
        }
        private void UpdateWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (IsProcessing) {
                e.Cancel = true; 
                Visibility = Visibility.Hidden;
            }
        }

        internal void AddError(string message) {
            ErrorTextBlock.Text = message;
            ErrorTextBlock.Visibility = Visibility.Visible;
            Visibility = Visibility.Visible;
        }

        internal void ForceClose() {
            IsProcessing = false;
            Close();
        }        
    }
}
