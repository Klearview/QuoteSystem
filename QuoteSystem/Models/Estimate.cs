using System.ComponentModel.DataAnnotations;

namespace QuoteSystem.Models
{
    public class Estimate
    {
        public Exterior Exterior { get; set; }
        public ExteriorAndInterior ExteriorAndInterior { get; set; }

        [Display(Name = "Siding Price")]
        public decimal? SidingPrice { get; set; }

        [Display(Name = "Evestrough Price")]
        public decimal? EvestroughPrice { get; set; }

        
        public Estimate()
        {
            Exterior = new Exterior();
            ExteriorAndInterior = new ExteriorAndInterior();
        }
    }

    public class Exterior
    {
        [Display(Name = "Exterior Price")]
        [DataType(DataType.Currency)]
        public decimal? ExteriorPrice { get; set; }

        [Display(Name = "Screens Price")]
        public decimal? ScreensPrice { get; set; }

        [Display(Name = "Interior of high windows only Price")]
        public decimal? HighWindowsPrice { get; set; }
    }

    public class ExteriorAndInterior
    {
        [Display(Name = "Exterior and Interior Price")]
        public decimal? Total { get; set; }
        public Basements Basements { get; set; }
        public Skylights Skylights { get; set; }

        [Display(Name = "Deck Glass Price")]
        public decimal? DeckGlassPrice { get; set; }

        public ExteriorAndInterior()
        {
            Basements = new Basements();
            Skylights = new Skylights();
        }
    }

    public class Basements
    {
        [Display(Name = "Walkout Only")]
        public bool WalkoutOnly { get; set; } = false;

        [Display(Name = "Basements")]
        public decimal? Price { get; set; }
    }

    public class Skylights
    {
        [Display(Name = "Skylights")]
        public decimal? Price { get; set; }

        [Display(Name = "In/Out")]
        public bool InAndOut { get; set; } = false;

        [Display(Name = "Out")]
        public bool Out { get; set; } = false; 
    }
}
