using BHCS.Application.Commanding;
using BHCS.Application.CommandingResults;
using BHCS.Domain.Repositories;
using BHCS.Infrastructure.Enums;
using BHCS.Infrastructure.Fast.Commanding;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Application.CommandingServicers
{
    public class AccountCommandServicer:ICommandServicer
    {
        private readonly IUserRepository _userRepository;

        public AccountCommandServicer(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ICommandResult Login(LoginCommand command)
        {
            var user = _userRepository.Get(p => (p.Account == command.Account || p.Mobile == command.Account || p.Email == command.Account) && p.Password == command.Password.ToMD5WithLower());
            Ensure.NotNull(user, "账户或密码输入错误！");
            Ensure.MustBeFalse(user.IsDelete, "账户已被删除！");
            Ensure.MustBeEqual((int)user.State, (int)AccountState.Enabled, "账户已被禁用！");
            return new LoginCommandResult(user.UserId);
        }
    }
}
