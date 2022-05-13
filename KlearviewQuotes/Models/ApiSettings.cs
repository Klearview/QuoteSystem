namespace KlearviewQuotes.Models
{
    public class ApiSettings
    {
        public string? AppDataContext { get; set; } // keep in appsettings.json
        public string? AppDataContextCredentials { get; set; } // keep in secrets
        public string? GoogleMapsUrl { get; set; } // keep in appsettings.json
        public string? GoogleMapsKey { get; set; } // keep in secrets
    }
}
