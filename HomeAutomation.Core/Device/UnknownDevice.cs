namespace HomeAutomation.Core
{
    public class UnknownDevice : DeviceBase
    {
        public override DeviceState DeviceState
        {
            get
            {
                return new DeviceState()
                {
                    Device = this,
                    ComponentStateList = new ComponentState[] { }
                };
            }
        }
    }
}
