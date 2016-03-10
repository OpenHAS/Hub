using System;
using HomeAutomation.Core;
using HomeAutomation.Communication.Mqtt;
using System.Collections.Generic;
using HomeAutomation.Core.Diagnostics;
using HomeAutomation.Core.Account;

namespace HomeAutomation.Controller.Mqtt
{
    public class MqttController : IController
    {
        MqttService mqttService;
        ControllerHost controllerHost;

        public MqttController(MqttService mqttService)
        {
            this.mqttService = mqttService;

            // subscribe to the messages coming from that network.
            mqttService.MessageReceived += MqttService_MessageReceived;

            // signal that we are interested on the following topics
            mqttService.SubscribeTopic(MqttTopic.CommandTopic);
        }

        public void Initialize(ControllerHost controllerHost)
        {
            this.controllerHost = controllerHost;

            controllerHost.OnDeviceStateReceived += ControllerHost_OnDeviceStateReceived;
        }

        private void ControllerHost_OnDeviceStateReceived(object sender, DeviceStateEventArgs e)
        {
            // append the username to the status topic
            string topicName = $"{mqttService.GetFullTopicName(MqttTopic.StatusTopic)}/{AccountInfo.Username}";

            mqttService.SendMessage(topicName, e.DeviceState.ConvertToString());
        }

        private void MqttService_MessageReceived(object sender, MqttMessage e)
        {
            // if the message was coming through the / Admin topic then execute it with the AdminCommandDistributor
            if (e.TopicName == MqttTopic.Admin)
            {
                // todo
            }

            // if the message arrived through the /Command topic then send the command directly to the device that was targeted
            if (e.TopicName == MqttTopic.CommandTopic)
            {
                DeviceCommand command = DeviceCommand.CreateFromString(e.Message);

                controllerHost.ExecuteDeviceCommand(command);
            }
        }

        public object GetUpdatedDiagnostics()
        {
            return new MqttControllerDiagnostics()
            {
                IsMqttClientConnected = mqttService.IsMqttConnected,
                ReceivedMessageCount = MqttStatistics.ReceivedMessageCount,
                SentMessageCount = MqttStatistics.SentMessageCount,
                DroppedMessageCount = MqttStatistics.LostMessageCount
            };
        }
    }
}
