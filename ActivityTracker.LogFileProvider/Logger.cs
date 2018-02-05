using System.Configuration;
using System.Linq;

namespace ActivityTracker.LogFileProvider
{
    public class Logger
    {
        public static string LogFilePath
        {
            get
            {
                const string logFileKey = "LogFile";
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (config.AppSettings.Settings.AllKeys.Contains(logFileKey))
                {
                    return config.AppSettings.Settings[logFileKey].Value;
                }
                else
                {
                    return "ActivityLog.log";
                }
            }
        }
    }
}