using System;

namespace HomeAutomation.Core
{
    public class DeviceNetworkDiagnosticsEventArgs : EventArgs
    {
        public object DiagnosticsObject;

        public DeviceNetworkDiagnosticsEventArgs(object diagnostics)
        {
            DiagnosticsObject = diagnostics;
        }
    }
}