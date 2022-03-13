using Microsoft.AspNetCore.Mvc;
using OnlineShoping.Application.DTOs.InputDTO;
using OnlineShoping.Application.ServicesInterfaces;
using SharedKernal.Common;
using SharedKernal.Middlewares.Basees;
using System.Threading.Tasks;

namespace OnlineShoping.Presentation.API.Controllers
{
    [ApiController]
    [Route(APIRoute.VersioningTemplate)]
    public class UserController : ControllerBase
    {
        public Presenter _presenter;
        private readonly IUserService _userService;
        public UserController(IUserService userService, Presenter presenter)
        {
            _userService = userService;
            _presenter = presenter;
        }

        [HttpPost]
        [MapToApiVersion(APIVersion.Version1)]
        public async Task<IActionResult> Login(BaseRequestDto<LoginDTO> userDTO) =>
             await _presenter.Handle(_userService.Login, userDTO);


        [HttpPost]
        [MapToApiVersion(APIVersion.Version1)]
        public async Task<IActionResult> Register(BaseRequestDto<RegistrationDTO> userRegistrationDTO) =>
          await _presenter.Handle(_userService.Register, userRegistrationDTO);


    }
}
