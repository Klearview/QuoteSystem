﻿using System.ComponentModel.DataAnnotations;

namespace QuoteSystem.Models
{
    public class QuoteInfo
    {

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime? DateOfEstimate { get; set; }

        [Display(Name = "Sales Rep")]
        public string? SalesRep { get; set; }

        [Display(Name = "Job Description")]
        public string? Description { get; set; }

        [Display(Name = "Notes (private)")]
        public string? Notes { get; set; }

    }
}
