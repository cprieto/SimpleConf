using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace SimpleConf
{
    public class AppSettingsConfigurationSource : IConfigurationSource
    {
        public IDictionary<string, string> GetValues()
        {
            var settings = ConfigurationManager.AppSettings;
            return settings.Keys.Cast<string>()
                .ToDictionary(key => key, key => settings[key]);
        }
    }
}