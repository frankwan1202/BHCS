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
    public class CourseCommandServicer:ICommandServicer
    {
        private readonly ICourseNatureRepository _courseNatureRepository;
        private readonly ICourseRepository _courseRepository;

        public CourseCommandServicer(ICourseNatureRepository courseNatureRepository,ICourseRepository courseRepository)
        {
            _courseNatureRepository = courseNatureRepository;
            _courseRepository = courseRepository;
        }

        public ICommandResult CreateNewCourseNature(CreateNewCourseNatureCommand command)
        {
            var nature = new CourseNature(command.NatureName);
            Ensure.MustBeFalse(_courseNatureRepository.IsExist(command.NatureName), "该课程性质名称已存在！");
            _courseNatureRepository.Insert(nature);
            return new CommandResult(Infrastructure.Fast.Infrastructure.ResultCode.Ok, "操作成功！");
        }

        public ICommandResult CreateNewCourse(CreateNewCourseCommand command)
        {
            var course = new Course(command.Name, command.NatureId, command.Hours, command.MajorId);
            Ensure.MustBeFalse(_courseRepository.IsExist(course.Name, course.MajorId), "该专业已存在该课程！");
            _courseRepository.Insert(course);
            return new CommandResult(Infrastructure.Fast.Infrastructure.ResultCode.Ok, "操作成功！");
        }
    }
}
