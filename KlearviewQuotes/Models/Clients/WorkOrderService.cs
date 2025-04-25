namespace KlearviewQuotes.Models.Clients
{
    public class WorkOrderService
    {
        public string? Id { get; set; }
        public string? AgreementServiceId { get; set; }
        public string? WorkOrderId { get; set; }
        public string? ServiceId { get; set; }
        public string? Description { get; set; }
        public string? BillingType { get; set; }
        public string? Rate { get; set; }
        public string? DoNotChargeForService { get; set; }
        public string? Removed { get; set; }

        public virtual WorkOrder? WorkOrder { get; set; }
    }
}
