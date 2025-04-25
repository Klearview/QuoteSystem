namespace KlearviewQuotes.Models.Clients
{
    public class Team
    {
        public string? TeamId { get; set; }
        public string? Name { get; set; }
        public string? CallSign { get; set; }
        public string? Color { get; set; }

        public virtual IList<WorkOrder> WorkOrders { get; set; }
        public virtual IList<WorkOrder> WorkOrdersCommited { get; set; }
        public virtual IList<WorkOrder> WorkOrdersPerformed { get; set; }
    }
}
