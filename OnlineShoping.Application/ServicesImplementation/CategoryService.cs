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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _autoMapper;
        public CategoryService(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IMapper autoMapper)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
            _categoryRepository = categoryRepository;
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
            Category categoryObj = new Category();

            categoryObj = _autoMapper.Map<Category>(categoryInputDTO.Data);


            if (categoryInputDTO.Data.Id > 0)
                _categoryRepository.Update(categoryObj);
            else
                await _categoryRepository.Add(categoryObj);

            return ResponseResultDto<bool>.Success(await _unitOfWork.Complete() > 0, " done");
        }
    }
}
