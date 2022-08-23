using System.ComponentModel.DataAnnotations;

namespace KlearviewQuotes.Models
{
    public class Estimate
    {
        [Display(Name = "Exterior")]
        public EstimateExt Exterior { get; set; }

        [Display(Name = "Complete Exterior and Interior")]
        public EstimateExtIn CompleteExtAndInt { get; set; }

        [Display(Name = "Siding Cleaning")]
        public EstimateSiding Siding { get; set; }

        [Display(Name = "Eavestrough / Gutter Clearing")]
        public EstimateEaves Eaves { get; set; }


        public Estimate()
        {
            if (Exterior == null)
                Exterior = new();

            if (CompleteExtAndInt == null)
                CompleteExtAndInt = new();

            if (Siding == null)
                Siding = new();

            if (Eaves == null)
                Eaves = new();
        }
    }

    public class EstimateExt
    {
        [Display(Name = "Exterior Price")]
        public decimal? Price { get; set; }

        [Display(Name = "Customer needs to remove exterior screens")]
        public bool CustomerRemoveExtScreens { get; set; }

        [Display(Name = "If we remove exterior screens, clean and reinstall them")]
        public EstimateWithExtra WeRemoveExtScreens { get; set; }

        [Display(Name = "Requires access to inside")]
        public bool WeRequireAccessInside { get; set; }


        public EstimateExt()
        {
            if (WeRemoveExtScreens == null)
                WeRemoveExtScreens = new();
        }
    }

    public class EstimateExtIn
    {
        [Display(Name = "Exterior and Interior Price")]
        public decimal? Price { get; set; }

        [Display(Name = "Basements")]
        public EstimateWithExtraAndIntExt Basements { get; set; }

        [Display(Name = "Skylights")]
        public EstimateWithExtraAndIntExt Skylights { get; set; }

        [Display(Name = "Deck Glass")]
        public EstimateWithExtra DeckGlass { get; set; }


        public EstimateExtIn()
        {
            if (Basements == null)
                Basements = new();

            if (Skylights == null)
                Skylights = new();

            if (DeckGlass == null)
                DeckGlass = new();
        }
    }

    public class EstimateSiding
    {
        [Display(Name = "Siding Price")]
        public decimal? Price { get; set; }
    }

    public class EstimateEaves
    {
        [Display(Name = "Eaves Price")]
        public decimal? Price { get; set; }
    }

    public class EstimateWithExtra
    {
        public bool Checked { get; set; }
        public decimal? Extra { get; set; }
    }

    public class EstimateWithExtraAndIntExt : EstimateWithExtra
    {
        [Display(Name = "Interior")]
        public bool Interior { get; set; }

        [Display(Name = "Exterior")]
        public bool Exterior { get; set; }
    }
}
