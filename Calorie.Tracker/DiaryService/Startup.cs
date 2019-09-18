using System;
using Lib.Models.Auth;
using Lib.Models.Database.Diary;
using Lib.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DiaryService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddHttpContextAccessor();

            #region Singletons
            services.AddSingleton<ISettingsUtils, SettingsUtils>();
            services.AddScoped<IDiaryDb, DiaryDb>();
            #endregion

            #region JWT Authentication/Authorization
            // get config
            TokenConfig tokenConfig = Configuration.GetSection("tokenConfig").Get<TokenConfig>();

            // apply config
            services
                .AddAuthentication(AuthUtils.AddAuthentiction())
                .AddJwtBearer(AuthUtils.AddJwtBearer(Configuration));

            // Apply RBAC
            services.AddAuthorization(AuthUtils.AddAuthorization());
            #endregion
        }

        //public void ConfigureDesignTimeServices(IServiceCollection services)
        //{
        //    services.AddSingleton<ISettingsUtils, SettingsUtils>();
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ISettingsUtils settingsUtils)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Configure utils for this environment
            RuntimeEnvironment myEnv = RuntimeEnvironment.UNKNOWN;
            try
            {
                Enum.TryParse<RuntimeEnvironment>(env.EnvironmentName, out myEnv);
            }
            catch (ArgumentException ex)
            {
                Log.Error($"Could not parse environment from config. Setting as {RuntimeEnvironment.UNKNOWN}", ex);
                throw ex;
            }
            settingsUtils.Initialize(myEnv);

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }

    
}
