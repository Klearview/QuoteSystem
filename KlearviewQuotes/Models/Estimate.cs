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

        [Display(Name = "Eavestrough/Gutter Clearing")]
        public EstimateEaves Eaves { get; set; }


        public Estimate()
        {
            Exterior = new();
            CompleteExtAndInt = new();
            Siding = new();
            Eaves = new();
        }
    }

    public class EstimateExt
    {
        [Display(Name = "Exterior Price")]
        public decimal? Price { get; set; }
    }

    public class EstimateExtIn
    {

    }

    public class EstimateSiding
    {

    }

    public class EstimateEaves
    {

    }
}
