using OnlineShoping.Domain.Entities;
using OnlineShoping.Domain.RepositoryInterfaces;
using OnlineShoping.Infra.Data.Context;

namespace OnlineShoping.Infra.Data.Repositrories
{
    public class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(OnlineShopingDBContext dbContext) : base(dbContext)
        {
        }
    }
}
