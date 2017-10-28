using BHCS.Querying.ReadModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Querying
{
    public class EntityFrameworkQueryDbContext:DbContext
    {
        public static DbContextOptions<EntityFrameworkQueryDbContext> Option;

        public EntityFrameworkQueryDbContext():base(Option)
        { }

        public DbSet<RoleReadModel> Roles { get; set; }

    }
}
