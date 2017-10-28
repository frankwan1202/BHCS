using BHCS.Domain.Models.Users;
using BHCS.Domain.Repositories;
using BHCS.Infrastructure.Fast.ThirdParty.FastDbCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Repositories.DbCommon
{
    public class UserRepository : FastDbCommonRepository<User>, IUserRepository
    {
        public bool IsExistAccount(string account)
        {
            if (Get(p => p.Account == account) != null) return true;
            else return false;
        }
    }
}
