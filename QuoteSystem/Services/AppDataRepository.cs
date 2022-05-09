using Microsoft.ApplicationInsights;
using QuoteSystem.Models;

namespace QuoteSystem.Services
{
    public class AppDataRepository : IAppDataRepository
    {

        private readonly AppDataContext _dbContext;
        private readonly TelemetryClient _telemetryClient;

        public AppDataRepository(AppDataContext dbContext, TelemetryClient telemetryClient)
        {
            _dbContext = dbContext;
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

        public async Task<Quote?> GetQuoteAsync(long id)
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
    }
}
