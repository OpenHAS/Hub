using HomeAutomation.Core.Service;

namespace HomeAutomation.Core.Scheduler
{
    public interface IScheduledTask
    {
        // invoked when the requested time period has elapsed.
        void TimeElapsed();
    }
}
