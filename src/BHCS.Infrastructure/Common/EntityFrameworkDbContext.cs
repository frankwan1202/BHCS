using BHCS.Infrastructure.Fast.Configurations;
using BHCS.Infrastructure.FastCommon.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Infrastructure.Common
{
    public class EntityFrameworkDbContext:DbContext
    {
        public EntityFrameworkDbContext(DbContextOptions<EntityFrameworkDbContext> option):base(option)
        {
            
        }
    }
}
