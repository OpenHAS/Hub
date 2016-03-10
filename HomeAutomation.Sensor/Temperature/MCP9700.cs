namespace HomeAutomation.Sensor.Temperature
{
    public class MCP9700
    {
        private const double BaseLineMilliVolts = 500.0;

        public static double TemperatureFromVoltage(double voltage)
        {
            return (voltage - BaseLineMilliVolts) / 10.0;
        }
    }
}
