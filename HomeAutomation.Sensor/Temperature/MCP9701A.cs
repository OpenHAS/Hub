namespace HomeAutomation.Sensor.Temperature
{
    public class MCP9701A
    {
        private const double BaseLineMilliVolts = 400.0;

        public static double TemperatureFromVoltage(double voltage)
        {
            return (voltage - BaseLineMilliVolts) / 19.5;
        }
    }
}
