using System;
using MP.Data.Keeper;
using MP.Helpers;
using MP.Keeper.Helpers;

namespace MP.Keeper.Services
{
    public interface IKeeperRepository
    {
        RubReport GetReportTransaction(Guid id);
        PagedList<RubReport> GetReportTransactions(ReportsResourceParameters parameters);
        void AddTransactionToReport(RubReport report);
        bool Save();
    }
}