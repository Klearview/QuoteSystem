using QuoteSystem.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KlearviewQuotes.Models
{
    public class Quote
    {
        [Key]
        [Required]
        public long? Id { get; set; }


        #region Tracking
        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "Created On")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Created By")]
        public string? CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "Updated On")]
        public DateTime? UpdatedAt { get; set; }

        [Display(Name = "Updated By")]
        public string? UpdatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "Sent On")]
        public DateTime? SentAt { get; set; }

        [Display(Name = "Sent By")]
        public string? SentBy { get; set; }
        #endregion

        #region Quote Data
        [NotMapped]
        public Estimate Estimate { get; set; }

        [NotMapped]
        public CustomerInfo CustomerInfo { get; set; }

        [NotMapped]
        public QuoteInfo QuoteInfo { get; set; }
        #endregion


        public Quote()
        {
            Estimate = new Estimate();
            CustomerInfo = new CustomerInfo();
            QuoteInfo = new QuoteInfo();

            CreatedAt = DateTime.Now;
        }

        #region Quote Data XML
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

        public byte[] QuoteInfoXML
        {
            get
            {
                return XMLSerializer.SerializeToXml(QuoteInfo);
            }
            set
            {
                QuoteInfo = XMLSerializer.DeserializeFromXML<QuoteInfo>(value);
            }
        }
        #endregion
    }
}
