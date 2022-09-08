using System.Net;
using System.Net.Mail;

namespace KlearviewQuotes.Models
{
    public class ApiSettings
    {
        public string? BaseUrl { get; set; }

        public string? AppDataContext { get; set; } // keep in appsettings.json
        public string? AppDataContextCredentials { get; set; } // keep in secrets
        public string? GoogleMapsUrl { get; set; } // keep in appsettings.json
        public string? GoogleMapsKey { get; set; } // keep in secrets

        public ApiSettingsEmail? Email { get; set; } // keep in appsettings.json
    }

    public class ApiSettingsEmail
    {
        public ApiSettingsSender? Sender { get; set; } // keep in appsettings.json
        public string? Host { get; set; } // keep in appsettings.json
        public int? Port { get; set; } // keep in appsettings.json
    }

    public class ApiSettingsSender
    {
        public string? Address { get; set; } // keep in secrets
        public string? DisplayName { get; set; } // keep in appsettings.json
        public string? Password { get; set; } // keep in secrets

        public MailAddress? MailAddress { 
            get
            {
                if (Address == null || DisplayName == null)
                    return null;

                return new MailAddress(Address, DisplayName);
            } 
        }

        public NetworkCredential? NetworkCredential
        {
            get
            {
                if (Address == null || Password == null)
                    return null;

                return new(Address, Password);
            }
        }
    }
}
