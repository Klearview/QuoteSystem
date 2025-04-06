using System.ComponentModel.DataAnnotations;

namespace KlearviewQuotes.Models.Clients
{
    public class Agreement
    {
        [Key]
        public string? AgreementId { get; set; }

        public string? AccountId { get; set; }
        public string? ServiceLocationId { get; set; }
        public string? BillingLocationId { get; set; }
        public string? BusinessUnitId { get; set; }
        public string? DefaultServiceContactId { get; set; }
        public string? DefaultBillingContactId { get; set; }
        public string? Name { get; set; }
        public string? Issue { get; set; }
        public string? CancelDate { get; set; }
        public string? CancelReason { get; set; }
        public string? CompletedOn { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? ContractName { get; set; }
        public string? ContractPeriodAmount { get; set; }
        public string? ContractChargeForProducts { get; set; }
        public string? ContractChargeForServices { get; set; }
        public string? ContractProrated { get; set; }
        public string? ContractAmountTaxable { get; set; }
        public string? InvoiceInitialStartDate { get; set; }
        public string? InvoiceInitialEndDate { get; set; }
        public string? InvoiceSchedulePeriod { get; set; }
        public string? InvoiceScheduleDelay { get; set; }
        public string? UpfrontInvoicingDaysAhead { get; set; }
        public string? UpfrontInvoicingTimeToCreateInvoice { get; set; }
        public string? CampaignId { get; set; }
        public string? CancelReasonOther { get; set; }
        public string? IsRenewal { get; set; }
        public string? RenewalDate { get; set; }
        public string? RenewalAgreementId { get; set; }
        public string? RenewalStatus { get; set; }
        public string? InvoiceMode { get; set; }

        public virtual ServiceLocation ServiceLocation { get; set; }
        public virtual Account Account { get; set; }
    }
}
