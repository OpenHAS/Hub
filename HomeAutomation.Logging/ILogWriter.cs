using System;

namespace HomeAutomation.Logging
{
    public interface ILogWriter
    {
        void Log(string message, LogLevel logLevel, ILogFormatter logFormatter);
    }
}
