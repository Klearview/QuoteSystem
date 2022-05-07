using System.ComponentModel.DataAnnotations;

namespace QuoteSystem.Models
{
    public class Estimate
    {
        public EstimateExt Exterior { get; set; }
        public EstimateExtIn CompleteExtAndInt { get; set; }
        public EstimateSiding Siding { get; set; }
        public EstimateEaves Eaves { get; set; }
    }

    public class EstimateExt
    {

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
