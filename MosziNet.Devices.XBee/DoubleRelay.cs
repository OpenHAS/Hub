using MosziNet.XBee;

namespace MosziNet.Devices.XBee
{
    public class DoubleRelay : RelayDeviceBase
    {
        /// <summary>
        /// Configure this device with D0 and D2 pins.
        /// </summary>
        public DoubleRelay() : base(Pins.AD0_DIO0, Pins.AD2_DIO2)
        {

        }
    }
}
