using OnlineShoping.Domain.Entities;
using OnlineShoping.Domain.RepositoryInterfaces;
using OnlineShoping.Infra.Data.Context;

namespace OnlineShoping.Infra.Data.Repositrories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(OnlineShopingDBContext context) : base(context)
        {

        }
    }
}
