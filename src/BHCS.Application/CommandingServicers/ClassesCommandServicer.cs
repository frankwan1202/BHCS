using BHCS.Application.Commanding;
using BHCS.Domain.Models;
using BHCS.Domain.Repositories;
using BHCS.Infrastructure.Fast.Commanding;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Application.CommandingServicers
{
    public class ClassesCommandServicer:ICommandServicer
    {
        private readonly IClassesRepository _classesRepository;

        public ClassesCommandServicer(IClassesRepository classesRepository)
        {
            _classesRepository = classesRepository;
        }

        public ICommandResult CreateNewClasses(CreateNewClassesCommand command)
        {
            var classes = new Classes(command.ClassesNo, command.Name, command.GradeId);
            Ensure.MustBeFalse(_classesRepository.IsExist(classes.ClassesNo), "该班号已存在！");
            _classesRepository.Insert(classes);
            return new CommandResult(Infrastructure.Fast.Infrastructure.ResultCode.Ok, "操作成功！");
        }
    }
}
