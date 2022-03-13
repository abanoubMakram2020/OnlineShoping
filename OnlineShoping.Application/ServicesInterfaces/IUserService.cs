using OnlineShoping.Application.DTOs.InputDTO;
using OnlineShoping.Application.DTOs.OutputDTO;
using SharedKernal.Middlewares.Basees;
using System.Threading.Tasks;

namespace OnlineShoping.Application.ServicesInterfaces
{
    public interface IUserService
    {
        Task<ResponseResultDto<bool>> Register(BaseRequestDto<RegistrationDTO> user);
        Task<ResponseResultDto<TokenDTO>> Login(BaseRequestDto<LoginDTO> user);
    }
}
