#region using
using AutoMapper;
using Centa.Monitor.ApplicationService.Interface.System;
using Centa.Monitor.WebApi.Filter.Action;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using Centa.Monitor.ViewModel.Response;
using Centa.Monitor.WebApi.ModelBinder;
using Centa.Monitor.Infrastructure.Enum;
using Centa.Monitor.Dto.System.User;
using Centa.Monitor.ViewModel.Request.System.User;
using Centa.Monitor.ViewModel.Response.System.User;
#endregion

namespace Centa.Monitor.WebAPI.Controllers.System
{
    #region user
    /// <summary>
    /// 用户管理
    /// </summary>
    [ApiVersion("1.1")]
    [Route("api/monitor/v{version:apiVersion}/system/user/")]
    [TokenValidate]
    public class UserController : Controller
    {
        #region private
        private readonly IUserService _userService;
        #endregion

        #region ioc
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region add
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="query">@Order=3,新增用户请求实体</param>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "用户管理" })]
        [SwaggerResponse(200, description: "成功")]
        public IActionResult Add([FromBody]AddUserRequestViewModel query, TokenModel tokenModel)
        {
            var userAddDto = Mapper.Map<UserAddDto>(query);
            var cnName = _userService.GetCnName(userAddDto.DomainAccount);
            if (string.IsNullOrEmpty(cnName))
            {
                return Ok(new ResponseViewModel(StatusCodeEnum.AccountNo, "域账号不存在"));
            }
            var result=_userService.Add(userAddDto);
            if (result==-1)
            {
                return Ok(new ResponseViewModel(StatusCodeEnum.AccountRepetition, "账号不能重复"));
            }
            return Ok(new ResponseViewModel());
        }
        #endregion

        #region list
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="query"></param>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        [HttpGet("users")]
        [SwaggerOperation(Tags = new[] { "用户管理" })]
        [SwaggerResponse(200, Type = typeof(UserListResponseViewModel), Description = "成功")]
        public IActionResult GetUserList(UserRequestViewModel query, TokenModel tokenModel)
        {
            var searchModel = Mapper.Map<UserSearchDto>(query);
            var list = _userService.GetList(searchModel);
            return Ok(new PageResponseViewModel() { Data = list.Items, Page = new PageViewModel { PageSize = query.PageSize, Total = list.TotalNum, PageIndex = query.PageIndex } });
        }
        #endregion

        #region update
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="query">@Order=3,编辑用户请求实体</param>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        [HttpPut]
        [SwaggerOperation(Tags = new[] { "用户管理" })]
        public IActionResult Update([FromBody]UpdateUserRequestViewModel query, TokenModel tokenModel)
        {
            var updateDto = Mapper.Map<UpdateUserDto>(query);
            _userService.Update(updateDto);
            return Ok(new ResponseViewModel());
        }
        #endregion

        #region delete
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="query"></param>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        [HttpDelete]
        [SwaggerOperation(Tags = new[] { "用户管理" })]
        [SwaggerResponse(200, Description = "成功")]
        public IActionResult DeleteUser([FromBody]DeleteUserRequestViewModel query, TokenModel tokenModel)
        {
            var model = Mapper.Map<DeleteUserDto>(query);
            _userService.Delete(model);
            return Ok(new ResponseViewModel());
        }
        #endregion
    }
    #endregion
}
