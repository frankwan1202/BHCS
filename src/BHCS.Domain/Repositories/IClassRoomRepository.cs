﻿using BHCS.Domain.Models;
using BHCS.Infrastructure.Fast.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Domain.Repositories
{
    public interface IClassRoomRepository:IRepository<ClassRoom>
    {
        bool IsExist(string roomNo);
    }
}