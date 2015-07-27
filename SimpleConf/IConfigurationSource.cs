using System.Collections.Generic;

namespace SimpleConf
{
    public interface IConfigurationSource
    {
        IDictionary<string, string> GetValues();
    }
}