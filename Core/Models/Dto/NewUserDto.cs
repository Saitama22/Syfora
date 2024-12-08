using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dto {
	/// <summary>
	/// Пользователь, полученный при редактировании/удалении
	/// </summary>
	public class NewUserDto {
		public string Login { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
