using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DbContexts;
using Core.Models;
using Core.Repositories.DbRepos.Base;
using Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Repositories.DbRepos {
	public class UserDbRepo : BaseDbRepo<User>, IUserRepo {
		public UserDbRepo(MainDbContext dbContext) : base(dbContext) {

		}

		public async Task<bool> LoginExistAsync(User user) {
			var isExist = await _context.Users.AnyAsync(u => u.Login == user.Login && u.Id != user.Id);
			return isExist;

        }
	}
}
