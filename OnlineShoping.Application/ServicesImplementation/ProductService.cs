using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OnlineShoping.Application.DTOs.InputDTO;
using OnlineShoping.Application.DTOs.OutputDTO;
using OnlineShoping.Application.ServicesInterfaces;
using OnlineShoping.Application.UnitOfWork;
using OnlineShoping.Domain.Entities;
using OnlineShoping.Domain.RepositoryInterfaces;
using SharedKernal.Common;
using SharedKernal.Common.Configuration;
using SharedKernal.Common.Enum;
using SharedKernal.Middlewares.Basees;
using SharedKernal.Middlewares.ResourcesReader;
using SharedKernal.Middlewares.ResourcesReader.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShoping.Application.ServicesImplementation
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _autoMapper;
        private readonly IValidator<ProductInputDTO> _validator;
        private readonly IMessageResourceReader _messageResourceReader;

        public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository, IValidator<ProductInputDTO> validator,
                              IMapper autoMapper, IMessageResourceReader messageResourceReader)
        {
            _productRepository = productRepository;
            _autoMapper = autoMapper;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _messageResourceReader = messageResourceReader;
        }
        public async Task<ResponseResultDto<bool>> DeleteById(BaseRequestDto<int> Id)
        {
            var productObj = await _productRepository.Get(Id.Data);
            if (productObj == null)
                return ResponseResultDto<bool>.NotFound(result: false, message: _messageResourceReader.GetMessage(ResponseStatusCode.NotFound));


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
                return ResponseResultDto<List<ProductOutputDTO>>.NotFound(result: null, message: _messageResourceReader.GetMessage(ResponseStatusCode.NotFound));

            var result = _autoMapper.Map<List<ProductOutputDTO>>(productList);
            return ResponseResultDto<List<ProductOutputDTO>>.Success(result, " done");
        }

        public async Task<ResponseResultDto<ProductOutputDTO>> GetById(BaseRequestDto<int> Id)
        {
            var product = await _productRepository.Get(Id.Data);
            if (product is null)
                return ResponseResultDto<ProductOutputDTO>.NotFound(null, "not found");

            var result = _autoMapper.Map<ProductOutputDTO>(product);
            return ResponseResultDto<ProductOutputDTO>.Success(result, " done");
        }

        public async Task<ResponseResultDto<bool>> SaveProduct(BaseRequestDto<ProductInputDTO> productInputDTO)
        {

            if (productInputDTO.Data.ProductImage is null && productInputDTO.Data.Id == default(int))
                return ResponseResultDto<bool>.InvalidData(result: false, message: _messageResourceReader.GetValidationMessage(ValidationMessageKey.ProductImageValidation));

            if (productInputDTO.Data.ProductImage is not null )
            {
                string perfix = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "-";
                bool uploadResult = await GeneralUtilities.Upload(productInputDTO.Data.ProductImage, UploadFilesConfigurations.ImageFolderUrl, perfix);
                if (uploadResult)
                    productInputDTO.Data.ImageURL = perfix + productInputDTO.Data.ProductImage.FileName;
                else
                    return ResponseResultDto<bool>.InvalidData(result: false, message: _messageResourceReader.GetValidationMessage(ValidationMessageKey.UploadProductImageFailed));
            }

            var result = _validator.Validate(productInputDTO.Data);
            if (!result.IsValid)
            {
                Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
                result.Errors.GroupBy(p => p.PropertyName).ToList().ForEach(item => dict.Add(item.Key, item.Distinct().Select(e => e.ErrorMessage).ToList()));
                return ResponseResultDto<bool>.MultiError(dic: dict, message: "error");
            }

            Product productObj = _autoMapper.Map<Product>(productInputDTO.Data);

            if (productInputDTO.Data.Id > default(int))
                _productRepository.Update(productObj);
            else
                await _productRepository.Add(productObj);

            return ResponseResultDto<bool>.Success(await _unitOfWork.Complete() > 0, " done");
        }
    }
}
