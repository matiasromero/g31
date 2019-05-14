using Newtonsoft.Json;

namespace HomeSwitchHome.Application
{
    public class AppConfiguration
    {
        public AuthenticationConfig Authentication { get; set; }
        public string BaseUrl { get; set; }
        public string ConnectionString { get; set; }
        public string LoggingConnectionString { get; set; }
        public int NHibernateTimeout { get; set; }
        public string FileStorageBasePath { get; set; }
        public bool RecreateDatabase { get; set; }
        public int ThumbnailMaxWidth { get; set; }
        public int ThumbnailMaxHeight { get; set; }
    }

    [JsonObject("tokenManagement")]
    public class AuthenticationConfig
    {
        [JsonProperty("secret")] public string JWT_SECRET_KEY { get; set; }

        [JsonProperty("audience")] public string JWT_AUDIENCE_TOKEN { get; set; }

        [JsonProperty("issuer")] public string JWT_ISSUER_TOKEN { get; set; }

        [JsonProperty("accessExpiration")] public int JWT_EXPIRE_MINUTES { get; set; }

        [JsonProperty("refreshExpiration")] public int JWT_REFRESH_TOKEN_EXPIRE_IN_DAYS { get; set; }
    }
}