using System.Collections.Generic;

namespace configurator.autofac
{
    public interface IConfigurationSource
    {
        IDictionary<string, string> GetValues();
    }
}