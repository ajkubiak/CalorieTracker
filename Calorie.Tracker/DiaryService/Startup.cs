using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lib.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DiaryService
{
    public class Startup
    {
        //private readonly string CorsConfig = "_corsConfig";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // Singletons
            services.AddTransient<ISettingsUtils, SettingsUtils>();
            // CORS
            //services.AddCors(options =>
            //{
            //    options.AddPolicy(CorsConfig, builder =>
            //    {
            //        builder.WithOrigins("http://localhost");
            //    });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ISettingsUtils settingsUtils)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                settingsUtils.Initialize(EnvironmentName.Development);
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                settingsUtils.Initialize(EnvironmentName.Production);
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            //app.UseCors(CorsConfig);
        }
    }
}
