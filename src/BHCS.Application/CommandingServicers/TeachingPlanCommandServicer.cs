using BHCS.Application.Commanding;
using BHCS.Domain.Models.TeachingPlans;
using BHCS.Domain.Repositories;
using BHCS.Infrastructure.Fast.Commanding;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Application.CommandingServicers
{
    public class TeachingPlanCommandServicer:ICommandServicer
    {
        private readonly ITeachingPlanRepository _teachingPlanRepository;
        private readonly ITeachingPlanCourseRepository _teachingPlanCourseRepository;

        public TeachingPlanCommandServicer(ITeachingPlanRepository teachingPlanRepository,
            ITeachingPlanCourseRepository teachingPlanCourseRepository)
        {
            _teachingPlanRepository = teachingPlanRepository;
            _teachingPlanCourseRepository = teachingPlanCourseRepository;
        }

        public ICommandResult CreateNewTeachingPlan(CreateNewTeachingPlanCommand command)
        {
            var teachingPlan = new TeachingPlan(command.Semester, command.Remark, command.MajorId, command.GradeId);
            Ensure.MustBeFalse(_teachingPlanRepository.IsExist(teachingPlan.MajorId, teachingPlan.GradeId), "该专业年级已制定教学计划！");
            _teachingPlanRepository.Insert(teachingPlan);
            Ensure.NotNull(command.CourseIds, "课程不能为空！");
            foreach(var courseId in command.CourseIds)
            {
                var teachingPlanCourse = new TeachingPlanCourse(teachingPlan.PlanId, courseId);
                _teachingPlanCourseRepository.Insert(teachingPlanCourse);
            }
            return new CommandResult(Infrastructure.Fast.Infrastructure.ResultCode.Ok, "操作成功！");
        }
    }
}
