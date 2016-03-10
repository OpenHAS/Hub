using System;
using System.Collections;
using System.Collections.Generic;

namespace HomeAutomation.Logging
{
    /// <summary>
    /// Provides logging functionality to the application.
    /// </summary>
    public static class Log
    {
        private class LogWriterInfo
        {
            public ILogWriter logWriter;
            public ILogFormatter logFormatter;
        }

        private static List<LogWriterInfo> logWriters = new List<LogWriterInfo>();

        public static void AddLogWriter(ILogWriter logWriter, ILogFormatter logFormatter)
        {
            logWriters.Add(new LogWriterInfo()
            {
                logWriter = logWriter,
                logFormatter = logFormatter
            });
        }

        public static void Debug(string message)
        {
            LogMessage(message, LogLevel.Debug);
        }

        public static void Error(string message)
        {
            LogMessage(message, LogLevel.Error);
        }

        public static void Information(string message)
        {
            LogMessage(message, LogLevel.Information);
        }

        public static void LogMessage(string message, LogLevel logLevel)
        {
            foreach (LogWriterInfo writerInfo in logWriters)
            {
                writerInfo.logWriter.Log(message, logLevel, writerInfo.logFormatter);
            }
        }
    }
}
