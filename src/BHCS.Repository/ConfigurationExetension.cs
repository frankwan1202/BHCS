using BHCS.Infrastructure.Common;
using BHCS.Infrastructure.FastCommon.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Infrastructure.Fast.Configurations
{
    public static class ConfigurationExetension
    {
        public static FastConfiguration UseEntityFramework(this FastConfiguration fastConfiguration,string connectString)
        {
            if (string.IsNullOrWhiteSpace(connectString)) throw new ArgumentNullException("Please set db connect string!");
            var optionBuilder = new DbContextOptionsBuilder<EntityFrameworkDbContext>();
            optionBuilder.UseMySql(connectString);
            EntityFrameworkDbContext.Option = optionBuilder.Options;
            ObjectContainer.Register<DbContext, EntityFrameworkDbContext>(LifeScope.Transient);
            return fastConfiguration;
        }
    }
}
