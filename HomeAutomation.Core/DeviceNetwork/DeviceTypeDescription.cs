using System;

namespace HomeAutomation.Core
{
    /// <summary>
    /// Contains information about a device type participating in a <see cref="IDeviceNetwork"/>.
    /// </summary>
    public class DeviceTypeDescription
    {
        public int DeviceTypeId { get; set; }

        public string DeviceClassTypeString { get; set; }

        public string DeviceNetworkName { get; set; }

        public DeviceTypeDescription()
        {
        }

        public DeviceTypeDescription(int deviceTypeId, string deviceClassTypeString, string deviceNetworkName)
        {
            DeviceTypeId = deviceTypeId;
            DeviceClassTypeString = deviceClassTypeString;
            DeviceNetworkName = deviceNetworkName;
        }

        public Type DeviceClassType
        {
            get
            {
                return Type.GetType(DeviceClassTypeString);
            }
        }
    }
}
