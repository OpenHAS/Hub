namespace HomeAutomation.Core.Scheduler
{
    internal class ScheduledTaskInfo
    {
        /// <summary>
        /// The task that is scheduled.
        /// </summary>
        public IScheduledTask Task { get; set; }

        /// <summary>
        /// The period in seconds to wait between the tasks are run. This is not an exact time period, but it should be considered
        /// at least + best effort. 
        /// </summary>
        public int ScheduledTimePeriod { get; set; }
    }
}
