using System;
using HomeAutomation.Core;
using HomeAutomation.Core.DeviceNetwork;
using System.Threading.Tasks;
using HomeAutomation.Core.Scheduler;

namespace HomeAutomation.Tests.FakeNetwork
{
    public class FakeNetwork : IDeviceNetwork, IScheduledTask
    {
        private DateTime lastMeasureTime;
        private DeviceNetworkHost networkHost;
        private Random randomGenerator;

        public void ExecuteCommand(DeviceCommand command)
        {
        }

        public object GetUpdatedDiagnostics()
        {
            return null;
        }

        public void Initialize(DeviceNetworkHost deviceNetworkHost)
        {
            networkHost = deviceNetworkHost;

            lastMeasureTime = DateTime.Now;
            randomGenerator = new Random(lastMeasureTime.Millisecond);
        }

        public void TimeElapsed()
        {
            // todo: refactor to use a "scheduler"
            // check if it's time to gather statistics
            if (lastMeasureTime.AddSeconds(5) < DateTime.Now)
            {
                lastMeasureTime = DateTime.Now;

                // send a new update to the host
                networkHost.DeviceStateReceived(new DeviceState()
                {
                     Device = new FakeDevice(this),
                     ComponentStateList = new ComponentState[]
                     {
                         new ComponentState()
                         {
                              Name = "LM35",
                              Value = GetRandomValue().ToString("N1")
                         }
                     }
                });
            }
        }

        private double GetRandomValue()
        {
            return randomGenerator.NextDouble() * 20;
        }
    }
}
