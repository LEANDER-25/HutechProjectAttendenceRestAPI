namespace RESTAPIRNSQLServer.Applications.FilterQueries
{
    public class PaginationOption
    {
        private int limit;
        public int? Limit
        {
            get
            {
                return limit == 0 ? 6 : limit;
            }
            set
            {
                limit = value.Value;
            }
        }
        private int page;
        public int? Page
        {
            get
            {
                return page == 0 ? 1 : page;
            }
            set
            {
                page = value.Value;
            }
        }
    }
}