namespace FRCScouting_API.Models
{
    public class ApiSettings
    {
        public string? AppDataContext { get; set; } // keep in appsettings.json
        public string? AppDataContextCredentials { get; set; } // keep in secrets
    }
}
