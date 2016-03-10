using System.Runtime.Serialization;

namespace HomeAutomation.Communication.Mqtt
{
    /// <summary>
    /// Contains configuration about the MQTT server.
    /// </summary>
    [DataContract]
    public class MqttServerConfiguration
    {
        /// <summary>
        /// Can contain IP address or host name.
        /// </summary>
        [DataMember]
        public string ServerHostName { get; set; }

        /// <summary>
        /// The number of seconds the keep alive loop should wait between keep alive checks.
        /// </summary>
        [DataMember]
        public int KeepAliveCheckPeriodInSeconds { get; set; }

        /// <summary>
        /// The client name how this application presents itself to the MQTT server.
        /// </summary>
        [DataMember]
        public string ClientName { get; set; }

        /// <summary>
        /// The root of the topic on which messages will be published and received.
        /// </summary>
        [DataMember]
        public string TopicRootName { get; set; }

        /// <summary>
        /// The username to use to connect.
        /// </summary>
        [DataMember]
        public string Username { get; set; }

        /// <summary>
        /// The password to use to connect.
        /// </summary>
        [DataMember]
        public string Password { get; set; }
    }
}
