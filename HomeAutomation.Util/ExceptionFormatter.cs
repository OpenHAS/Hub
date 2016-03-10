using System;
using System.Text;

namespace HomeAutomation.Util
{
    public static class ExceptionFormatter
    {
        /// <summary>
        /// Returns the string representation of an exception.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string FormatToLog(this Exception ex)
        {
            if (ex == null)
                return String.Empty;

            StringBuilder builder = new StringBuilder();

            builder.Append(ex.ToString());
            builder.Append("\n");
            builder.Append(ex.Message??String.Empty);
            builder.Append("\n");
            builder.Append("Inner exception: \n");
            builder.Append(ex.InnerException?.FormatToLog());
            builder.Append(ex.StackTrace??String.Empty);

            return builder.ToString();
        }
    }
}
