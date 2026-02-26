namespace Core
{
    public class Logging
    {
        public LogLevelConfig LogLevel { get; set; }
    }

    public class LogLevelConfig
    {
        public string Default { get; set; }
        public string MicrosoftAspNetCore { get; set; }
    }

    public class ConnectionStrings
    {
        public string Application { get; set; }
    }

    public class ServiceConfig
    {
        public string Host { get; set; }
        public string ConsulHost { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public List<string> Tags { get; set; }
    }

    public class JwtConfig
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public int ExpireMinutes { get; set; }
    }

    public class RedisConfig
    {
        public string Configuration { get; set; }
        public string InstanceName { get; set; }
    }

    public class AppSettings
    {
        public Logging Logging { get; set; }
        public bool EnableRequestLogging { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public Configuration Configuration { get; set; }
    }

    public class Configuration
    {
        public ServiceConfig Service { get; set; }
        public JwtConfig Jwt { get; set; }
        public RedisConfig Redis { get; set; }
    }
}
