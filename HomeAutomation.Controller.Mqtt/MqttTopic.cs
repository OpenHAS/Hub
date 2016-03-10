using System;

namespace HomeAutomation.Controller.Mqtt
{
    public static class MqttTopic
    {
        public static readonly string StatusTopic = "/Status";

        public static readonly string LogTopic = "/Log";

        public static readonly string ErrorTopic = "/Error";

        public static readonly string CommandTopic = "/Command";

        public static readonly string Heartbeat = "/Heartbeat";

        public static readonly string Admin = "/Admin";

        public static readonly string Statistics = "/Statistics";
    }
}
