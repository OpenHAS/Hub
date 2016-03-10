using System.Runtime.Serialization;
using Windows.Devices.SerialCommunication;

namespace HomeAutomation.Application.Configuration
{
    [DataContract]
    public class XBeeConfiguration
    {
        [DataMember]
        public uint BaudRate { get; set; }

        [DataMember]
        public SerialParity SerialParity { get; set; }

        [DataMember]
        public SerialStopBitCount SerialStopBitCount { get; set; }

        [DataMember]
        public ushort DataBits { get; set; }
    }
}
