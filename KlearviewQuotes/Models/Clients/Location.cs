using System.ComponentModel.DataAnnotations;

namespace KlearviewQuotes.Models.Clients
{
    public class Location
    {
        [Display(Name = "Street 1")]
        public string? Street1 { get; set; }

        [Display(Name = "Street 2")]
        public string? Street2 { get; set; }

        public string? City { get; set; }

        [Display(Name = "Provice/State")]
        public string? State { get; set; }

        [Display(Name = "Provice/State Abbr.")]
        public string? StateAbbreviation { get; set; }

        [Display(Name = "Postal/Zip Code")]
        public string? PostalCode { get; set; }

        public string? Country { get; set; }

        [Display(Name = "Country Code")]
        public string? CountryCode { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? County { get; set; }

        [Display(Name = "Address")]
        public string Address
        {
            get
            {
                return $"{Street1}{(string.IsNullOrEmpty(Street2) ? "" : $"\n{Street2}")}" +
                        $"\n{City} {StateAbbreviation} {PostalCode}" +
                        $"{(Country == "Canada" ? "" : $"\n{Country}")}";
            }
        }
    }
}
