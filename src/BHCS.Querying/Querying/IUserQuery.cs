using BHCS.Infrastructure.Middlewares;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Querying.Querying
{
    public interface IUserQuery
    {
        SignMember GetLoginUserInfo(Guid userId);
    }
}
