using System.Collections.Generic;

namespace RESTAPIRNSQLServer.Applications.Paginations
{
    public class PaginationResultSet<T>
    {
        public PaginationMetadata Pagination { get; set; }
        public IEnumerable<T> Contents { get; set; }
        public static PaginationResultSet<T> PackingGoods(int page, int total, int limit, IEnumerable<T> contents)
        {
            var pagination = new PaginationMetadata(page, total, limit);
            return new PaginationResultSet<T>()
            {
                Pagination = pagination,
                Contents = contents
            };
        }
    }
}