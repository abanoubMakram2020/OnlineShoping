using AutoMapper;
using FluentValidation;
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
using SharedKernal.Middlewares.ResourcesReader;
using SharedKernal.Middlewares.ResourcesReader.Message;
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
        private readonly IValidator<RegistrationDTO> _validator;
        private readonly IMessageResourceReader _messageResourceReader;
        private readonly ICartRepository _cartRepository;
        #endregion



        #region CTOR
        public UserService(IMapper autoMapper, ITokenHandler tokenHandler, IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository, IUnitOfWork unitOfWork, IValidator<RegistrationDTO> validator,
            IMessageResourceReader messageResourceReader, ICartRepository cartRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
            _tokenHandler = tokenHandler;
            _httpContextAccessor = httpContextAccessor;
            _validator = validator;
            _messageResourceReader = messageResourceReader;
            _cartRepository = cartRepository;
        }
        #endregion
        public async Task<ResponseResultDto<TokenDTO>> Login(BaseRequestDto<LoginDTO> userDTO)
        {

            var claims = _tokenHandler.GetTokenData(_httpContextAccessor.HttpContext.Request);
            if (claims != null && claims.Any())
            {
                return ResponseResultDto<TokenDTO>.MultiError(result: null, "you already authrized");
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
                    return ResponseResultDto<TokenDTO>.Success(result: tokenDTO, message: _messageResourceReader.GetMessage(ResourcesMessageKey.Successfully));
                return ResponseResultDto<TokenDTO>.NotFound(result: null, message: _messageResourceReader.GetMessage(ResourcesMessageKey.NotDataFound));
            }
        }

        public async Task<ResponseResultDto<bool>> Register(BaseRequestDto<RegistrationDTO> userRegistrationDTO)
        {
            var result = _validator.Validate(userRegistrationDTO.Data);
            if (!result.IsValid)
            {
                Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
                result.Errors.GroupBy(p => p.PropertyName).ToList().ForEach(item => dict.Add(item.Key, item.Distinct().Select(e => e.ErrorMessage).ToList()));
                return ResponseResultDto<bool>.MultiError(dic: dict, message: "error");
            }
            var claims = _tokenHandler.GetTokenData(_httpContextAccessor.HttpContext.Request);
            if (claims != null && claims.Any())
            {
                return ResponseResultDto<bool>.InvalidData(result: false, message: _messageResourceReader.GetMessage(ResourcesMessageKey.AlreadAuthorized));
            }
            else
            {
                User userObj = _autoMapper.Map<User>(userRegistrationDTO.Data);
                await _unitOfWork.StartTransactionScope();
                var user = await _userRepository.Add(userObj);
                await _unitOfWork.Complete();

                var cartObj = new Cart()
                {
                    Total = 0,
                    UserId = user.Id
                };
                await _cartRepository.Add(cartObj);
                await _unitOfWork.TransactionScopeComplete();
                return ResponseResultDto<bool>.Success(result: true, message: _messageResourceReader.GetMessage(ResourcesMessageKey.Successfully));
            }
        }


    }
}
