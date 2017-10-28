using BHCS.Infrastructure.Fast.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Application.Commanding
{
    public class CreateNewClassRoomCommand:Command
    {
        public string RoomNo { get; set; }

        public string Name { get; set; }

        public Guid ClassBuildingId { get; set; }
    }
}
