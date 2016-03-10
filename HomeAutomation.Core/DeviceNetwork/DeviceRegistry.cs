using HomeAutomation.Logging;
using HomeAutomation.Util;
using System;
using System.Collections.Generic;

namespace HomeAutomation.Core
{
    /// <summary>
    /// Keeps an up to date registry of the devices in the system.
    /// </summary>
    public class DeviceRegistry
    {
        private Dictionary<string, IDevice> deviceRegistry = new Dictionary<string, IDevice>();
        private List<string> stagingDevices = new List<string>();

        /// <summary>
        /// Registers a device into the system.
        /// </summary>
        /// <param name="deviceNetwork">The device network for this device.</param>
        /// <param name="device">The device instance that handles all things related to a device.</param>
        /// <param name="deviceByteId">The address/id of the device.</param>
        public void RegisterDevice(IDeviceNetwork deviceNetwork, IDevice device, byte[] deviceByteId)
        {
            string deviceId = GetDeviceId(deviceNetwork, deviceByteId);

            if (deviceRegistry.ContainsKey(deviceId))
            {
                Log.Debug("Replacing device for id: " + deviceId);
            }

            // make sure before we add this device to our network, we will 
            // set the deviceNetwork for this device.
            ((DeviceBase)device).DeviceNetwork = deviceNetwork;

            deviceRegistry[deviceId] = device;

            stagingDevices.Remove(deviceId);
        }

        /// <summary>
        /// Returns the class type that handles all things related to the device with the specified address.
        /// </summary>
        /// <param name="deviceByteId"></param>
        /// <returns></returns>
        public IDevice GetDeviceById(IDeviceNetwork deviceNetwork, byte[] deviceByteId)
        {
            string deviceId = GetDeviceId(deviceNetwork, deviceByteId);

            IDevice device = deviceRegistry.ContainsKey(deviceId) ? (IDevice)deviceRegistry[deviceId] : null;

            return device;
        }

        public void RegisterStagingDevice(IDeviceNetwork deviceNetwork, byte[] deviceByteId)
        {
            string deviceId = GetDeviceId(deviceNetwork, deviceByteId);

            stagingDevices.Add(deviceId);
        }

        public bool IsStagingDevice(IDeviceNetwork deviceNetwork, byte[] deviceByteId)
        {
            string deviceId = GetDeviceId(deviceNetwork, deviceByteId);

            return stagingDevices.Contains(deviceId);
        }

        public void RegisterDeviceWithTypeID(IDeviceNetwork deviceNetwork, int deviceIdentification, byte[] address, byte[] networkAddress)
        {
            if (IsStagingDevice(deviceNetwork, address))
            {
                // get the device from the configuration based on this type id
                Type deviceType = HomeAutomationSystem.DeviceTypeRegistry.GetDeviceType(deviceNetwork, deviceIdentification);
                IDevice device = Activator.CreateInstance(deviceType) as IDevice;

                if (device != null)
                {
                    device.DeviceID = address;
                    device.NetworkAddress = networkAddress;

                    RegisterDevice(deviceNetwork, device, device.DeviceID);
                }
                else
                {
                    Log.Debug($"The device type specified by the sensor with ID {address.ToHexString()} is not know.");

                    RegisterDevice(deviceNetwork, new UnknownDevice(), address);
                }
            }
        }

        private string GetDeviceId(IDeviceNetwork deviceNetwork, byte[] deviceId)
        {
            return HomeAutomationSystem.DeviceNetworkRegistry.GetDeviceNetworkUniqueId(deviceNetwork) + deviceId.ToHexString();
        }
    }
}
