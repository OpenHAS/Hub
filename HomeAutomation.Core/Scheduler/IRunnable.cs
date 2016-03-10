namespace HomeAutomation.Core.Scheduler
{
    public interface IRunnable
    {
        void Step();
        void Start();
        void Stop();
    }
}