using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernal.Common.Configuration
{
    public class DatabaseConfiguration
    {
        public static string ConnectionString { get; set; } = "Server=.;Database=OnlineShopingDB;Trusted_Connection=True;MultipleActiveResultSets=true";
        public static bool AllowDropRecreateDatabase { get; set; }
    }
}
