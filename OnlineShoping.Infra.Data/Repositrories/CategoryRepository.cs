using OnlineShoping.Domain.Entities;
using OnlineShoping.Domain.RepositoryInterfaces;
using OnlineShoping.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoping.Infra.Data.Repositrories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(OnlineShopingDBContext dbContext) : base(dbContext)
        {
        }
    }
}
