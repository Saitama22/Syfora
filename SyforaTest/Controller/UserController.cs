using System.ComponentModel.DataAnnotations;
using Core.Handlers.Interfaces;
using Core.Models;
using Core.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace WebApp.Controller {
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBase {
		private readonly IUserHandler _userHandler;

		public UsersController(IUserHandler userService) {
			_userHandler = userService;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetAllUsers() {
			try {
                var users = await _userHandler.GetAllUsersAsync();
                if (!users.Any()) {
                    return NoContent();
                }
                return Ok(users);
            }
			catch (Exception ex) {
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetUserById(Guid id) {
			try {
                var user = await _userHandler.GetUserByIdAsync(id);
                if (user == null)
                    return NoContent();
				return Ok(user);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

		[HttpPost]
		public async Task<IActionResult> AddUser(NewUserDto user) {
			try {
                await _userHandler.AddUserAsync(user);
                return CreatedAtAction(nameof(AddUser), user);
            }
			catch (NullReferenceException ex) {
                return BadRequest(ex.Message);
            }
			catch (ValidationException ex) {
                return BadRequest(ex.Message);
            }
			catch (Exception ex) {
                return BadRequest(ex.Message);
            }
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser(Guid id, NewUserDto user) {
			try {
                var updUser = await _userHandler.UpdateUserAsync(id, user);
                return Ok(updUser);
            }
			catch (Exception ex) {
                return BadRequest(ex.Message);
            }
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(Guid id) {
			try {
				await _userHandler.DeleteUserAsync(id);
				return NoContent();
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
	}
}
