using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShoping.Application.DTOs.InputDTO;
using OnlineShoping.Application.DTOs.OutputDTO;
using OnlineShoping.Application.ServicesInterfaces;
using OnlineShoping.Application.UnitOfWork;
using OnlineShoping.Domain.Entities;
using OnlineShoping.Domain.RepositoryInterfaces;
using SharedKernal.Middlewares.Basees;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShoping.Application.ServicesImplementation
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _autoMapper;

        public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository, IMapper autoMapper)
        {
            _productRepository = productRepository;
            _autoMapper = autoMapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseResultDto<bool>> DeleteById(BaseRequestDto<int> Id)
        {
            var productObj = await _productRepository.Get(Id.Data);
            if (productObj == null)
                return ResponseResultDto<bool>.NotFound(false, "not found");


            _productRepository.Delete(productObj);
            await _unitOfWork.Complete();
            return ResponseResultDto<bool>.Success(true, "Deleted successfully");
        }

        public async Task<ResponseResultDto<List<ProductOutputDTO>>> FilterByCategory(BaseRequestDto<int> categoryId)
        {
            var productList = await _productRepository.Get(x => x.CategoryId == categoryId.Data).ToListAsync();
            if (productList is null)
                return ResponseResultDto<List<ProductOutputDTO>>.NotFound(null, "not found");

            var result = _autoMapper.Map<List<ProductOutputDTO>>(productList);
            return ResponseResultDto<List<ProductOutputDTO>>.Success(result, " done");
        }

        public async Task<ResponseResultDto<List<ProductOutputDTO>>> GetAll()
        {
            var productList = await _productRepository.Get().ToListAsync();
            if (productList is null)
                return ResponseResultDto<List<ProductOutputDTO>>.NotFound(null, "not found");

            var result = _autoMapper.Map<List<ProductOutputDTO>>(productList);
            return ResponseResultDto<List<ProductOutputDTO>>.Success(result, " done");
        }

        public async Task<ResponseResultDto<ProductOutputDTO>> GetById(BaseRequestDto<int> Id)
        {
            var product = await _productRepository.Get(x => x.Id == Id.Data).FirstOrDefaultAsync();
            if (product is null)
                return ResponseResultDto<ProductOutputDTO>.NotFound(null, "not found");

            var result = _autoMapper.Map<ProductOutputDTO>(product);
            return ResponseResultDto<ProductOutputDTO>.Success(result, " done");
        }

        public async Task<ResponseResultDto<bool>> SaveProduct(BaseRequestDto<ProductInputDTO> productInputDTO)
        {
            Product productObj = _autoMapper.Map<Product>(productInputDTO.Data);

            if (productInputDTO.Data.Id > 0)
                _productRepository.Update(productObj);
            else
                await _productRepository.Add(productObj);

            return ResponseResultDto<bool>.Success(await _unitOfWork.Complete() > 0, " done");
        }
    }
}
