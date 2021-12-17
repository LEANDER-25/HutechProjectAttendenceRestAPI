using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTAPIRNSQLServer.Applications.Paginations
{
    public class PaginationMetadata
    {
        public PaginationMetadata(int currentPage, int totalRows, int limit)
        {
            CurrentPage = currentPage == 0 ? 1 : currentPage;
            TotalRows = totalRows;
            Limit = limit;
            TotalPages = (int)Math.Ceiling(totalRows / (double)limit);
        }
        public int CurrentPage { get; set; }
        public int Limit { get; set; }
        public int TotalRows { get; set; }
        public int TotalPages { get; set; }
        public bool HasPrevious { get { return CurrentPage > 1; } }
        public bool HasNext { get { return CurrentPage < TotalPages; } }
    }
}
