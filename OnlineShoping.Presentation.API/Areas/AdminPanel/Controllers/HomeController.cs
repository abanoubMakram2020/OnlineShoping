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
        private readonly IDashboardService _dashboardService;
        #endregion

        #region ctor
        public HomeController(IDashboardService dashboardService, Presenter presenter) : base(presenter)
        {
            _dashboardService = dashboardService;
        }

        #endregion


        [HttpGet]
        [MapToApiVersion(APIVersion.Version1)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseResultDto<DashboardDTO>))]
        public async Task<IActionResult> GetStastics() => await _Presenter.Handle(_dashboardService.GetStatistics);



    }
}