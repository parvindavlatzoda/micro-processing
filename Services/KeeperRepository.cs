using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MP.Data;
using MP.Data.Keeper;
using MP.Helpers;
using MP.Keeper.Helpers;

namespace MP.Keeper.Services {
    public class KeeperRepository : IKeeperRepository {
        private AppDbContext _context;

        public KeeperRepository(AppDbContext context) {
            _context = context;
        }

        public RubReport GetReportTransaction(Guid id) {
            var transaction = _context.RubReports.FirstOrDefault(c => c.Id == id);

            return transaction;
        }

        public RubReport GetReportTransaction(int serviceUpgId, string qpayTransactionId, string providerTransactionId) {
            return _context.RubReports
                .Where(t => t.ServiceUpgId == serviceUpgId
                    && t.QpayTransactionId == qpayTransactionId
                    && t.ServiceProviderTransactionId == providerTransactionId)
                .FirstOrDefault();
        }

        public PagedList<RubReport> GetReportTransactions(ReportsResourceParameters parameters) {
            var collectionBeforePaging = _context.RubReports
                .AsNoTracking()
                .OrderByDescending(c => c.QpayCreatedAt)
                .AsQueryable();

            // Search.
            // if (!string.IsNullOrEmpty(parameters.SearchQuery)) {
            //     var searchQyeries = parameters.SearchQuery.Split(" ").ToList();

            //     foreach (var query in searchQyeries) {
            //         var searchQueryForWhereClause = query.Trim().ToLowerInvariant();
            //         collectionBeforePaging = collectionBeforePaging
            //             .Where(c => c.QpayTransactionId.ToLowerInvariant().Contains(searchQueryForWhereClause)
            //                 || c.GatewayTransactionId.ToLowerInvariant().Contains(searchQueryForWhereClause)
            //                 || c.ServiceProviderTransactionId.ToLowerInvariant().Contains(searchQueryForWhereClause)
            //                 || c.Account.ToLowerInvariant().Contains(searchQueryForWhereClause)
            //             );
            //     }
            // }

            var reports =  PagedList<RubReport>.Create(collectionBeforePaging,
                    parameters.PageNumber,
                    parameters.PageSize);

            return reports;
        }

        // Save to the database.
        public bool Save() {
            return (_context.SaveChanges() >= 0);
        }

        public void AddTransactionToReport(RubReport report) {
            if (report.Id == Guid.Empty) {
                report.Id = Guid.NewGuid();
            }

            _context.RubReports.Add(report);
        }

        public IEnumerable<Currency> GetCurrencies() {
            return _context.Currencies.ToList();
        }

        public Currency GetCurrency(Guid id) {
            return _context.Currencies.FirstOrDefault(c => c.Id == id);
        }

        public Currency GetCurrency(string isoCode) {
            return _context.Currencies.FirstOrDefault(c => c.IsoCode == isoCode);
        }

        public void AddCurrency(Currency currency) {
            _context.Currencies.Add(currency);
        }

        public CurrencyRate GetLastRateForCurrency(string isoCode) {
            var currency = _context.Currencies
                .Include(c => c.Rates)
                .FirstOrDefault(c => c.IsoCode == isoCode);
            
            return currency.Rates
                .OrderBy(c => c.CreatedAt)
                .LastOrDefault();
        }

        public CurrencyRate GetRateById(Guid id) {
            return _context.CurrencyRates
                .FirstOrDefault(c => c.Id == id);
        }

        public void AddRateForCurrency(CurrencyRate rate) {
            _context.CurrencyRates.Add(rate);
        }

        public PagedList<CurrencyRate> GetCurrencyRates(RatesResourceParameters parameters) {
            var collectionBeforePaging = _context.CurrencyRates
                .OrderByDescending(c => c.CreatedAt)
                .AsNoTracking()
                .AsQueryable();

            // Search.
            // if (!string.IsNullOrEmpty(parameters.SearchQuery)) {
            //     var searchQyeries = parameters.SearchQuery.Split(" ").ToList();

            //     foreach (var query in searchQyeries) {
            //         var searchQueryForWhereClause = query.Trim().ToLowerInvariant();
            //         collectionBeforePaging = collectionBeforePaging
            //             .Where(c => c.QpayTransactionId.ToLowerInvariant().Contains(searchQueryForWhereClause)
            //                 || c.GatewayTransactionId.ToLowerInvariant().Contains(searchQueryForWhereClause)
            //                 || c.ServiceProviderTransactionId.ToLowerInvariant().Contains(searchQueryForWhereClause)
            //                 || c.Account.ToLowerInvariant().Contains(searchQueryForWhereClause)
            //             );
            //     }
            // }

            if (!string.IsNullOrEmpty(parameters.IsoCode)) {
                //collectionBeforePaging = collectionBeforePaging
                //    .Where(c => c.IsoCode == parameters.IsoCode);
            }

            


            var rates =  PagedList<CurrencyRate>.Create(collectionBeforePaging,
                    parameters.PageNumber,
                    parameters.PageSize);

            return rates;
        }
    }
}