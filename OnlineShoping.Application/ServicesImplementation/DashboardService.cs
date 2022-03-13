using Microsoft.EntityFrameworkCore;
using OnlineShoping.Application.DTOs.OutputDTO;
using OnlineShoping.Application.ServicesInterfaces;
using OnlineShoping.Domain.RepositoryInterfaces;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShoping.Application.ServicesImplementation
{
    public class DashboardService : IDashboardService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;

        public DashboardService(ICategoryRepository categoryRepository, IUserRepository userRepository,
                                IProductRepository productRepository, ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public async Task<DashboardDTO> GetStatistics() =>
          new DashboardDTO
          {
              CategoriesCount = await _categoryRepository.Get().CountAsync(),
              ProductsCount = await _productRepository.Get().CountAsync(),
              UsersCount = await _userRepository.Get().CountAsync(),
              CartsCount = await _cartRepository.Get().CountAsync()
          };

    }
}
