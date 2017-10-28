using BHCS.Domain.Models;
using BHCS.Domain.Repositories;
using BHCS.Infrastructure.Fast.ThirdParty.FastDbCommon;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Repositories.DbCommon
{
    public class ClassRoomRepository : FastDbCommonRepository<ClassRoom>, IClassRoomRepository
    {
        public bool IsExist(string roomNo)
        {
            Ensure.NotNullOrWhiteSpace(roomNo, "课室编号不能为空！");
            if (Get(p => p.RoomNo == roomNo) != null) return true;
            else return false;
        }
    }
}
