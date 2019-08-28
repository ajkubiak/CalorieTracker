using System;
using Microsoft.Extensions.Logging;

namespace Lib.Utils
{
    public enum RuntimeEnvironment
    {
        UNKNOWN,
        DEVELOP,
        INTEGRATION,
        PRODUCTION
    }
    /**
     * <summary>The Settings utilities Singleton</summary>
     */
    public interface ISettingsUtils
    {
        /**
         * <summary>Initialize the settings for the runtime environment</summary>
         */
        void Initialize();
        string GetDbConnectionString();
        //DbContextOptions<T> BuildOptions<T>() where T : Microsoft.EntityFrameworkCore.DbContext;
    }

    public class SettingsUtils : ISettingsUtils
    {
        private readonly ILogger _logger;
        private readonly string _hostingEnvironment;
        private readonly IConfiguration _config;
        private RuntimeEnvironment env;

        public SettingsUtils(ILogger<SettingsUtils> logger, IHostingEnvironment env, IConfiguration config)
        {
            _logger = logger;
            _hostingEnvironment = env.EnvironmentName;
            _config = config;
        }

        public void Initialize() 
        {
            env = RuntimeEnvironment.UNKNOWN;
            try
            {
                Enum.TryParse<RuntimeEnvironment>(_hostingEnvironment, out env);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"Could not parse environment from config. Setting as {RuntimeEnvironment.UNKNOWN}", ex);
                throw ex;
            }
            _logger.LogDebug("Environment is set to: {env}", env);
        }

        /**
         * <summary>
         *  Retrieve database connection settings. Must call the <see cref="Initialize(RuntimeEnvironment)"/> method
         *  before calling this function.
         * </summary>
         * <returns>Postgresql connection string. Null if no environment is defined.</returns>
         */
        public string GetDbConnectionString()
        {
            _logger.LogDebug("retrieving env settings for {env}", env);
            string host = null;
            string databaseName = null;
            string username = null;
            string password = null;

            switch (env)
            {
                case RuntimeEnvironment.DEVELOP:
                    host = _config.GetValue<string>("DB_NAME");
                    databaseName = _config.GetValue<string>("DB_HOST");
                    username = _config.GetValue<string>("DB_USER");
                    password = _config.GetValue<string>("DB_PASS");
                    _logger.LogDebug($"Host={host};Database={databaseName};Username={username};");
                    return $"Host={host};Database={databaseName};Username={username};Password={password}";
                case RuntimeEnvironment.INTEGRATION:
                    break;
                case RuntimeEnvironment.PRODUCTION:
                    break;

                default:
                    _logger.LogWarning("No environment specified.");
                    return null;
            }
            
            return $"Host={host};Database={databaseName};Username={username};Password={password}";
        }
    }
}
