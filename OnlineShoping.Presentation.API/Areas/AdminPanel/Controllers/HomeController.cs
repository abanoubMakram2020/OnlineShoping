using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShoping.Application.DTOs.OutputDTO;
using OnlineShoping.Application.ServicesInterfaces;
using OnlineShoping.Presentation.API.Controllers;
using SharedKernal.Common;
using SharedKernal.Middlewares.Basees;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShoping.Presentation.API.Areas.AdminPanel.Controllers
{
   [Area("AdminPanel")]
    public class HomeController : BaseController
    {
        #region Fields
        private readonly ICategoryService _CategoryService;
        #endregion

        #region ctor
        public HomeController(ICategoryService CategoryService, Presenter presenter) : base(presenter)
        {
            _CategoryService = CategoryService;
        }

        #endregion


        [HttpGet]
        [MapToApiVersion(APIVersion.Version1)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseResultDto<List<CategoryOutputDTO>>))]
        public async Task<IActionResult> GetAll() => await _Presenter.Handle(_CategoryService.GetAll);


        //public IActionResult Dashboard()
        //{
        //    return View();
        //}
    }
}