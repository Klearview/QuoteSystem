using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace KlearviewQuotes.Models.Clients
{
    public class Invoice
    {
        public string? InvoiceId { get; set; }
        public string? InvoiceNum { get; set; }

        [Display(Name = "Invoice Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? InvoiceDate { get; set; }
        public string? AccountId { get; set; }
        public string? SubTotal { get; set; }
        public string? Total { get; set; }
        public string? Tax { get; set; }
        public string? Balance { get; set; }
        public string? BillingStreet { get; set; }
        public string? BillingStreet2 { get; set; }
        public string? BillingCity { get; set; }
        public string? BillingState { get; set; }
        public string? BillingPostalCode { get; set; }
        public string? BillingCountry { get; set; }
        public string? BillingEmailAddress { get; set; }
        public string? BillingLatitude { get; set; }
        public string? BillingLongitude { get; set; }
        public string? LocationStreet { get; set; }
        public string? LocationStreet2 { get; set; }
        public string? LocationCity { get; set; }
        public string? LocationState { get; set; }
        public string? LocationPostalCode { get; set; }
        public string? LocationCountry { get; set; }
        public string? LocationLatitude { get; set; }
        public string? LocationLongitude { get; set; }
        public string? Void { get; set; }
        public string? VoidReason { get; set; }
        public string? VoidRefund { get; set; }
        public string? VoidRefundMethod { get; set; }
        public string? AgreementId { get; set; }
        public string? InvoiceStart { get; set; }
        public string? InvoiceEnd { get; set; }
        public string? ContractName { get; set; }
        public string? ContractPeriod { get; set; }
        public string? ContractPeriodicAmount { get; set; }
        public string? BusinessUnitId { get; set; }
        public string? BillingLocationBillingMemo { get; set; }
        public string? VoidDate { get; set; }
        public string? CampaignId { get; set; }
        public string? ReissuedFromInvoiceId { get; set; }
        public string? ReissuedToInvoiceId { get; set; }

        public virtual Account Account { get; set; }

        [Display(Name = "Billing Address")]
        public string BillingAddress
        {
            get
            {
                return $"{BillingStreet} {(string.IsNullOrEmpty(BillingStreet2) ? "" : $"\n{BillingStreet2}")} " +
                        $"\n{BillingCity} {BillingState} {BillingPostalCode} " +
                        $"{(BillingCountry == "Canada" ? "" : $"\n{BillingCountry}")} ";
            }
        }

        [Display(Name = "Location Address")]
        public string LocationAddress
        {
            get
            {
                return $"{LocationStreet} {(string.IsNullOrEmpty(LocationStreet2) ? "" : $"\n{LocationStreet2}")} " +
                        $"\n{LocationCity} {LocationState} {LocationPostalCode} " +
                        $"{(LocationCountry == "Canada" ? "" : $"\n{LocationCountry}")} ";
            }
        }
    }
}
