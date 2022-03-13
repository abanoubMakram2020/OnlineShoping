using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShoping.Application.DTOs.InputDTO;
using OnlineShoping.Application.DTOs.OutputDTO;
using OnlineShoping.Application.ServicesInterfaces;
using OnlineShoping.Application.UnitOfWork;
using OnlineShoping.Domain.Entities;
using OnlineShoping.Domain.RepositoryInterfaces;
using SharedKernal.Middlewares.Basees;
using System.Threading.Tasks;

namespace OnlineShoping.Application.ServicesImplementation
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _autoMapper;
        public CartService(IUnitOfWork unitOfWor, ICartRepository cartRepository, IMapper autoMapper)
        {
            _autoMapper = autoMapper;
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWor;
        }

        public async Task<ResponseResultDto<CartOutputDTO>> GetByUserId(BaseRequestDto<int> userId)
        {
            var cart = await _cartRepository.Get(x => x.UserId == userId.Data).FirstOrDefaultAsync();
            if (cart is null)
                return ResponseResultDto<CartOutputDTO>.NotFound(null, "not found");

            var result = _autoMapper.Map<CartOutputDTO>(cart);
            return ResponseResultDto<CartOutputDTO>.Success(result, " done");
        }
        public async Task<ResponseResultDto<bool>> SaveCart(BaseRequestDto<CartInputDTO> cartInputDTO)
        {
            Cart cartObj = _autoMapper.Map<Cart>(cartInputDTO.Data);

            if (cartInputDTO.Data.Id > 0)
                _cartRepository.Update(cartObj);
            else
                await _cartRepository.Add(cartObj);

            return ResponseResultDto<bool>.Success(await _unitOfWork.Complete() > 0, " done");
        }
    }
}
