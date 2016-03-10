using HomeAutomation.Core;
using MosziNet.XBee.Frame;

namespace HomeAutomation.DeviceNetwork.XBee.FrameProcessor
{
    internal interface IXBeeFrameProcessor
    {
        void ProcessFrame(XBeeDeviceNetwork deviceNetwork, IXBeeFrame frame);
    }
}
