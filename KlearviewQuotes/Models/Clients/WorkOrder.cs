using System.ComponentModel.DataAnnotations;

namespace KlearviewQuotes.Models.Clients
{
    public class WorkOrder
    {
        public string? WorkOrderId { get; set; }
        public string? AccountId { get; set; }
        public string? ServiceLocationId { get; set; }
        public string? BillingLocationId { get; set; }
        public string? AgreementId { get; set; }
        public string? BusinessUnitId { get; set; }
        public int? Number { get; set; }
        public string? Issue { get; set; }
        public string? DurationAdjustment { get; set; }
        public string? ExpectedDuration { get; set; }
        public string? ServiceSubTotal { get; set; }
        public string? ProductSubTotal { get; set; }

        [Display(Name = "Sub Total")]
        public decimal? SubTotal { get; set; } //
        public string? ResourceId { get; set; } //
        public string? CommittedResourceId { get; set; } //
        public string? EstimatedStartTime { get; set; }
        public string? EstimatedTimeOfArrival { get; set; }
        public string? EstimatedCompletedTime { get; set; }
        public string? DepartureTime { get; set; }
        public string? ActualStartTime { get; set; }
        public string? ActualCompletedTime { get; set; }
        public string? StartOfCommitmentWindow { get; set; }
        public string? EndOfCommitmentWindow { get; set; }
        public string? StartingEligibleDate { get; set; }
        public string? EndingEligibleDate { get; set; }
        public string? StartTimePreference { get; set; }
        public string? EndTimePreference { get; set; }
        public string? PreferredResourceId { get; set; }
        public string? DayOfWeekPreference { get; set; }
        public string? StartedBy { get; set; }
        public string? CompletedBy { get; set; }
        public string? UnassignedResourceId { get; set; }
        public string? Status { get; set; } //
        public string? Closed { get; set; } //

        [Display(Name = "Scheduled Date")]
        public string? ScheduledDate { get; set; } //
        public string? ServiceComment { get; set; }
        public string? TechComment { get; set; }
        public string? CancelledDate { get; set; } //
        public string? CancelledTime { get; set; } //
        public string? IssueNote { get; set; }
        public string? PerformedByResourceId { get; set; } //

        [Display(Name = "Service Memo")]
        public string? ServiceLocationServiceMemo { get; set; } //

        [Display(Name = "Billing Memo")]
        public string? BillingLocationBillingMemo { get; set; } //
        public string? CampaignId { get; set; } //
        public string? CancelReason { get; set; }
        public string? CancelReasonOther { get; set; }

        public virtual Account? Account { get; set; }
        public virtual ServiceLocation? ServiceLocation { get; set; }
        public virtual BillingLocation? BillingLocation { get; set; }

        [Display(Name = "Resource")]
        public virtual Team? Resource { get; set; }

        [Display(Name = "Committed Resource")]
        public virtual Team? CommittedResource { get; set; }

        [Display(Name = "Performed by Resource")]
        public virtual Team? PerformedByResource { get; set; }

        public virtual Campaign? Campaign { get; set; }
    }
}
