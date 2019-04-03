using System;
using MP.Data.Keeper;
using MP.Helpers;
using MP.Keeper.Helpers;

namespace MP.Keeper.Services
{
    public interface IKeeperRepository
    {
        RubReport GetReportTransaction(Guid id);
        RubReport GetReportTransaction(int serviceUpgId, string qpayTransactionId, string providerTransactionId);
        PagedList<RubReport> GetReportTransactions(ReportsResourceParameters parameters);
        void AddTransactionToReport(RubReport report);
        bool Save();
    }
}