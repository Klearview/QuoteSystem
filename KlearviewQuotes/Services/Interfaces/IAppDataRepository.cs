using KlearviewQuotes.Models;
using KlearviewQuotes.Models.Clients;

namespace KlearviewQuotes.Services.Interfaces
{
    public interface IAppDataRepository
    {
        Task<IList<Quote>?> GetAllQuotesAsync();
        Task<Quote?> GetQuoteAsync(int id);

        Task<bool> AddQuoteAsync(Quote quote);
        Task<bool> UpdateQuoteAsync(Quote quote);

        //Task<string> GetUsernameById(string id);

        Task<IList<Status>?> GetStatusAsync();


        #region Old Data

        Task<IList<Account>?> GetAccountsAsync();
        Task<Account?> GetAccountAsync(string id);

        Task<IList<WorkOrder>?> GetWorkOrdersAsync();
        Task<WorkOrder?> GetWorkOrderAsync(string id);

        Task<IList<Invoice>?> GetInvoicesAsync();
        Task<Invoice?> GetInvoiceAsync(string id);

        Task<ServiceLocation?> GetServiceLocationAsync(string id);
        Task<BillingLocation?> GetBillingLocationAsync(string id);

        #endregion
    }
}
