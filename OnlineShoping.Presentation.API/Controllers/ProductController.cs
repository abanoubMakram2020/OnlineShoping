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
    
    public class ProductController : BaseController
    {
        #region Fields
        private readonly IProductService _ProductService;
        #endregion

        #region ctor
        public ProductController(IProductService ProductService, Presenter presenter) : base(presenter)
        {
            _ProductService = ProductService;
        }

        #endregion

        #region Actions
        [HttpGet]
        [MapToApiVersion(APIVersion.Version1)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseResultDto<List<ProductOutputDTO>>))]
        public async Task<IActionResult> GetAll() => await _Presenter.Handle(_ProductService.GetAll);

        [HttpGet]
        [MapToApiVersion(APIVersion.Version1)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseResultDto<ProductOutputDTO>))]
        public async Task<IActionResult> GetById(int? ProductId)
        {
            ProductOutputDTO model = new ProductOutputDTO();

            if (ProductId != null)
                return await _Presenter.Handle(_ProductService.GetById, new BaseRequestDto<int>
                {
                    Data = ProductId.Value
                });

            return Ok(model);
        }

        [HttpPost]
        [MapToApiVersion(APIVersion.Version1)]
        public async Task<IActionResult> CreateOrUpdate([FromForm] BaseRequestDto<ProductInputDTO> ProductInputDTO) =>
            await _Presenter.Handle(_ProductService.SaveProduct, ProductInputDTO);

        [HttpDelete]
        [MapToApiVersion(APIVersion.Version1)]
        public async Task<IActionResult> Delete(int ProductId) =>
            await _Presenter.Handle(_ProductService.DeleteById, new BaseRequestDto<int> { Data = ProductId });



        #endregion

    }
}
