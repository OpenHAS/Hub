using System;

namespace HomeAutomation.Core.Diagnostics
{
    public class Statistics
    {
        public DateTime SystemStartTime { get; set; }

        public DateTime CurrentTime { get; set; }

        public int UptimeDays { get; set; }
    }
}
