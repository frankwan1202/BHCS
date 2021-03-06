﻿using BHCS.Domain.Models;
using BHCS.Infrastructure.Fast.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Domain.Repositories
{
    public interface IClassBuildingRepository:IRepository<ClassBuilding>
    {
        bool IsExist(string name);
    }
}
