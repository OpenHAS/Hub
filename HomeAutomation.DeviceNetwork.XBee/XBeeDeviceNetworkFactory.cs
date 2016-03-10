using HomeAutomation.Core;
using HomeAutomation.Core.Network;
using MosziNet.XBee;
using System;
using System.Collections.Generic;
using Windows.Devices.SerialCommunication;

namespace HomeAutomation.DeviceNetwork.XBee
{
    class XBeeDeviceNetworkFactory : IDeviceNetworkFactory
    {
        uint baudRate;
        SerialParity parity;
        SerialStopBitCount stopBit;
        ushort dataBits;

        APIVersion apiVersion;

        public IDeviceNetwork CreateDeviceNetwork(Dictionary<string, string> configuration)
        {
            // get serial port configuration from the configuration dictionary
            baudRate = UInt32.Parse(configuration["BaudRate"]);
            parity = (SerialParity)Enum.Parse(typeof(SerialParity), configuration["SerialParity"], true);
            stopBit = (SerialStopBitCount)Enum.Parse(typeof(SerialStopBitCount), configuration["SerialStopBitCount"], true);
            dataBits = UInt16.Parse(configuration["DataBits"]);
            apiVersion = (APIVersion)Enum.Parse(typeof(APIVersion), configuration["APIVersion"], true);

            // create the serial port
            IXBeeSerialPort serialPort = CreateSerialPortWithSavedConfigurationParameters();

            // now create the device network with this serial port
            XBeeDeviceNetwork network = new XBeeDeviceNetwork(serialPort);

            return network;
        }

        public IXBeeSerialPort CreateSerialPortWithSavedConfigurationParameters()
        {
            return new XBeeSerialPort(baudRate, parity, stopBit, dataBits, apiVersion);
        }
    }
}
