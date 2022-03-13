using OnlineShoping.Application.DTOs.InputDTO;
using OnlineShoping.Application.DTOs.OutputDTO;
using SharedKernal.Middlewares.Basees;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShoping.Application.ServicesInterfaces
{
    public interface ICategoryService
    {
        Task<ResponseResultDto<CategoryOutputDTO>> GetById(BaseRequestDto<int> Id);
        Task<ResponseResultDto<List<CategoryOutputDTO>>> GetAll();
        Task<ResponseResultDto<bool>> SaveCategory(BaseRequestDto<CategoryInputDTO> categoryInputDTO);
        Task<ResponseResultDto<bool>> DeleteById(BaseRequestDto<int> Id);
    }
}
