using System;

namespace HomeAutomation.Core
{
    public class DeviceStateEventArgs : EventArgs
    {
        public DeviceState DeviceState { get; set; }

        public DeviceStateEventArgs(DeviceState deviceState)
        {
            DeviceState = deviceState;
        }
    }
}