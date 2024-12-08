using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Handlers.Interfaces;
using Core.Models;
using Core.Models.Dto;
using Core.Repositories.Interfaces;

namespace Core.Handlers {
	public class UserHandler : IUserHandler {
		private readonly IUserRepo _userRepo; 
		private readonly IMapper _mapper;

		public UserHandler(IUserRepo userRepo, IMapper mapper) {
			_userRepo = userRepo;
			_mapper = mapper;
		}

		public async Task AddUserAsync(NewUserDto userDto) {
			var user = GetModelUser(userDto);
			await ValidateUserAsync(user);
			await _userRepo.AddRecordAsync(user);
		}

		public async Task<UserDto> UpdateUserAsync(Guid userId, NewUserDto userDto) {
			var user = await _userRepo.GetRecordAsync(userId);
			if (user == null)
                throw new ValidationException("Пользователь не найден");
            
			user.LastName = userDto.LastName;
			user.FirstName = userDto.FirstName;
			user.Login = userDto.Login;

            await ValidateUserAsync(user);
			var updUser = await _userRepo.UpdateRecordAsync(user);
			var updUserDto = GetDtoUser(updUser);
			return updUserDto;
		}

		private User GetModelUser(UserDto userDto) {
			return _mapper.Map<User>(userDto);
		}
		private User GetModelUser(NewUserDto userDto) {
			return _mapper.Map<User>(userDto);
		}
		private UserDto GetDtoUser(User user) {
			return _mapper.Map<UserDto>(user);
		}

		private async Task ValidateUserAsync(User user) {
			if (user == null) 
				throw new NullReferenceException();
			if (string.IsNullOrEmpty(user.Login))
				throw new ValidationException("Логин пользователя не может быть пустым");
			if (await _userRepo.LoginExistAsync(user) == true)
				throw new ValidationException("Логин пользователя должен быть уникальным");
		}

		public async Task DeleteUserAsync(Guid id) {
			var user = await _userRepo.GetRecordAsync(id);
			if (user is null)
				throw new ValidationException("Пользователь не найден");
			await _userRepo.DeleteRecordAsync(id);
		}

		public async Task<IEnumerable<UserDto>> GetAllUsersAsync() {			
			var users = await _userRepo.GetRecordsAsync(); 
			return _mapper.Map<IEnumerable<UserDto>>(users);
		}

		public async Task<UserDto> GetUserByIdAsync(Guid id) {
			var user = await _userRepo.GetRecordAsync(id);
			var userDto = GetDtoUser(user);
			return userDto;
		}
	}
}
