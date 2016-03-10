using HomeAutomation.Core;
using System.Diagnostics;

namespace HomeAutomation.Controller.Internal
{
    public class InternalController : IController
    {
        public object GetUpdatedDiagnostics()
        {
            return null;
        }

        public void Initialize(ControllerHost controllerHost)
        {
            controllerHost.OnDeviceStateReceived += ControllerHost_OnDeviceStateReceived;
        }

        private void ControllerHost_OnDeviceStateReceived(object sender, DeviceStateEventArgs e)
        {
            
        }
    }
}
