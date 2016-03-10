namespace HomeAutomation.Core
{
    /// <summary>
    /// Base interface for all XBee based devices in the system.
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// Defines the device's id. (hardware address)
        /// </summary>
        byte[] DeviceID { get; set; }

        /// <summary>
        /// Defined the device's network address.
        /// </summary>
        byte[] NetworkAddress { get; set; }

        /// <summary>
        /// Returns a <see cref="DeviceState"/> structure with all the components from this device.
        /// </summary>
        /// <returns></returns>
        DeviceState DeviceState { get; }

        /// <summary>
        /// The device network where this device operates.
        /// </summary>
        IDeviceNetwork DeviceNetwork { get; }

        /// <summary>
        /// Executes the command on this instance. 
        /// More precisely this invokes the aCommand.Name method with the specified parameters.
        /// </summary>
        /// <param name="aCommand"></param>
        void ExecuteCommand(DeviceCommand aCommand);
    }
}
