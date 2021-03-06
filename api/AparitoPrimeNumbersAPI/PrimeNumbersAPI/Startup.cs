using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PrimeNumbersAPI.Logic;

namespace WebApplication1
{
    public class Startup
    {
        private readonly string DEVELOPMENT_CORS_POLICY = "DevelopmentCorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddCors(options =>
            {
                options.AddPolicy(name: DEVELOPMENT_CORS_POLICY,
                    builder =>
                    {
                        builder.WithOrigins(Environment.GetEnvironmentVariable("DEVELOPMENT_CORS_ORIGIN"))
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            services.AddMemoryCache();

            services.AddControllers();

            services.AddSingleton<IPrimeNumberLogic, PrimeNumberLogic>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseCors(DEVELOPMENT_CORS_POLICY);
            }

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
