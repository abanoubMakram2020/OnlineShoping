using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShoping.Application.DTOs.InputDTO;
using OnlineShoping.Application.DTOs.OutputDTO;
using OnlineShoping.Application.ServicesInterfaces;
using SharedKernal.Common;
using SharedKernal.Middlewares.Basees;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShoping.Presentation.API.Controllers
{
    
    public class CategoryController : BaseController
    {
        #region Fields
        private readonly ICategoryService _CategoryService;
        #endregion

        #region ctor
        public CategoryController(ICategoryService CategoryService, Presenter presenter) : base(presenter)
        {
            _CategoryService = CategoryService;
        }

        #endregion

        #region Actions
        [HttpGet]
        [MapToApiVersion(APIVersion.Version1)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseResultDto<List<CategoryOutputDTO>>))]
        public async Task<IActionResult> GetAll() => await _Presenter.Handle(_CategoryService.GetAll);

        [HttpGet]
        [MapToApiVersion(APIVersion.Version1)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseResultDto<CategoryOutputDTO>))]
        public async Task<IActionResult> GetById(int? CategoryId)
        {
            CategoryOutputDTO model = new CategoryOutputDTO();

            if (CategoryId != null)
                return await _Presenter.Handle(_CategoryService.GetById, new BaseRequestDto<int>
                {
                    Data = CategoryId.Value
                });

            return Ok(model);
        }

        [HttpPost]
        [MapToApiVersion(APIVersion.Version1)]
        public async Task<IActionResult> CreateOrUpdate(BaseRequestDto<CategoryInputDTO> CategoryInputDTO) =>
            await _Presenter.Handle(_CategoryService.SaveCategory, CategoryInputDTO);

        [HttpDelete]
        [MapToApiVersion(APIVersion.Version1)]
        public async Task<IActionResult> Delete(int CategoryId) =>
            await _Presenter.Handle(_CategoryService.DeleteById, new BaseRequestDto<int> { Data = CategoryId });



        #endregion

    }
}
