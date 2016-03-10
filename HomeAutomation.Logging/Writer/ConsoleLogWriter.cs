using System;
using System.Diagnostics;

namespace HomeAutomation.Logging.Writer
{
    public class ConsoleLogWriter : ILogWriter
    {
        public void Log(string message, LogLevel logLevel, ILogFormatter logFormatter)
        {
            Debug.WriteLine(logFormatter.Format(message, logLevel, DateTime.Now));
        }
    }
}
