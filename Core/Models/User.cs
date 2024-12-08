using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Interfaces;

namespace Core.Models {
	/// <summary>
	/// Сущность пользователя
	/// </summary>
	public class User : IGuidModel {
		public Guid Id { get; set; }
		public string Login { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
