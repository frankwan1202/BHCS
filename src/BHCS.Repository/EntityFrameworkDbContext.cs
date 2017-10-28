using BHCS.Domain.Models;
using BHCS.Domain.Models.Users;
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
        public static DbContextOptions<EntityFrameworkDbContext> Option;

        public EntityFrameworkDbContext():base(Option)
        {
            
        }

        public DbSet<Role> Roles { get; set; } 

        public DbSet<User> Users { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Student> Students { get; set; }
    }
}
