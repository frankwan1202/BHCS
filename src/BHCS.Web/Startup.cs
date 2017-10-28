using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using BHCS.Infrastructure.FastCommon.Configurations;
using BHCS.Infrastructure.Fast.Configurations;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Razor;
using BHCS.Infrastructure.FastDbCommon.Infrastructure.MySql;
using BHCS.Infrastructure.FastCommon.Utilities;
using BHCS.Infrastructure.Middlewares;
using BHCS.Web;

namespace web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcContext();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });

            Configuration.Instance.UseAutofac().RegisterComponents()
                .UseNewtonsoftJson()
                .UseUserSession(new WebUserSession())
                .UseMySql(new MySqlDatabase("Data Source=127.0.0.1;port=3306;Initial Catalog=bhcs;user id=root;password=wanbin1994;Character Set=utf8;SslMode=none"));

            var fastConfiguration = new FastConfiguration().Intialize()
                .RegisterAssembiles(
                    Assembly.Load("BHCS.Application"),
                    Assembly.Load("BHCS.Domain"),
                    Assembly.Load("BHCS.Repositories.DbCommon"),
                    Assembly.Load("BHCS.Querying")
                );
                //.UseEntityFramework()
                //.UseEntityFrameworkQuery("Data Source=127.0.0.1;port=3306;Initial Catalog=bhcs;user id=root;password=wanbin1994;Character Set=utf8");
        }
        
    }
}
