using OnlineShoping.Domain.Entities;
using OnlineShoping.Domain.RepositoryInterfaces;
using OnlineShoping.Infra.Data.Context;

namespace OnlineShoping.Infra.Data.Repositrories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(OnlineShopingDBContext dbContext) : base(dbContext)
        {
        }
    }
}
