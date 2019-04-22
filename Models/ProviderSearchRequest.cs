using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GridApp.Models
{
    public class ProviderSearchRequest
    {
        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }

        public string SearchTerm { get; set; }
        
    }
}
