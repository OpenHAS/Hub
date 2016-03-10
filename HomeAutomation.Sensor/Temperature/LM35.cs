namespace HomeAutomation.Sensor.Temperature
{
    /// <summary>
    /// Provides functionality to calculate temperature based on voltage for the LM35 device.
    /// Device datasheet: http://www.ti.com/lit/ds/symlink/lm35.pdf
    /// </summary>
    public static class LM35
    {
        public static double TemperatureFromVoltage(double voltage)
        {
            return voltage / 10.0;
        }
    }
}
