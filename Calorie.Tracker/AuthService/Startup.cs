﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Lib.Models;
using Lib.Models.Database.Auth;
using Lib.Utils;
using Serilog;

namespace AuthService
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Singletons
            services.AddSingleton<ISettingsUtils, SettingsUtils>();
            services.AddSingleton<IAuthDb, AuthDb>();

            // Use authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = Configuration.GetSection("authentication").GetValue<string>("authority");
                options.RequireHttpsMetadata = false;
                options.Audience = Configuration.GetSection("authentication").GetValue<string>("audience");
            });
        }

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
            app.UseMvc();
            app.UseAuthentication();
        }
    }
}
