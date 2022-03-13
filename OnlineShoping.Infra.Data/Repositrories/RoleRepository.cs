using OnlineShoping.Domain.Entities;
using OnlineShoping.Domain.RepositoryInterfaces;
using OnlineShoping.Infra.Data.Context;

namespace OnlineShoping.Infra.Data.Repositrories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(OnlineShopingDBContext dbContext) : base(dbContext)
        {
        }
    }
}
