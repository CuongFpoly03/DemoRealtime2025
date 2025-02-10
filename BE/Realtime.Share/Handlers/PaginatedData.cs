using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Realtime.Share.Handlers
{
    public class PaginatedData<T> where T : class
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<T> Data { get; set; }
        public PaginatedData()
        {
            Data = [];
        }
        public PaginatedData(List<T> _Data, int _TotalCount, int _PageNumber, int _PageSize)
        {
            Data = _Data;
            TotalCount = _TotalCount;
            PageNumber = _PageNumber;
            PageNumber = _PageSize;
        }
    }
}