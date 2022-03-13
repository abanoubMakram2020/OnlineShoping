using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoping.Application.DTOs.OutputDTO
{
    public class DashboardDTO
    {
        public int CategoriesCount { get; set; }
        public int ProductsCount { get; set; }
        public int UsersCount { get; set; }
        public int CartsCount { get; set; }
    }
}
