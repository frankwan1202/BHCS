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
    public class GradeCommandServicer:ICommandServicer
    {
        private readonly IGradeRepository _gradeRepository;

        public GradeCommandServicer(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public ICommandResult CreateNewGrade(CreateNewGradeCommand command)
        {
            var grade = new Grade(command.Name);
            Ensure.MustBeFalse(_gradeRepository.IsExist(grade.Name), "该年级名称不能为空！");
            _gradeRepository.Insert(grade);
            return new CommandResult(Infrastructure.Fast.Infrastructure.ResultCode.Ok, "操作成功！");
        }
    }
}
