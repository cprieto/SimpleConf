using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace SimpleConf
{
    public class ConnectionStringsConfigurationSource : IConfigurationSource
    {
        private readonly string _prefix;
        private readonly string _separator;

        public ConnectionStringsConfigurationSource(): this(string.Empty, string.Empty)
        {
        }

        public ConnectionStringsConfigurationSource(string prefix, string separator)
        {
            _prefix = prefix;
            _separator = separator;
        }

        public IDictionary<string, string> GetValues()
        {
            var connectionStrings = ConfigurationManager.ConnectionStrings;
            var values = connectionStrings.Cast<ConnectionStringSettings>()
                .ToDictionary(cs => $"{_prefix}{_separator}{cs.Name}", cs => cs.ConnectionString);

            return values;
        }
    }
}