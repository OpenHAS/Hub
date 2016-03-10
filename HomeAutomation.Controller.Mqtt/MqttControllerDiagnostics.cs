namespace HomeAutomation.Controller.Mqtt
{
    public class MqttControllerDiagnostics
    {
        public bool IsMqttClientConnected { get; set; }

        public uint SentMessageCount { get; set; }

        public uint ReceivedMessageCount { get; set; }

        public uint DroppedMessageCount { get; set; }
    }
}
