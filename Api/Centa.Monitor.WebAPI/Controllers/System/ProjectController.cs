#region using
using AutoMapper;
using Centa.Monitor.ApplicationService.Interface.System;
using Centa.Monitor.WebApi.Filter.Action;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using Centa.Monitor.ViewModel.Response;
using Centa.Monitor.WebApi.ModelBinder;
using Centa.Monitor.Dto.System.Project;
using Centa.Monitor.ViewModel.Request.System.Project;
using Centa.Monitor.ViewModel.Response.System.Project;
using Centa.Monitor.Infrastructure.Model.System.Project;
#endregion

namespace Centa.Monitor.WebAPI.Controllers.System
{
    #region project
    /// <summary>
    /// 项目管理
    /// </summary>
    [ApiVersion("1.1")]
    [Route("api/monitor/v{version:apiVersion}/system/project/")]
    [TokenValidate]
    public class ProjectController : Controller
    {
        #region private
        private readonly IProjectService _projectService;
        #endregion

        #region ioc
        public ProjectController(IProjectService ProjectService)
        {
            _projectService = ProjectService;
        }
        #endregion

        #region add
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="query">@Order=3,新增实体</param>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "项目管理" })]
        [SwaggerResponse(200, description: "成功")]
        public IActionResult Add([FromBody]AddProjectRequestViewModel query, TokenModel tokenModel)
        {
            var dto = Mapper.Map<AddProjectDto>(query);
            _projectService.Add(dto);
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
        [HttpGet("projects")]
        [SwaggerOperation(Tags = new[] { "项目管理" })]
        [SwaggerResponse(200, Type = typeof(ProjectListResponseViewModel), Description = "成功")]
        public IActionResult GetProjectList(ProjectRequestViewModel query, TokenModel tokenModel)
        {
            var searchModel = Mapper.Map<SearchProjectDto>(query);
            var list = _projectService.GetList(searchModel);
            return Ok(new PageResponseViewModel() { Data = list.Items, Page = new PageViewModel { PageSize = query.PageSize, Total = list.TotalNum, PageIndex = query.PageIndex } });
        }
        #endregion

        #region update
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="query">@Order=3,编辑项目请求实体</param>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        [HttpPut]
        [SwaggerOperation(Tags = new[] { "项目管理" })]
        public IActionResult Update([FromBody]UpdateProjectRequestViewModel query, TokenModel tokenModel)
        {
            var updateDto = Mapper.Map<UpdateProjectDto>(query);
            _projectService.Update(updateDto);
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
        [SwaggerOperation(Tags = new[] { "项目管理" })]
        [SwaggerResponse(200, Description = "成功")]
        public IActionResult DeleteProject([FromBody]DeleteProjectRequestViewModel query, TokenModel tokenModel)
        {
            var model = Mapper.Map<DeleteProjectDto>(query);
            _projectService.Delete(model);
            return Ok(new ResponseViewModel());
        }
        #endregion
    }
    #endregion
}
