using OnlineShoping.Application.DTOs.InputDTO;
using OnlineShoping.Application.DTOs.OutputDTO;
using OnlineShoping.Domain.Entities;
using SharedKernal.Middlewares.Basees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoping.Application.ServicesInterfaces
{
    public interface IProductService
    {
        Task<ResponseResultDto<ProductOutputDTO>> GetById(BaseRequestDto<int> Id);
        Task<ResponseResultDto<List<ProductOutputDTO>>> GetAll();
        Task<ResponseResultDto<bool>> SaveProduct(BaseRequestDto<ProductInputDTO> productInputDTO);
        Task<ResponseResultDto<bool>> DeleteById(BaseRequestDto<int> Id);
        Task<ResponseResultDto<List<ProductOutputDTO>>> FilterByCategory(BaseRequestDto<int> categoryId);
    }
}
