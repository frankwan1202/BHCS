using BHCS.Application.Commanding;
using BHCS.Domain.Models.Users;
using BHCS.Domain.Repositories;
using BHCS.Infrastructure.Fast.Commanding;
using BHCS.Infrastructure.Fast.Infrastructure;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Application.CommandingServicers
{
    public class StudentCommandServicer:ICommandServicer
    {
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;

        public StudentCommandServicer(IUserRepository userRepository,IStudentRepository studentRepository)
        {
            _userRepository = userRepository;
            _studentRepository = studentRepository;
        }

        public ICommandResult CreateNewStudent(CreateNewStudentCommand command)
        {
            Ensure.MustBeEqual(command.Password, command.RepeatPassword, "两次输入密码不一致！");
            var user = new User(command.UserName, command.Account, command.Password, command.Mobile, command.Email, Guid.Empty);
            var student = new Student(command.StudentNo, command.ClassesId, command.GradeId, command.MajorId, user.UserId);
            Ensure.MustBeFalse(_userRepository.IsExistAccount(user.Account), "已存在该账号！");
            Ensure.MustBeFalse(_studentRepository.IsExistStudent(student.StudentNo), "该学号已存在！");
            user.MD5Password();
            _userRepository.Insert(user);
            _studentRepository.Insert(student);
            return new CommandResult(ResultCode.Ok, "操作成功！");
        }
    }
}
