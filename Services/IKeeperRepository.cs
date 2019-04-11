using System;
using System.Collections.Generic;
using MP.Data.Keeper;
using MP.Helpers;
using MP.Keeper.Helpers;

namespace MP.Keeper.Services
{
    public interface IKeeperRepository
    {
        RubReport GetReportTransaction(Guid id);
        ICollection<RubReport> GetReportTransactions(DateTime? from = null, DateTime? to = null, int? serviceUpgId = null);
        RubReport GetReportTransaction(int serviceUpgId, string qpayTransactionId, string providerTransactionId);
        PagedList<RubReport> GetReportTransactions(ReportsResourceParameters parameters);
        void AddTransactionToReport(RubReport report);
        bool Save();

        IEnumerable<Currency> GetCurrencies();
        Currency GetCurrency(Guid id);
        Currency GetCurrency(string isoCode);
        void AddCurrency(Currency currency);

        CurrencyRate GetLastRateForCurrency(string isoCode);
        CurrencyRate GetRateById(Guid id);
        void AddRateForCurrency(CurrencyRate rate);
        PagedList<CurrencyRate> GetCurrencyRates(RatesResourceParameters parameters);

    }
}