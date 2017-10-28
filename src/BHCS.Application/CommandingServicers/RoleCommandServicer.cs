using BHCS.Application.Commanding;
using BHCS.Domain.Models;
using BHCS.Domain.Repositories;
using BHCS.Infrastructure.Fast.Commanding;
using BHCS.Infrastructure.Fast.Infrastructure;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Application.CommandingServicers
{
    public class RoleCommandServicer:ICommandServicer
    {
        private readonly IRoleRepository _roleRepository;

        public RoleCommandServicer(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public ICommandResult CreateNewRole(CreateNewRoleCommand command)
        {
            Ensure.NotNull(command, "消息为空！");
            Ensure.NotNullOrWhiteSpace(command.RoleName, "角色名不能为空！");
            var role = new Role(command.RoleName);
            _roleRepository.Insert(role);
            return new CommandResult(ResultCode.Ok, "操作成功！");
        }
    }
}
