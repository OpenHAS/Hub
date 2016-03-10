using System;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace HomeAutomation.Communication.Mqtt
{
    public class MqttClientWrapper : IMqttClient
    {
        MqttClient mqttClient;

        public MqttClientWrapper(string brokerHostName)
        {
            mqttClient = new MqttClient(brokerHostName);

            mqttClient.ConnectionClosed += MqttClient_ConnectionClosed;
            mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
        }
        public event Action<object, EventArgs> ConnectionClosed;
        public event Action<object, MqttMessage> MqttMsgPublishReceived;

        public bool IsConnected
        {
            get
            {
                return mqttClient.IsConnected;
            }
        }        

        public void Connect(string clientName, string username, string password)
        {
            if (String.IsNullOrEmpty(username))
            {
                mqttClient.Connect(clientName);
            }
            else
            {
                mqttClient.Connect(clientName, username, password);
            }
        }

        public void Publish(string topic, string message)
        {
            mqttClient.Publish(topic, Encoding.UTF8.GetBytes(message));
        }

        public void Subscribe(string[] topics, byte[] qosLevels)
        {
            mqttClient.Subscribe(topics, qosLevels);
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            MqttMessage m = new MqttMessage()
            {
                TopicName = e.Topic,
                Message = new String(Encoding.UTF8.GetChars(e.Message))
            };

            MqttMsgPublishReceived?.Invoke(sender, m);
        }

        private void MqttClient_ConnectionClosed(object sender, EventArgs e)
        {
            ConnectionClosed?.Invoke(sender, e);
        } 
    }
}
