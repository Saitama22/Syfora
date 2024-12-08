using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Repositories.Interfaces {
	public interface IUserRepo : IRepo<User> {
		Task<bool> LoginExistAsync(User user);
	}
}
