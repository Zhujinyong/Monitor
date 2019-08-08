#region using
using Centa.Monitor.ViewModel.Response;
using Centa.Monitor.WebApi.Token;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using AutoMapper;
using Centa.Monitor.Infrastructure.Model.System;
using Centa.Monitor.ApplicationService.Interface.System;
using Centa.Monitor.WebApi.Filter.Action;
using Centa.Monitor.WebApi.ModelBinder;
using Centa.Monitor.Dto.System.User;
using Centa.Monitor.ViewModel.Request.System.User;
using Centa.Monitor.Infrastructure.Enum;
#endregion

namespace Centa.Monitor.WebApi.Controllers.System
{
    #region login infomarion logout
    /// <summary>
    /// 登录
    /// </summary>
    [ApiVersion("1.1")]
    [Route("api/monitor/v{version:apiVersion}/system/account")]
    public class LoginController : Controller
    {
        #region private
        private readonly ITokenVerify _tokenService;

        private readonly IUserService _userService;
        #endregion 

        #region ioc
        public LoginController(ITokenVerify tokenService, 
            IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }
        #endregion 

        #region login
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost("token")]
        [SwaggerOperation(Tags = new[] { "登录/登出/账号信息" })]
        [SwaggerResponse(200, Type = typeof(TokenResponseViewModel), Description = "成功")]
        [SwaggerResponse(808, Description = "账号不存在")]
        [SwaggerResponse(809, Description = "密码错误")]
        public IActionResult GetToken([FromBody]LoginRequestViewModel loginRequestDto)
        {
            var login = Mapper.Map<LoginDto>(loginRequestDto);
            var state = _userService.Login(login);
            if (state == -1 || state == -2)
            {              
                return Ok(new ResponseViewModel(StatusCodeEnum.AccountError,"账号或密码错误")  );
            }          
            var tokenString = _tokenService.WriteToken(new UserModel() { DomainAccount = loginRequestDto.Account });
          
            return Ok(new DataResponseViewModel() { Data = new { errorMessage = "", token = tokenString } });
        }
        #endregion

        #region infomation
        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        [TokenValidate]
        [HttpGet("infomation")]
        public IActionResult GetUserInfo(TokenModel tokenModel)
        {
            var account = _userService.GetAccount(tokenModel.DomainAccount);
            return Ok(new DataResponseViewModel()
            {
                Data = new 
                {
                    RealName = account.RealName
                }
            });
        }
        #endregion

        #region logout
        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        [SwaggerOperation(Tags = new[] { "登录/登出/账号信息" })]
        [SwaggerResponse(200, Description = "成功")]
        public IActionResult LogOut()
        {
            //TODO 后续完善
            return Ok(new ResponseViewModel());
        }
        #endregion
    }
    #endregion
}