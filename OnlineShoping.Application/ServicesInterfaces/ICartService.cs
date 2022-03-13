using OnlineShoping.Application.DTOs.InputDTO;
using OnlineShoping.Application.DTOs.OutputDTO;
using SharedKernal.Middlewares.Basees;
using System.Threading.Tasks;

namespace OnlineShoping.Application.ServicesInterfaces
{
    public interface ICartService
    {
        Task<ResponseResultDto<CartOutputDTO>> GetByUserId(BaseRequestDto<int> userId);
        Task<ResponseResultDto<bool>> SaveCart(BaseRequestDto<CartInputDTO> cartInputDTO);
    }
}
