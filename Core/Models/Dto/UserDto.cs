using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Models.Dto {
	public class UserDto {
		public Guid Id { get; set; }        
		public string Login { get; set; }   
		public string FirstName { get; set; }
		public string LastName { get; set; } 
	}
}
