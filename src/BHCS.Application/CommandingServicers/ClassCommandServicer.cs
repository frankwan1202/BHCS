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
    public class ClassCommandServicer:ICommandServicer
    {
        private readonly IClassBuildingRepository _classBuildingRepository;
        private readonly IClassRoomRepository _classRoomRepository;

        public ClassCommandServicer(IClassBuildingRepository classBuildingRepository,IClassRoomRepository classRoomRepository)
        {
            _classBuildingRepository = classBuildingRepository;
            _classRoomRepository = classRoomRepository;
        }

        public ICommandResult CreateNewClassBuilding(CreateNewClassBuildingCommand command)
        {
            var classBuilding = new ClassBuilding(command.Name, command.Address);
            Ensure.MustBeFalse(_classBuildingRepository.IsExist(classBuilding.Name), "已存在该教学楼名称！");
            _classBuildingRepository.Insert(classBuilding);
            return new CommandResult(Infrastructure.Fast.Infrastructure.ResultCode.Ok, "操作成功！");
        }

        public ICommandResult CreateNewClassRoom(CreateNewClassRoomCommand command)
        {
            var classRoom = new ClassRoom(command.RoomNo, command.Name, command.ClassBuildingId);
            Ensure.MustBeFalse(_classRoomRepository.IsExist(classRoom.RoomNo), "已存在该课室编号！");
            _classRoomRepository.Insert(classRoom);
            return new CommandResult(Infrastructure.Fast.Infrastructure.ResultCode.Ok, "操作成功！");
        }
    }
}
