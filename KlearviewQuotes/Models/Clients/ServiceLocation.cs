using System.ComponentModel.DataAnnotations;

namespace KlearviewQuotes.Models.Clients
{
    public class ServiceLocation
    {
        [Key]
        public string? ServiceLocationId { get; set; }

        public string? AccountId { get; set; }
        public string? BusinessUnitId { get; set; }
        public string? DefaultContactId { get; set; }
        public string? Name { get; set; }
        public string? Number { get; set; }
        public string? Active { get; set; }
        public string? TaxRate { get; set; }
        public string? Street1 { get; set; }
        public string? Street2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? StateAbbreviation { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? CountryCode { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? County { get; set; }
        public string? TimeZoneId { get; set; }
        public string? ZoneId { get; set; }
        public string? ServiceMemo { get; set; }

        public virtual Account Account { get; set; }

        public string Address { get
            {
                return $"{Street1}{(string.IsNullOrEmpty(Street2) ? "" : $"\n{Street2}")}" +
                        $"\n{City} {StateAbbreviation} {PostalCode}" +
                        $"{(Country == "Canada" ? "" : $"\n{Country}")}";
            }
        }
    }
}
