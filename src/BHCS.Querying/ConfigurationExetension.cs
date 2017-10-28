using BHCS.Infrastructure.Common;
using BHCS.Infrastructure.FastCommon.Components;
using BHCS.Querying;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Infrastructure.Fast.Configurations
{
    public static class ConfigurationExetension
    {
        public static FastConfiguration UseEntityFrameworkQuery(this FastConfiguration fastConfiguration,string connectString)
        {
            if (string.IsNullOrWhiteSpace(connectString)) throw new ArgumentNullException("Please set db connect string!");
            var optionBuilder = new DbContextOptionsBuilder<EntityFrameworkQueryDbContext>();
            optionBuilder.UseMySql(connectString);
            EntityFrameworkQueryDbContext.Option = optionBuilder.Options;
            ObjectContainer.Register<EntityFrameworkQueryDbContext>(LifeScope.Transient);
            return fastConfiguration;
        }
    }
}
