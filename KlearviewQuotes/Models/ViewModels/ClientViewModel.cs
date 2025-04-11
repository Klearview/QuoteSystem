using KlearviewQuotes.Models.Clients;
using X.PagedList;

namespace KlearviewQuotes.Models.ViewModels
{
    public class ClientViewModel
    {
        public Account Account { get; set; } 

        public IPagedList<Agreement> PagedAgreements { get; set; }
        public IPagedList<WorkOrder> PagedWorkOrders { get; set; }

        public ClientViewModel(Account account)
        {
            Account = account;
        }
    }
}
