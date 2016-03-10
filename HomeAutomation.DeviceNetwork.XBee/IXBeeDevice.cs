using HomeAutomation.Core;
using MosziNet.XBee.Frame;

namespace HomeAutomation.DeviceNetwork.XBee
{
    public interface IXBeeDevice : IDevice
    {
        void ProcessFrame(IXBeeFrame frame);
    }
}
