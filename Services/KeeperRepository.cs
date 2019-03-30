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


        public PagedList<RubReport> GetReportTransactions(ReportsResourceParameters parameters) {
            var collectionBeforePaging = _context.RubReports
                .AsNoTracking()
                .AsQueryable();

            // Search.
            if (!string.IsNullOrEmpty(parameters.SearchQuery)) {
                var searchQyeries = parameters.SearchQuery.Split(" ").ToList();

                foreach (var query in searchQyeries) {
                    var searchQueryForWhereClause = query.Trim().ToLowerInvariant();
                    collectionBeforePaging = collectionBeforePaging
                        .Where(c => c.QpayTransactionId.ToLowerInvariant().Contains(searchQueryForWhereClause)
                            || c.GatewayTransactionId.ToLowerInvariant().Contains(searchQueryForWhereClause)
                            || c.ServiceProviderTransactionId.ToLowerInvariant().Contains(searchQueryForWhereClause)
                            || c.Account.ToLowerInvariant().Contains(searchQueryForWhereClause)
                        );
                }
            }

            var reports =  PagedList<RubReport>.Create(collectionBeforePaging,
                    parameters.PageNumber,
                    parameters.PageSize);

            return reports;
        }
    }
}