using System;

namespace HomeAutomation.DeviceNetwork.XBee
{
    public class XBeeNetworkDiagnostics
    {
        public UInt64 XBeeMessageSentCount { get; set; }

        public UInt64 XBeeMessageReceiveCount { get; set; }

        public bool IsSerialPortConnected { get; set; }
    }
}
