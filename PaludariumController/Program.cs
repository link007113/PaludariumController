using System;
using NLog;
using NLog.Config;
using NLog.Targets;
using System.Windows.Forms;

namespace PaludariumController
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SetupLog();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static void SetupLog()
        {            
            var config = new LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new FileTarget("logfile")
            {
                FileName = "${basedir}/logs/latest.log",
                ArchiveFileName = "${basedir}/logs/archives/AppLog.${longdate}.log",
                MaxArchiveFiles = 30,
                ArchiveEvery = FileArchivePeriod.Day,
                ArchiveAboveSize = 102400000,
                ArchiveOldFileOnStartup = true
            };

            // Rules for mapping loggers to targets   
            config.AddRule(LogLevel.Info, LogLevel.Error, logfile);

            // Apply config           
            LogManager.Configuration = config;
        }
    }
}
