using HomeAutomation.Core.DeviceNetwork;
using HomeAutomation.Core.Scheduler;
using HomeAutomation.Core.Service;

namespace HomeAutomation.Core
{
    /// <summary>
    /// Provides easy access to information related to device networks, device instances, devices types, etc.
    /// </summary>
    public static class HomeAutomationSystem
    {
        public static DeviceTypeRegistry DeviceTypeRegistry { get; private set; } = new DeviceTypeRegistry();

        public static DeviceRegistry DeviceRegistry { get; private set; } = new DeviceRegistry();

        public static DeviceNetworkRegistry DeviceNetworkRegistry { get; private set; } = new DeviceNetworkRegistry();

        public static ControllerRegistry ControllerRegistry { get; private set; } = new ControllerRegistry();

        public static ServiceRegistry ServiceRegistry { get; private set; } = new ServiceRegistry();

        public static ScheduledTaskManager ScheduledTasks { get; private set; } = new ScheduledTaskManager();

        
        internal static ControllerHost ControllerHost { get; private set; } = new ControllerHost();

        internal static DeviceNetworkHost DeviceNetworkHost { get; private set; } = new DeviceNetworkHost();

        internal static Gateway Gateway { get; private set; } = new Gateway();
    }
}
