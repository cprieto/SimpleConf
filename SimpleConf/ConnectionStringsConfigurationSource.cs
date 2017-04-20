using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace SimpleConf
{
    public class ConnectionStringsConfigurationSource : IConfigurationSource
    {
        public IDictionary<string, string> GetValues()
        {
            var connectionStrings = ConfigurationManager.ConnectionStrings;
            var values = connectionStrings.Cast<ConnectionStringSettings>()
                .ToDictionary(cs => cs.Name, cs => cs.ConnectionString);

            return values;
        }
    }
}