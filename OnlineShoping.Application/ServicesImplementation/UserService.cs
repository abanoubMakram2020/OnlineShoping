using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineShoping.Application.DTOs.InputDTO;
using OnlineShoping.Application.DTOs.OutputDTO;
using OnlineShoping.Application.ServicesInterfaces;
using OnlineShoping.Application.UnitOfWork;
using OnlineShoping.Domain.Entities;
using OnlineShoping.Domain.RepositoryInterfaces;
using SharedKernal.Common.Enum;
using SharedKernal.Middlewares.Basees;
using SharedKernal.Middlewares.JWTSettings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShoping.Application.ServicesImplementation
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMapper;
        private readonly ITokenHandler _tokenHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion



        #region CTOR
        public UserService(IMapper autoMapper, ITokenHandler tokenHandler, IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository,IUnitOfWork unitOfWork
)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
            _tokenHandler = tokenHandler;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion
        public async Task<ResponseResultDto<TokenDTO>> Login(BaseRequestDto<UserInputDTO> userDTO)
        {

            var claims = _tokenHandler.GetTokenData(_httpContextAccessor.HttpContext.Request);
            if (claims != null && claims.Any())
            {
                return ResponseResultDto<TokenDTO>.MultiError(null, "you already authrized");
            }
            else
            {
                TokenDTO tokenDTO = new TokenDTO();
                var user = await _userRepository.Get(x => x.UserName == userDTO.Data.UserName && x.Password == userDTO.Data.Password).FirstOrDefaultAsync();
                if (user != null)
                {

                    List<UserClaim> userClaims = new List<UserClaim>()
                    {
                        new UserClaim { Name = SecurityEnum.TokenInfo.UserId, Value = user.Id.ToString() },
                        new UserClaim{Name=SecurityEnum.TokenInfo.UserName,Value=user.UserName }
                    };

                    tokenDTO.Token = _tokenHandler.CreateToken(userClaims, SecurityEnum.Audiance.Web);

                }
                if (!string.IsNullOrEmpty(tokenDTO.Token))
                    return ResponseResultDto<TokenDTO>.Success(tokenDTO, "Success");
                return ResponseResultDto<TokenDTO>.NotFound(null, "Failed");
            }
        }

        public async Task<ResponseResultDto<bool>> Register(BaseRequestDto<UserInputDTO> userRegistrationDTO)
        {
            var claims = _tokenHandler.GetTokenData(_httpContextAccessor.HttpContext.Request);
            if (claims != null && claims.Any())
            {
                return ResponseResultDto<bool>.InvalidData(false, "you already authrized");
            }
            else
            {
                User userObj = _autoMapper.Map<User>(userRegistrationDTO.Data);
                await _userRepository.Add(userObj);
                await _unitOfWork.Complete();

                return ResponseResultDto<bool>.Success(true, "Done");
            }
        }


    }
}
