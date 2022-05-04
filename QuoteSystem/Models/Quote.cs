using QuoteSystem.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuoteSystem.Models
{
    public class Quote
    {
        [Key]
        [Required]
        [Display(Name = "Quote #")]
        [Column("id")]  
        public long QuoteNumber { get; set; }

        [Required]
        [Column("accepted")]
        public bool Accepted { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        [Column("date")]
        public DateTime Date { get; set; }

        [Display(Name = "Sales Rep")]
        [Column("sales_rep")]
        public string? SalesRep { get; set; }

        [Display(Name = "Job Description")]
        [Column("description")]
        public string? Description { get; set; }

        [Display(Name = "Notes (private)")]
        [Column("notes")]
        public string? Notes { get; set; }

        [Column("estimate")]
        public byte[] EstimateXML {
            get
            {
                return XMLSerializer.SerializeToXml(Estimate);
            }
            set
            {
                Estimate = XMLSerializer.DeserializeFromXML<Estimate>(value);
            }
        }

        [Column("customer_info")]
        public byte[] CustomerInfoXML
        {
            get
            {
                return XMLSerializer.SerializeToXml(CustomerInfo);
            }
            set
            {
                CustomerInfo = XMLSerializer.DeserializeFromXML<CustomerInfo>(value);
            }
        }

        [NotMapped]
        public Estimate Estimate { get; set; }

        [NotMapped]
        public CustomerInfo CustomerInfo { get; set; }


        public Quote()
        {
            Estimate = new Estimate();
            CustomerInfo = new CustomerInfo();
        }
    }
}
