using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Core.Repositories.DbRepos.Base;
using Core.Repositories.InMemoryRepos.Base;
using Core.Repositories.Interfaces;

namespace Core.Repositories.InMemoryRepos {
    internal class UserInMemoryRepo : BaseInMemoryRepo<User>, IUserRepo {
        public async Task<bool> LoginExistAsync(User user) {
            var records = await GetRecordsAsync();
            var isExist = records.Any(r => r.Login == user.Login && r.Id != user.Id);
            return isExist;
        }
    }
}
