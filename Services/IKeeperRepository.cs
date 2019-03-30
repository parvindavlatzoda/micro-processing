using MP.Data.Keeper;
using MP.Helpers;
using MP.Keeper.Helpers;

namespace MP.Keeper.Services
{
    public interface IKeeperRepository
    {
        PagedList<RubReport> GetReportTransactions(ReportsResourceParameters parameters);
    }
}