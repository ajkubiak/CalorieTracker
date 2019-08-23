using System;
namespace Lib.Utils
{
    /**
     * <summary>The Settings utilities Singleton</summary>
     */
    public interface ISettingsUtils
    {
        void Initialize(string env);
        string GetDbConnectionString();
    }
    
    public class SettingsUtils : ISettingsUtils
    {
        private string env;

        public void Initialize(string env)
        {
            this.env = env;
        }
        public string GetDbConnectionString()
        {
            switch (env)
            {
                case :
                    break;
                case "Production":

                default:
                    break;
            }
            string host = null;
            string databaseName = null;
            string username = null;
            string password = null;
            return $"Host={host};Database={databaseName};Username={username};Password={password}";
        }
    }
}
