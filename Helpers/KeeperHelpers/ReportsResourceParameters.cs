namespace MP.Keeper.Helpers
{
    public class ReportsResourceParameters {
        private const int maxPageSize = 500;

        public int PageNumber { get; set; } = 1;
        public string SearchQuery { get; set; }

        private int _pageSize = 10;

        public int PageSize {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}