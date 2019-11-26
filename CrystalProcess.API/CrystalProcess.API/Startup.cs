using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrystalProcess.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace CrystalProcess.API
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //HACK - till microsoft issue guidence for not being able to override
            //the database provider in new webApplicationFactory.
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            var env = serviceProvider.GetService<IHostingEnvironment>();
            //logic in here to configure correct DB 
            if (env.IsEnvironment("IntegrationTests"))
            {
                services.AddEntityFrameworkInMemoryDatabase();
                
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>
                    (options => options.UseSqlServer(Configuration.GetConnectionString("CrystalProcessDatabase")));

            }

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsEnvironment("IntegrationTests"))
            {
                //code in here to override database service with inmemory db.
            }
               

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
