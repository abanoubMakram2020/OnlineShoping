using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShoping.Application.DTOs.InputDTO;
using OnlineShoping.Application.DTOs.OutputDTO;
using OnlineShoping.Application.ServicesInterfaces;
using OnlineShoping.Application.UnitOfWork;
using OnlineShoping.Domain.Entities;
using OnlineShoping.Domain.RepositoryInterfaces;
using SharedKernal.Middlewares.Basees;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShoping.Application.ServicesImplementation
{
    public class CartItemService : ICartItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _autoMapper;


        public CartItemService(IUnitOfWork unitOfWork, ICartItemRepository cartItemRepository, IMapper autoMapper)
        {
            _unitOfWork = unitOfWork;
            _cartItemRepository = cartItemRepository;
            _autoMapper = autoMapper;
        }
        public async Task<ResponseResultDto<bool>> DeleteById(BaseRequestDto<int> Id)
        {
            var productObj = await _cartItemRepository.Get(Id.Data);
            if (productObj == null)
                return ResponseResultDto<bool>.NotFound(false, "not found");


            _cartItemRepository.Delete(productObj);
            await _unitOfWork.Complete();
            return ResponseResultDto<bool>.Success(true, "Deleted successfully");
        }

        public async Task<ResponseResultDto<List<CartItemOutputDTO>>> FilterByCartItem(BaseRequestDto<int> cartaId)
        {
            var cartItemList = await _cartItemRepository.Get(x => x.CartId == cartaId.Data).ToListAsync();
            if (cartItemList is null)
                return ResponseResultDto<List<CartItemOutputDTO>>.NotFound(null, "not found");

            var result = _autoMapper.Map<List<CartItemOutputDTO>>(cartItemList);
            return ResponseResultDto<List<CartItemOutputDTO>>.Success(result, " done");
        }


        public async Task<ResponseResultDto<CartItemOutputDTO>> GetById(BaseRequestDto<int> Id)
        {
            var cartItem = await _cartItemRepository.Get(x => x.Id == Id.Data).FirstOrDefaultAsync();
            if (cartItem is null)
                return ResponseResultDto<CartItemOutputDTO>.NotFound(null, "not found");

            var result = _autoMapper.Map<CartItemOutputDTO>(cartItem);
            return ResponseResultDto<CartItemOutputDTO>.Success(result, " done");
        }

        public async Task<ResponseResultDto<bool>> SaveCartItem(BaseRequestDto<CartItemInputDTO> cartItemInputDTO)
        {
            CartItem cartitemObj = _autoMapper.Map<CartItem>(cartItemInputDTO.Data);

            if (cartItemInputDTO.Data.Id > 0)
                _cartItemRepository.Update(cartitemObj);
            else
                await _cartItemRepository.Add(cartitemObj);

            return ResponseResultDto<bool>.Success(await _unitOfWork.Complete() > 0, " done");
        }
    }
}
