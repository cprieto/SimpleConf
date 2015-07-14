using System.Linq;
using System.Collections.Generic;
using System.Configuration;

namespace configurator.autofac
{
    public class AppSettingsConfigurationSourceProvider : IConfigurationSourceProvider
    {
        public IDictionary<string, string> GetValues()
        {
            var settings = ConfigurationManager.AppSettings;
            return settings.Keys.Cast<string>()
                .ToDictionary(key => key, key => settings[key]);
        }
    }
}