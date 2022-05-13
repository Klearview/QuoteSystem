﻿using QuoteSystem.Models;

namespace QuoteSystem.Services
{
    public interface IAppDataRepository
    {

        Task<IList<Quote>?> GetAllQuotesAsync();
        Task<Quote?> GetQuoteAsync(long id);

        Task<bool> AddQuoteAsync(Quote quote);
        Task<bool> UpdateQuoteAsync(Quote quote);

    }
}