using System;

namespace HomeAutomation.Logging.Formatter
{
    public class StandardLogFormatter : ILogFormatter
    {
        public string Format(string message, LogLevel logLevel, DateTime logTime)
        {
            return $"[{logTime.ToString("yyyy-MM-dd HH:mm:ss")}] {logLevel} - {message}";
        }
    }
}
