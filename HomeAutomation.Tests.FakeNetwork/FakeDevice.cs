using HomeAutomation.Core;
using System.Collections.Generic;
using System.Text;

namespace HomeAutomation.Tests.FakeNetwork
{
    class FakeDevice : DeviceBase
    {
        static int deviceIndex = 0;
        static List<byte[]> fakeDevices = new List<byte[]>();

        static FakeDevice()
        {
            fakeDevices.Add(Encoding.UTF8.GetBytes("ABCDEF"));
            fakeDevices.Add(Encoding.UTF8.GetBytes("123456"));
            fakeDevices.Add(Encoding.UTF8.GetBytes("FFF888"));
        }

        private static byte[] GetNextDeviceID()
        {
            deviceIndex = (deviceIndex + 1) % fakeDevices.Count;

            return fakeDevices[deviceIndex];
        }

        public FakeDevice(IDeviceNetwork network)
        {
            this.DeviceID = GetNextDeviceID();
            this.NetworkAddress = new byte[] { 0 };
            this.DeviceNetwork = network;
        }

        public override DeviceState DeviceState
        {
            get
            {
                return null;
            }
        }
    }
}
