using HomeAutomation.Core;
using HomeAutomation.DeviceNetwork.XBee;
using HomeAutomation.Logging;
using MosziNet.XBee.Frame;
using System;

namespace MosziNet.Devices.XBee
{
    public class EpamTemperatureDeviceV1 : DeviceBase, IXBeeDevice
    {
        private byte MagiConstant_EpamDevice = 0xEA; // EpAm
        private byte MagiConstant_TemperatureDevice = 01;

        public float Temperature { get; private set; }

        public override DeviceState DeviceState
        {
            get
            {
                return new DeviceState()
                {
                    Device = this,
                    ComponentStateList = new ComponentState[]
                    {
                    new ComponentState() { Name = "TMP", Value = Temperature.ToString("N1") }
                    }
                };
            }
        }

        public void ProcessFrame(IXBeeFrame frame)
        {
            ReceivePacket dataSample = frame as ReceivePacket;
            if (dataSample != null)
            {
                if (dataSample.ReceivedData[0] == MagiConstant_EpamDevice &&
                    dataSample.ReceivedData[1] == MagiConstant_TemperatureDevice)
                {
                    Temperature = BitConverter.ToSingle(dataSample.ReceivedData, 2);
                }
                else
                {
                    Log.Debug("[EpamTemperatureDeviceV1] Wrong start bytes.");
                }
            }
            else
            {
                Log.Debug("[EpamTemperatureDeviceV1] Wrong frame type (or null).");
            }
        }

        
    }
}
