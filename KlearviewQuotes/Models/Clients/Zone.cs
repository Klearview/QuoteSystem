using System.ComponentModel.DataAnnotations;

namespace KlearviewQuotes.Models.Clients
{
    public class Zone
    {
        [Key]
        public string? Id { get; set; }

        public string? BusinessUnitId { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
        public string? Active { get; set; }

        public virtual IList<ServiceLocation> ServiceLocations { get; set; }
    }
}
