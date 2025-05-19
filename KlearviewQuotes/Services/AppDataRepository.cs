using Microsoft.ApplicationInsights;
using KlearviewQuotes.Models;
using KlearviewQuotes.Data;
using KlearviewQuotes.Services.Interfaces;
using KlearviewQuotes.Models.Clients;
using Microsoft.EntityFrameworkCore;

namespace KlearviewQuotes.Services
{
    public class AppDataRepository : IAppDataRepository
    {

        private readonly AppDataContext _dbContext;
        private readonly ApplicationDbContext _appDbContext;
        private readonly TelemetryClient _telemetryClient;

        public AppDataRepository(AppDataContext dbContext, ApplicationDbContext appDbContext, TelemetryClient telemetryClient)
        {
            _dbContext = dbContext;
            _appDbContext = appDbContext;
            _telemetryClient = telemetryClient;
        }

        public async Task<IList<Quote>?> GetAllQuotesAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                var quotes = _dbContext.Quotes.ToList();

                return quotes;
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackException(ex);
                return null;
            }
        }

        public async Task<Quote?> GetQuoteAsync(int id)
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                var quote = await _dbContext.Quotes.FindAsync(id);

                return quote;
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackException(ex);
                return null;
            }
        }

        public async Task<bool> AddQuoteAsync(Quote quote)
        {
            try
            {
                await _dbContext.Quotes.AddAsync(quote);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackException(ex);
                return false;
            }
        }

        public async Task<bool> UpdateQuoteAsync(Quote quote)
        {
            try
            {  
                _dbContext.Quotes.Update(quote);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackException(ex);
                return false;
            }
        }

        public async Task<IList<Status>?> GetStatusAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                var status = _dbContext.Status.ToList();

                return status;
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackException(ex);
                return null;
            }
        }

        public async Task<IList<Account>?> GetAccountsAsync()
        {
            try
            {
                await _appDbContext.SaveChangesAsync();
                return _appDbContext.Accounts
                    .Include(a => a.DefaultServiceLocation.DefaultContact.ContactEmails)
                    .Include(a => a.DefaultServiceLocation.DefaultContact.ContactPhones)
                    .ToList();
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackException(ex);
                return null;
            }
        }

        public async Task<Account?> GetAccountAsync(string id)
        {
            try
            {
                await _appDbContext.SaveChangesAsync();
                return await _appDbContext.Accounts
                    .Include(a => a.Agreements)
                    .Include(a => a.WorkOrders)
                        .ThenInclude(a => a.ServiceLocation)
                    .Include(a => a.WorkOrders)
                        .ThenInclude(a => a.BillingLocation)
                    .Include(a => a.ServiceLocations)
                        .ThenInclude(a => a.Zone)
                    .Include(a => a.DefaultServiceLocation)
                    .Include(a => a.DefaultServiceLocation)
                    .FirstOrDefaultAsync(a => a.AccountId == id);
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackException(ex);
                return null;
            }
        }

        public async Task<IList<WorkOrder>?> GetWorkOrdersAsync()
        {
            try
            {
                await _appDbContext.SaveChangesAsync();
                return _appDbContext.WorkOrders
                    .Include(a => a.ServiceLocation)
                    .Include(a => a.BillingLocation)
                    .Include(a => a.Account)
                    .ToList();
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackException(ex);
                return null;
            }
        }

        public async Task<WorkOrder?> GetWorkOrderAsync(string id)
        {
            try
            {
                await _appDbContext.SaveChangesAsync();
                return await _appDbContext.WorkOrders
                    .Include(a => a.ServiceLocation)
                    .Include(a => a.BillingLocation)
                    .Include(a => a.Resource)
                    .Include(a => a.CommittedResource)
                    .Include(a => a.PerformedByResource)
                    .Include(a => a.Campaign)
                    .Include(a => a.WorkOrderServices)
                    .Include(a => a.UserDefinedFieldValues)
                        .ThenInclude(a => a.UserDefinedField)
                    .FirstOrDefaultAsync(a => a.WorkOrderId == id);
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackException(ex);
                return null;
            }
        }

        public async Task<ServiceLocation?> GetServiceLocationAsync(string id)
        {
            try
            {
                await _appDbContext.SaveChangesAsync();
                var location = await _appDbContext.ServiceLocations
                    .Include(e => e.DefaultContact.ContactPhones)
                    .Include(e => e.DefaultContact.ContactEmails)
                    .Include(e => e.UserDefinedFieldValues)
                        .ThenInclude(e => e.UserDefinedField)
                    .FirstOrDefaultAsync(e => e.ServiceLocationId == id);

                return location;
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackException(ex);
                return null;
            }
        }

        public async Task<BillingLocation?> GetBillingLocationAsync(string id)
        {
            try
            {
                await _appDbContext.SaveChangesAsync();
                var location = await _appDbContext.BillingLocations
                    .Include(e => e.DefaultContact.ContactPhones)
                    .Include(e => e.DefaultContact.ContactEmails)
                    .FirstOrDefaultAsync(e => e.BillingLocationId == id);

                return location;
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackException(ex);
                return null;
            }
        }

        public async Task<IList<Invoice>?> GetInvoicesAsync()
        {
            try
            {
                await _appDbContext.SaveChangesAsync();
                return _appDbContext.Invoices.ToList();
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackException(ex);
                return null;
            }
        }

        public async Task<Invoice?> GetInvoiceAsync(string id)
        {
            try
            {
                await _appDbContext.SaveChangesAsync();
                return await _appDbContext.Invoices
                    .Include(e => e.Account)
                    .Include(e => e.Account.DefualtBillingLocation.DefaultContact.ContactPhones)
                    .FirstOrDefaultAsync(a => a.InvoiceId == id);
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackException(ex);
                return null;
            }
        }
    }
}
