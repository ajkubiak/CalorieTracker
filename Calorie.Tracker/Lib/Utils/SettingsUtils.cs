﻿using Microsoft.Extensions.Configuration;
using Serilog;

namespace Lib.Utils
{
    public enum RuntimeEnvironment
    {
        UNKNOWN,
        Development,
        INTEGRATION,
        PRODUCTION
    }
    /**
     * <summary>The Settings utilityies Singleton</summary>
     */
    public interface ISettingsUtils
    {
        /**
         * <summary>Initialize the settings for the runtime environment</summary>
         */
        void Initialize(RuntimeEnvironment env);
        string GetDbConnectionString();
    }

    public class SettingsUtils : ISettingsUtils
    {
        public static readonly string dbstring;
        private readonly IConfiguration _config;
        private RuntimeEnvironment env;

        public SettingsUtils(IConfiguration config)
        {
            _config = config;

        }

        public void Initialize(RuntimeEnvironment env)
        {
            Log.Debug("Environment is set to {env}", env);
            this.env = env;
        }

        /**
         * <summary>
         *  Retrieve database connection settings. Must call the <see cref="Initialize(RuntimeEnvironment)"/> method
         *  before calling this function.
         *
         *  NOTE: This logic must be match the design time database connection string
         * </summary>
         * 
         * <returns>Postgresql connection string. Null if no environment is defined.</returns>
         */
        public string GetDbConnectionString()
        {
            string host = null;
            string databaseName = null;
            string username = null;
            string password = null;

            switch (env)
            {
                case RuntimeEnvironment.Development:
                    host = _config.GetValue<string>("DB_HOST");
                    databaseName = _config.GetValue<string>("DB_NAME");
                    username = _config.GetValue<string>("DB_USER");
                    password = _config.GetValue<string>("DB_PASS");
                    Log.Debug($"Host={host};Database={databaseName};Username={username};");
                    return $"Host={host};Database={databaseName};Username={username};Password={password}";
                case RuntimeEnvironment.INTEGRATION:
                    break;
                case RuntimeEnvironment.PRODUCTION:
                    break;

                default:
                    Log.Warning("No environment specified.");
                    return null;
            }

            return $"Host={host};Database={databaseName};Username={username};Password={password}";
        }
    }
}
