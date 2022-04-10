using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShoping.Application.DTOs.InputDTO;
using OnlineShoping.Application.DTOs.OutputDTO;
using OnlineShoping.Application.ServicesInterfaces;
using OnlineShoping.Application.UnitOfWork;
using OnlineShoping.Domain.Entities;
using OnlineShoping.Domain.RepositoryInterfaces;
using SharedKernal.Common.Enum;
using SharedKernal.Middlewares.Basees;
using SharedKernal.Middlewares.ResourcesReader.Message;
using System.Threading.Tasks;

namespace OnlineShoping.Application.ServicesImplementation
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _autoMapper;
        private readonly IMessageResourceReader _messageResourceReader;

        public CartService(IUnitOfWork unitOfWor, ICartRepository cartRepository, IMapper autoMapper, IMessageResourceReader messageResourceReader)
        {
            _autoMapper = autoMapper;
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWor;
            _messageResourceReader = messageResourceReader;
        }

        public async Task<ResponseResultDto<CartOutputDTO>> GetByUserId(BaseRequestDto<int> userId)
        {
            var cart = await _cartRepository.Get(x => x.UserId == userId.Data).FirstOrDefaultAsync();
            if (cart is null)
                return ResponseResultDto<CartOutputDTO>.NotFound(result: null, message: _messageResourceReader.GetMessage(ResponseStatusCode.NotFound));

            var result = _autoMapper.Map<CartOutputDTO>(cart);
            return ResponseResultDto<CartOutputDTO>.Success(result: result, message: _messageResourceReader.GetMessage(ResponseStatusCode.Successfully));
        }
        public async Task<ResponseResultDto<bool>> SaveCart(BaseRequestDto<CartInputDTO> cartInputDTO)
        {
            Cart cartObj = _autoMapper.Map<Cart>(cartInputDTO.Data);

            if (cartInputDTO.Data.Id > 0)
                _cartRepository.Update(cartObj);
            else
                await _cartRepository.Add(cartObj);

            return ResponseResultDto<bool>.Success(result: await _unitOfWork.Complete() > 0, message: _messageResourceReader.GetMessage(ResponseStatusCode.Successfully));
        }
    }
}
