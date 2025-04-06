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

        // Old Data Viewer

        Task<IList<Account>?> GetAccountsAsync();
        Task<Account?> GetAccountAsync(string id);
    }
}
