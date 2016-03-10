using HomeAutomation.Communication.Mqtt;
using HomeAutomation.Core;
using HomeAutomation.Core.Controller;
using System;
using System.Collections.Generic;

namespace HomeAutomation.Controller.Mqtt
{
    public class MqttControllerFactory : IControllerFactory
    {
        public IController CreateController(Dictionary<string, string> configuration)
        {
            IMqttClient client = new MqttClientWrapper((string)configuration["ServerHostName"]);

            MqttServerConfiguration mqttConfig = new MqttServerConfiguration()
            {
                ClientName = configuration["ClientName"],
                ServerHostName = configuration["ServerHostName"],
                KeepAliveCheckPeriodInSeconds = Int32.Parse(configuration["KeepAliveCheckPeriodInSeconds"]),
                TopicRootName = configuration["TopicRootName"],
                Username = configuration["Username"],
                Password = configuration["Password"]
            };

            MqttService service = new MqttService(mqttConfig, client);
            HomeAutomationSystem.ScheduledTasks.ScheduleRealtimeTask(service);

            MqttController controller = new MqttController(service);

            return controller;
        }
    }
}
