using System;
using System.Collections.Generic;
using HomeAutomation.Application;
using HomeAutomation.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;
using HomeAutomation.Core.Diagnostics;
using HomeAutomation.DeviceNetwork.XBee;
using HomeAutomation.Controller.Mqtt;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HomeAutomation.Gateway.App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, IController
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.Loaded += MainPage_Loaded;
        }

        public void Initialize(ControllerHost controllerHost)
        {
            controllerHost.OnDeviceNetworkDiagnosticsReceived += ControllerHost_OnDeviceNetworkDiagnosticsReceived;
            controllerHost.OnControllerDiagnosticsReceived += ControllerHost_OnControllerDiagnosticsReceived;
            controllerHost.OnStatisticsReceived += ControllerHost_OnStatisticsReceived;
        }

        private void ControllerHost_OnStatisticsReceived(object sender, StatisticsEventArgs e)
        {
            var task = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                systemTime.Text = e.Statistics.CurrentTime.ToString("HH:mm:ss");
            });
        }

        private void ControllerHost_OnControllerDiagnosticsReceived(object sender, Core.Controller.ControllerDiagnosticsEventArgs e)
        {
            MqttControllerDiagnostics diagnostics = e.DiagnosticsObject as MqttControllerDiagnostics;
            if (diagnostics != null)
            {
                var task = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    mqttConnected.Text = diagnostics.IsMqttClientConnected ? "Connected" : "Not Connected";
                    receivedMqttMessageCount.Text = diagnostics.ReceivedMessageCount.ToString();
                    sentMqttMessageCount.Text = diagnostics.SentMessageCount.ToString();
                    droppedMqttMessageCount.Text = diagnostics.DroppedMessageCount.ToString();
                });
            }
        }

        private void ControllerHost_OnDeviceNetworkDiagnosticsReceived(object sender, DeviceNetworkDiagnosticsEventArgs e)
        {
            XBeeNetworkDiagnostics xbeeDiagnostics = e.DiagnosticsObject as XBeeNetworkDiagnostics;
            if (xbeeDiagnostics != null)
            {
                var task = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    receivedXBeeFrameCount.Text = xbeeDiagnostics.XBeeMessageReceiveCount.ToString();
                    sentXBeeFrameCount.Text = xbeeDiagnostics.XBeeMessageSentCount.ToString();

                    xbeeSerialPortConnected.Text = xbeeDiagnostics.IsSerialPortConnected ? "Connected" : "Not Connected";
                });
            }

        }

        private void MainPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            MainApplication.Initialize("Config/HomeAutomationConfiguration.json");

            HomeAutomationSystem.ControllerRegistry.RegisterController(this);

            MainApplication.Run();
        }

        public object GetUpdatedDiagnostics()
        {
            // this is a TV display, we are not having any diagnostics object.
            return null;
        }
    }
}
