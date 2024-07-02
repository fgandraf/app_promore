namespace Promore.Core;

public static class Configuration
{
    public static SecretsConfiguration Secrets { get; set; } = new();
    public static DatabaseConfiguration Database { get; set; } = new();
    public static bool IsMockDataBase { get; set; } = false;

    public const int DefaultPageSize = 25;
    public const int DefaultPageNumber = 1;
    public const int DefaultStatusCode = 200;
    
    
    
    public class DatabaseConfiguration
    {
        public string ConnectionString { get; set; } = string.Empty;
    }
    
    public class SecretsConfiguration
    {
        public string JwtPrivateKey { get; set; } = string.Empty;
    }
}