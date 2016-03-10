using HomeAutomation.Core.Scheduler;
using System;

namespace HomeAutomation.Core.Diagnostics
{
    public class StatisticsService : IScheduledTask
    {
        private Statistics systemStatistics;
        private DateTime lastMeasureTime;
        private int statisticsIntervalInSeconds;

        public StatisticsService()
        {
            this.statisticsIntervalInSeconds = 1;

            systemStatistics = new Statistics();
            systemStatistics.SystemStartTime = DateTime.Now;

            lastMeasureTime = DateTime.Now;
        }

        private void ReportStatistics()
        {
            HomeAutomationSystem.ControllerHost.StatisticsReceived(systemStatistics);
        }

        private void GatherStatistics()
        {
            systemStatistics.CurrentTime = DateTime.Now;
            systemStatistics.UptimeDays = systemStatistics.CurrentTime.Subtract(systemStatistics.SystemStartTime).Days;
        }

        public void TimeElapsed()
        {
            // todo: refactor to use a "scheduler"
            // check if it's time to gather statistics
            if (lastMeasureTime.AddSeconds(statisticsIntervalInSeconds) < DateTime.Now)
            {
                lastMeasureTime = DateTime.Now;

                GatherStatistics();
                ReportStatistics();

                // todo: refactor this for a smaller update period
                RequestDeviceNetworkDiagnostics();
                RequestControllerDiagnostics();
            }
        }

        private void RequestControllerDiagnostics()
        {
            IController[] controllers = HomeAutomationSystem.ControllerRegistry.GetControllers();
            foreach(IController oneController in controllers)
            {
                object diagnosticsObject = oneController.GetUpdatedDiagnostics();
                if (diagnosticsObject != null)
                {
                    HomeAutomationSystem.ControllerHost.ControllerDiagnosticsReceived(oneController, diagnosticsObject);
                }
            }
        }

        private void RequestDeviceNetworkDiagnostics()
        {
            IDeviceNetwork[] networkList = HomeAutomationSystem.DeviceNetworkRegistry.GetDeviceNetworks();
            IController[] controllers = HomeAutomationSystem.ControllerRegistry.GetControllers();
            foreach (IDeviceNetwork oneNetwork in networkList)
            {
                object diagnostics = oneNetwork.GetUpdatedDiagnostics();

                if (diagnostics != null)
                {
                    foreach (IController oneController in controllers)
                    {
                        HomeAutomationSystem.ControllerHost.DeviceNetworkDiagnosticsReceived(oneNetwork, diagnostics);
                    }
                }
            }
        }
    }
}
