using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GridApp.Models
{
    public class PagedResult<T>
    {
        public T[] Items { get; set; }

        public long TotalRowCount { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

    }
}
