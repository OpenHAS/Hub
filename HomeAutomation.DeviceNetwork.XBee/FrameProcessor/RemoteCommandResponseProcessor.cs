using HomeAutomation.Core;
using MosziNet.XBee;
using MosziNet.XBee.Frame;

namespace HomeAutomation.DeviceNetwork.XBee.FrameProcessor
{
    internal class RemoteCommandResponseProcessor : IXBeeFrameProcessor
    {
        public void ProcessFrame(XBeeDeviceNetwork deviceNetwork, IXBeeFrame frame)
        {
            RemoteCommandResponse responseFrame = (RemoteCommandResponse)frame;

            // check if this is a DD command response - used for identifying devices by their type
            if (XBeeFrameUtil.IsSameATCommand(responseFrame.ATCommand, ATCommands.DD))
            {
                // build the ID for the device type based on the frame info
                int deviceIdentification = responseFrame.Parameters[2] * 256 + responseFrame.Parameters[3];

                HomeAutomationSystem.DeviceRegistry.RegisterDeviceWithTypeID(deviceNetwork, deviceIdentification, frame.Address, frame.NetworkAddress);
            }
            else
            {                
                // get the device type from device registry, and send the frame for processing it.
                IXBeeDevice device = HomeAutomationSystem.DeviceRegistry.GetDeviceById(deviceNetwork, frame.Address) as IXBeeDevice;

                device?.ProcessFrame(frame);
            }
        }
    }
}
