using HomeAutomation.Core;
using HomeAutomation.DeviceNetwork.XBee;
using HomeAutomation.Logging;
using MosziNet.XBee.Frame;
using System;

namespace MosziNet.Devices.XBee
{
    public class EpamTemperatureDeviceV2 : DeviceBase, IXBeeDevice
    {
        private static readonly double AnalogPinMaxVoltage = 1200.0; // in millivolts
        private static readonly double AnalogPinResolution = 1023;

        public double Temperature { get; private set; }

        public void ProcessFrame(IXBeeFrame frame)
        {
            IODataSample dataSample = frame as IODataSample;
            if (dataSample != null)
            {
                if (dataSample.Samples.Length == 2)
                {
                    int measuredAnalogValue = dataSample.Samples[0] * 256 + dataSample.Samples[1];

                    // now read the temperature sensor reading
                    double analogReading = measuredAnalogValue * AnalogPinMaxVoltage / AnalogPinResolution;

                    Temperature = HomeAutomation.Sensor.Temperature.MCP9701A.TemperatureFromVoltage(analogReading);
                }
                else
                {
                    Log.Debug("[TemperatureDeviceV1] Wrong number of samples received.");
                }
            }
            else
            {
                Log.Debug("[TemperatureDeviceV1] Wrong frame type (or null) for temperature sensor device.");
            }
        }

        public override DeviceState DeviceState
        {
            get
            {
                return new DeviceState()
                {
                    Device = this,
                    ComponentStateList = new ComponentState[]
                    {
                    new ComponentState() { Name = "MCP9701A", Value = Temperature.ToString("N1") }
                    }
                };
            }
        }
    }
}
