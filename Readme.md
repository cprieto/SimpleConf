# SimpleConf
### Configuration made simple!
[![Build status](https://ci.appveyor.com/api/projects/status/kyov4ki5guvt0qid?svg=true)](https://ci.appveyor.com/project/CristianPrieto/configurator) 

How many times do you have to write a line of code like this?
```csharp
var portString = 
	Environment.GetEnvironmentVariable("port");
	
portString = string.IsNullOrEmpty(portString) 
	? ConfigurationManager.AppSettings["port"] 
	: portString;

int port;
if (!Int32.TryParse(portString, out port)) {
	throw new Exception("No port, no bananas!");
}
```

_(Well, maybe not many times but hey, I hope you got the point!)_


What if I can use something as simple as this:

```csharp
public interface ISampleConfiguration {
    int Port { get; }
}

// yada yada yada

var configuration = 
    new ConfigurationBuilder<ISampleConfiguration>()
	    .FromAppSettings()
	    .FromEnvironment()
	    .Build();
```

Now you can set the value in your AppSettings section in your configuration file or setting the value in your environment variables.

## Prefixed values

Given a simple configuration setting like this:
```xml
<configuration>
	<appSettings>
		<add key="logging:key" value="abcd12345" />
		<add key="server-url" value="http://example.org" />
		<add key="server" value="http://boo.org" />
	</appSettings>
</configuration>
```

We can partition configuration in three different configuration interfaces:
```csharp
public interface ILoggingConfiguration {
	string Key { get; }
}

public interface IServerConfiguration {
	string Url { get; }
}

public interface IConfiguration {
	string Url { get; }
}
```
We can grab the configuration in two ways:

 - Using the configuration builder

```csharp
var logCfg = new ConfigurationBuilder<ILoggingConfiguration>().WithPrefix("logging").Build();
	
var srvCfg = new ConfigurationBuilder<IServerConfiguration>().WithPrefix("server", "-").Build();

var cfg = new ConfigurationBuilder<IConfiguration>().Build();
```

- Using attributes

```csharp
[KeyPrefix("logging:")]
public interface ILoggingConfiguration {
	string Key { get; }
}

[KeyPrefix("server-")]
public interface IServerConfiguration {
	string Url { get; }
}

public interface IConfiguration {
	string Url { get; }
}
```

## Default values

We can provide default values using the [```DefaultValue```](https://msdn.microsoft.com/en-us/library/system.componentmodel.defaultvalueattribute%28v=vs.110%29.aspx) Attribute. Right now, if a value is not present in any of the sources it will throw an exception in the moment the configuration class is build, _except if you have a default value attribute applied to the property_.

## Type conversion

``TypeConverters`` are supported, well, in fact, anything supported by ``Castle.DynamicDictionary`` is supported too. Just decorate the property with a ``TypeConverter`` attribute providing your [custom TypeConverter](https://msdn.microsoft.com/en-us/library/ayybcxe5.aspx) and that's all, happiness is guaranteed. 


Heavily inspired in [this blog post](http://kozmic.net/2014/03/22/strongly-typed-app-settings-with-castle-dictionaryadapter/) from [Krzysztof Koźmic](https://twitter.com/kkozmic). Thanks mate, you are awesome.