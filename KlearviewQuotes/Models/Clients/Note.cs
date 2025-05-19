namespace KlearviewQuotes.Models.Clients
{
    public class Note
    {
        public string? Id { get; set; }
        public string? Body { get; set; }
        public string? CreatedOn { get; set; }
        public string? OriginalAuthorId { get; set; }
        public string? AuthorId { get; set; }
        public string? AccountId { get; set; }
        public string? WorkOrderId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual WorkOrder? WorkOrder { get; set; }
    }
}
