using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Infrastructure.Middlewares
{
    public static class MiddlewareExetension
    {
        public static IApplicationBuilder UseMvcContext(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException("app");
            return app.UseMiddleware<MvcContextMiddleware>();
        }
    }
}
