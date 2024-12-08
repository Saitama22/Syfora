using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.Dto;

namespace Core.Handlers.Interfaces {
	/// <summary>
	/// Обработчик пользователя, содержит всю логику, которая должна быть при работе пользователем, кроме работы с поставщиками данных
	/// </summary>
	public interface IUserHandler {
		Task AddUserAsync(NewUserDto userDto);
		Task<UserDto> UpdateUserAsync(Guid userId, NewUserDto user);
		Task DeleteUserAsync(Guid id);
		Task<IEnumerable<UserDto>> GetAllUsersAsync();
		Task<UserDto> GetUserByIdAsync(Guid id);
	}
}
