using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace HomeAutomation.Communication.Mqtt
{
    public interface IMqttClient
    {
        event Action<object, EventArgs> ConnectionClosed;
        event Action<object, MqttMessage> MqttMsgPublishReceived;

        bool IsConnected { get; }  

        void Publish(string topic, string message);

        void Subscribe(string[] v1, byte[] v2);

        void Connect(string clientName, string username, string password);
    }
}
