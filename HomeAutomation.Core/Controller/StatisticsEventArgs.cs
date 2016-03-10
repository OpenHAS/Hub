using HomeAutomation.Core.Diagnostics;
using System;

namespace HomeAutomation.Core
{
    public class StatisticsEventArgs : EventArgs
    {
        public Statistics Statistics { get; set; }

        public StatisticsEventArgs(Statistics s)
        {
            Statistics = s;
        }
    }
}