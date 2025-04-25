namespace KlearviewQuotes.Models.Clients
{
    public class UserDefinedFieldValue
    {
        public string? Id { get; set; }
        public string? UserDefinedFieldId { get; set; }
        public string? Text { get; set; }
        public string? ServiceLocationId { get; set; }
        public string? WorkOrderId { get; set; }

        public virtual ServiceLocation ServiceLocation { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }
        public virtual UserDefinedField UserDefinedField { get; set; }
    }
}
