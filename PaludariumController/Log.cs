using NLog;
using System;


namespace PaludariumController
{
    public enum LogTarget
    {
        File, Database, EventLog
    }

    public static class LogHelper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public static void Log(string message, string logLevel)
        {

            switch (logLevel)
            {
                case "info": { Logger.Info(message); break; }
                case "debug": { Logger.Debug(message); }; break;
                case "error": { Logger.Error(message); }; break;
                default:
                    throw new Exception("Unexpected Case");
            }
        }
    }
}
