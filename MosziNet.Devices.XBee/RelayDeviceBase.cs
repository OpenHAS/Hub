using HomeAutomation.Core;
using HomeAutomation.DeviceNetwork.XBee;
using HomeAutomation.Logging;
using HomeAutomation.Util;
using MosziNet.XBee;
using MosziNet.XBee.Frame;
using System.Collections.Generic;

namespace MosziNet.Devices.XBee
{
    /// <summary>
    /// An abstract class that defines the relay basics.
    /// </summary>
    public abstract class RelayDeviceBase : DeviceBase, IXBeeDevice
    {
        private const byte StateOFF = 0;
        private const byte StateON = 1;

        private const byte PinLow = 4;
        private const byte PinHigh = 5;

        private List<Pins> xbeeConfiguration = new List<Pins>();
        protected byte[] switchStates;

        private Dictionary<Pins, byte[]> atCommandForPin = new Dictionary<Pins, byte[]>()
        {
            [Pins.AD0_DIO0] = ATCommands.D0,
            [Pins.AD1_DIO1] = ATCommands.D1,
            [Pins.AD2_DIO2] = ATCommands.D2,
            [Pins.AD3_DIO3] = ATCommands.D3,
            [Pins.AD4_DIO4] = ATCommands.D4,
            [Pins.AD5_DIO5] = ATCommands.D5,
            [Pins.AD6_DIO6] = ATCommands.D6,
            [Pins.DIO7] = ATCommands.D7,
            [Pins.DI8] = ATCommands.D8
        };
   
        public RelayDeviceBase(Pins relayXBeePin1)
        {
            switchStates = new byte[1];
            xbeeConfiguration.Add(relayXBeePin1);
        }

        public RelayDeviceBase(Pins relayXBeePin1, Pins relayXBeePin2)
        {
            switchStates = new byte[2];
            xbeeConfiguration.Add(relayXBeePin1);
            xbeeConfiguration.Add(relayXBeePin2);
        }

        public RelayDeviceBase(Pins relayXBeePin1, Pins relayXBeePin2, Pins relayXBeePin3)
        {
            switchStates = new byte[3];
            xbeeConfiguration.Add(relayXBeePin1);
            xbeeConfiguration.Add(relayXBeePin2);
            xbeeConfiguration.Add(relayXBeePin3);
        }

        public RelayDeviceBase(Pins relayXBeePin1, Pins relayXBeePin2, Pins relayXBeePin3, Pins relayXBeePin4)
        {
            switchStates = new byte[4];

            xbeeConfiguration.Add(relayXBeePin1);
            xbeeConfiguration.Add(relayXBeePin2);
            xbeeConfiguration.Add(relayXBeePin3);
            xbeeConfiguration.Add(relayXBeePin4);
        }


        #region / DeviceBase implementation /

        /// <summary>
        /// Processes any frames sent to this device.
        /// </summary>
        /// <param name="frame"></param>
        public virtual void ProcessFrame(IXBeeFrame frame)
        {
            IODataSample dataSample = frame as IODataSample;
            if (dataSample != null)
            {
                // if there are digital pin states in the sample then use them for populating wht switchStates variable.
                if (dataSample.DigitalMask > 0 && dataSample.Samples.Length >= 2)
                {
                    int digitalReadingMLB = dataSample.Samples[0] * 256 + dataSample.Samples[1];

                    // build the switch states
                    for (int i = 0; i < switchStates.Length; i++)
                    {
                        switchStates[i] = dataSample.IsDigitalPinHigh(xbeeConfiguration[i]) ? StateOFF : StateON;
                    }
                }
            }
        }

        /// <summary>
        /// Builds the state of the switches.
        /// </summary>
        /// <returns></returns>
        public override DeviceState DeviceState
        {
            get
            {
                ComponentState[] states = new ComponentState[switchStates.Length];

                for (int i = 0; i < switchStates.Length; i++)
                {
                    states[i] = new ComponentState()
                    {
                        Name = $"Relay{i}",
                        Value = switchStates[i].ToString()
                    };
                }

                DeviceState state = new DeviceState()
                {
                    Device = this,
                    ComponentStateList = states
                };

                return state;
            }
        }

        #endregion / DeviceBase implementation /

        /// <summary>
        /// This is a method that will be invoked by commands sent to the system.
        /// </summary>
        /// <param name="relayIndex"></param>
        /// <param name="state"></param>
        public void SetRelayState(ushort relayIndex, byte state)
        {
            if (relayIndex >= xbeeConfiguration.Count)
                relayIndex = (byte)(xbeeConfiguration.Count - 1);

            if (state > StateON)
                state = StateON;

            switchStates[relayIndex] = state;

            IXBeeService xbeeService = ((XBeeDeviceNetwork)DeviceNetwork).XBeeService;

            // The relay is on the D1 and D2 pins, set the appropriate state for them
            xbeeService.SendFrame(XBeeFrameBuilder.CreateRemoteATCommand(
                atCommandForPin[xbeeConfiguration[relayIndex]],
                0,
                this.DeviceID,
                this.NetworkAddress,
                new byte[] { ((byte)(state == StateON ? PinLow : PinHigh)) },
                RemoteATCommand.OptionCommitChanges));

            Log.Debug($"[{DeviceID.ToHexString()}] Setting relay {relayIndex} to {state}");
        }
    }
}
