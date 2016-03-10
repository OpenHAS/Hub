namespace HomeAutomation.Communication.Mqtt
{
    public static class MqttStatistics
    {
        public static uint SentMessageCount { get; set; }

        public static uint ReceivedMessageCount { get; set; }

        public static uint LostMessageCount { get; set; }
    }
}
