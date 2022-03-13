using OnlineShoping.Domain.Entities;
using OnlineShoping.Domain.RepositoryInterfaces;
using OnlineShoping.Infra.Data.Context;

namespace OnlineShoping.Infra.Data.Repositrories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(OnlineShopingDBContext dbContext) : base(dbContext)
        {
        }
    }
}
