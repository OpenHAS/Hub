using System;
using System.Reflection;

namespace HomeAutomation.Core
{
    /// <summary>
    /// Provides a basic implementation of the <see cref="IDevice"/> interface.
    /// </summary>
    public abstract class DeviceBase : IDevice
    {
        public byte[] DeviceID { get; set; }

        public byte[] NetworkAddress { get; set; }

        public abstract DeviceState DeviceState { get; }

        public IDeviceNetwork DeviceNetwork { get; set; }

        /// <summary>
        /// Executes the command on this instance. 
        /// More precisely this invokes the aCommand.Name method with the specified parameters.
        /// </summary>
        /// <param name="aCommand"></param>
        public void ExecuteCommand(DeviceCommand aCommand)
        {
            MethodInfo executableMethod = this.GetType().GetMethod(aCommand.Name);

            object[] parameters = new object[aCommand.Parameters.Length];
            ParameterInfo[] parameterInfos = executableMethod.GetParameters();

            for (int i = 0; i < parameters.Length; i++)
            {
                Type destinationType = parameterInfos[i].ParameterType;

                parameters[i] = Convert.ChangeType(aCommand.Parameters[i], destinationType);
            }

            executableMethod.Invoke(this, parameters);
        }
    }
}
