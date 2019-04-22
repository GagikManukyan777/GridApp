using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GridApp.Models
{
    public class ProviderGridItem
    {
        public string Provider_Name{ get; set; }

        public string Federal_Provider_Number { get; set; }

        public string Provider_Address { get; set; }

        public string Provider_City { get; set; }

        public string Provider_State { get; set; }

        public string Provider_Zip_Code { get; set; }

        public string Provider_Phone_Number { get; set; }
    }
}
