using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OnlineShoping.Application.DTOs.InputDTO;
using OnlineShoping.Application.DTOs.OutputDTO;
using OnlineShoping.Application.ServicesInterfaces;
using OnlineShoping.Application.UnitOfWork;
using OnlineShoping.Application.Validations;
using OnlineShoping.Domain.Entities;
using OnlineShoping.Domain.RepositoryInterfaces;
using SharedKernal.Middlewares.Basees;
using SharedKernal.Middlewares.ResourcesReader;
using SharedKernal.Middlewares.ResourcesReader.Message;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShoping.Application.ServicesImplementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _autoMapper;
        private readonly IValidator<CategoryInputDTO> _validator;
        private readonly IMessageResourceReader _messageResourceReader;

        public CategoryService(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository
                             , IValidator<CategoryInputDTO> validator, IMapper autoMapper, IMessageResourceReader messageResourceReader)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
            _categoryRepository = categoryRepository;
            _validator = validator;
            _messageResourceReader = messageResourceReader;
        }
        public async Task<ResponseResultDto<bool>> DeleteById(BaseRequestDto<int> Id)
        {
            var categoryObj = await _categoryRepository.Get(Id.Data);
            if (categoryObj == null)
                return ResponseResultDto<bool>.NotFound(false, "not found");


            _categoryRepository.Delete(categoryObj);
            await _unitOfWork.Complete();
            return ResponseResultDto<bool>.Success(true, "Deleted successfully");
        }

        public async Task<ResponseResultDto<List<CategoryOutputDTO>>> GetAll()
        {
            var categoryList = await _categoryRepository.Get().ToListAsync();
            if (categoryList is null)
                return ResponseResultDto<List<CategoryOutputDTO>>.NotFound(null, "not found");

            var result = _autoMapper.Map<List<CategoryOutputDTO>>(categoryList);
            return ResponseResultDto<List<CategoryOutputDTO>>.Success(result, " done");
        }

        public async Task<ResponseResultDto<CategoryOutputDTO>> GetById(BaseRequestDto<int> Id)
        {
            var category = await _categoryRepository.Get(expression: r => r.Id == Id.Data).FirstOrDefaultAsync();
            if (category is null)
                return ResponseResultDto<CategoryOutputDTO>.NotFound(null, "not found");

            var result = _autoMapper.Map<CategoryOutputDTO>(category);
            return ResponseResultDto<CategoryOutputDTO>.Success(result, " done");
        }

        public async Task<ResponseResultDto<bool>> SaveCategory(BaseRequestDto<CategoryInputDTO> categoryInputDTO)
        {
            var result = _validator.Validate(categoryInputDTO.Data);
            if (!result.IsValid)
            {
                Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
                result.Errors.GroupBy(p => p.PropertyName).ToList().ForEach(item => dict.Add(item.Key, item.Distinct().Select(e => e.ErrorMessage).ToList()));
                return ResponseResultDto<bool>.MultiError(dic: dict, message: "error");
            }
            Category categoryObj = new Category();

            categoryObj = _autoMapper.Map<Category>(categoryInputDTO.Data);


            if (categoryInputDTO.Data.Id > 0)
                _categoryRepository.Update(categoryObj);
            else
                await _categoryRepository.Add(categoryObj);

            return ResponseResultDto<bool>.Success(result: await _unitOfWork.Complete() > 0, message: _messageResourceReader.GetMessage(ResourcesMessageKey.Successfully));
        }
    }
}
