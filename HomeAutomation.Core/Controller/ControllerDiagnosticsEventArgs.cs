using System;

namespace HomeAutomation.Core.Controller
{
    public class ControllerDiagnosticsEventArgs : EventArgs
    {
        public ControllerDiagnosticsEventArgs(object diagnosticsObject)
        {
            DiagnosticsObject = diagnosticsObject;
        }

        public object DiagnosticsObject { get;}
    }
}
