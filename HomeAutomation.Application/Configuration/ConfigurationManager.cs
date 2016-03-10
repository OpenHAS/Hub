using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace HomeAutomation.Application.Configuration
{
    public class ConfigurationManager
    {
        public T LoadFile<T>(string fileName)
            where T : new()
        {
            T returnResult = default(T);

            string processedFileName = ProcessConfigFile(fileName);

            using (FileStream fs = File.Open(processedFileName, FileMode.Open,FileAccess.Read,FileShare.Read))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                
                returnResult = (T)ser.ReadObject(fs);
            }

            return returnResult;
        }

        private string ProcessConfigFile(string fileName)
        {
            string newFileName = Path.Combine(Path.GetTempPath(), "HomeAutomationConfiguration.json.ha");

            string sourceConfig = File.ReadAllText(fileName);
            sourceConfig = sourceConfig.Replace("\r", String.Empty);

            File.WriteAllText(newFileName, sourceConfig);

            return newFileName;
        }
    }
}
