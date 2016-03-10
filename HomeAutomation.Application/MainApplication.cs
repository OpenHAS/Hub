using HomeAutomation.Application.Configuration;
using HomeAutomation.Core;
using HomeAutomation.Logging;

namespace HomeAutomation.Application
{
    public class MainApplication
    {
        public static void Initialize(string configurationFile)
        {
            var configuration = new ConfigurationManager().LoadFile<HomeAutomationConfiguration>(configurationFile);

            new ApplicationInitializer().Initialize(configuration);
        }

        public static void Run()
        {
            HomeAutomationSystem.ScheduledTasks.Runner.Start();

            Log.Debug("[HomeAutomation] Started.");
        }
    }
}
