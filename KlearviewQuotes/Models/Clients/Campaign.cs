namespace KlearviewQuotes.Models.Clients
{
    public class Campaign
    {
        public string? CampaignId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual IList<WorkOrder> WorkOrders { get; set; }
    }
}
