using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Core.Repositories.DbRepos.Base;
using Core.Repositories.Interfaces;
using Core.Repositories.XmlRepos.Base;

namespace Core.Repositories.XmlRepos {
    public class UserXmlRepo : BaseXmlRepo<User>, IUserRepo {
        public UserXmlRepo(string storageDirectory) : base(storageDirectory, nameof(User)) {
        }

        public async Task<bool> LoginExistAsync(User user) {
            var records = await GetRecordsAsync();
            var isExist = records.Any(r => r.Login == user.Login && r.Id != user.Id);
            return isExist;
        }
    }
}
