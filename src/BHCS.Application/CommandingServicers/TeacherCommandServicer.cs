using BHCS.Application.Commanding;
using BHCS.Domain.Models.Users;
using BHCS.Domain.Repositories;
using BHCS.Infrastructure.Fast.Commanding;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Application.CommandingServicers
{
    public class TeacherCommandServicer:ICommandServicer
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUserRepository _userRepository;

        public TeacherCommandServicer(ITeacherRepository teacherRepository,IUserRepository userRepository)
        {
            _teacherRepository = teacherRepository;
            _userRepository = userRepository;
        }

        public ICommandResult CreateNewTeacher(CreateNewTeacherCommand command)
        {
            Ensure.MustBeEqual(command.Password, command.RepeatPassword, "两次输入密码不一致！");
            var user = new User(command.UserName,command.Account,command.Password,command.Mobile,command.Email,command.RoleId);
            var teacher = new Teacher(command.MajorId, user);
            Ensure.MustBeFalse(_userRepository.IsExistAccount(user.Account),"已存在该账号！");
            user.MD5Password();
            _userRepository.Insert(user);
            _teacherRepository.Insert(teacher);
            return new CommandResult(Infrastructure.Fast.Infrastructure.ResultCode.Ok, "操作成功！");
        }
    }
}
