using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GridApp.DataService
{
    public static class MetaDataHelper
    {
        public static string[] ColumnNames = new string[]
        {
            "federal_provider_number",
            "provider_name",
            "provider_address",
            "provider_city",
            "provider_state",
            "provider_zip_code",
            "provider_phone_number"
        };

        public static readonly string QuerySelect = "?$query=select";

        public static string GetColumnList()
        {
            return string.Join(", ", ColumnNames);
        }

    }
}
