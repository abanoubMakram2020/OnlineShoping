using OnlineShoping.Application.DTOs.InputDTO;
using OnlineShoping.Application.DTOs.OutputDTO;
using SharedKernal.Middlewares.Basees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoping.Application.ServicesInterfaces
{
    public interface ICartItemService
    {
        Task<ResponseResultDto<CartItemOutputDTO>> GetById(BaseRequestDto<int> Id);
        Task<ResponseResultDto<bool>> SaveCartItem(BaseRequestDto<CartItemInputDTO> CartItemInputDTO);
        Task<ResponseResultDto<bool>> DeleteById(BaseRequestDto<int> Id);
        Task<ResponseResultDto<List<CartItemOutputDTO>>> FilterByCartItem(BaseRequestDto<int> cartaId);
    }
}
