#region using
using Centa.Monitor.ViewModel.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using Centa.Monitor.Infrastructure.Model.Monitor;
using Centa.Monitor.ApplicationService.Interface.Monitor;
using System;
#endregion

namespace Centa.Monitor.WebApi.Controllers.Monitor
{
    #region upload log and get screen shot image
    /// <summary>
    /// 上传日志，获取截图
    /// </summary>
    [ApiVersion("1.1")]
    [Route("api/monitor/v{version:apiVersion}/upload-log/")]
    public class UploadLogController : Controller
    {
        #region private
        private readonly IBehaviorService _behaviorService;

        private readonly IJavascriptErrorService _javascriptErrorService;

        private readonly ICustomerPVService _customerPVService;

        private readonly ILoadPageService _loadPageService;

        private readonly IResourceLoadService _resourceLoadService;

        private readonly IHttpLogService _httpLogService;

        private readonly IExtendBehaviorService _extendBehaviorService;

        private readonly IScreenShotService _screenShotService;
        #endregion

        #region ioc
        public UploadLogController(IBehaviorService behaviorService
            , IJavascriptErrorService javascriptErrorService
            , ICustomerPVService customerPVService
            , ILoadPageService loadPageService
            , IResourceLoadService resourceLoadService
            , IHttpLogService httpLogService
            , IExtendBehaviorService extendBehaviorService
            , IScreenShotService screenShotService)
        {
            _behaviorService = behaviorService;
            _javascriptErrorService = javascriptErrorService;
            _customerPVService = customerPVService;
            _loadPageService = loadPageService;
            _resourceLoadService = resourceLoadService;
            _httpLogService = httpLogService;
            _extendBehaviorService = extendBehaviorService;
            _screenShotService = screenShotService;
        }
        #endregion

        #region upload log
        /// <summary>
        /// 上传日志
        /// </summary>
        /// <param name="data">@Order=3,计提调整请求实体</param>
        /// <returns></returns>
        [HttpPost("")]
        [SwaggerOperation(Tags = new[] { "上传日志" })]
        [SwaggerResponse(200, Description = "成功")]
        public IActionResult UploadLog(string data)
        {
            string clientIpString = "";
            string province = "";
            string city = "";
            dynamic param = JsonConvert.DeserializeObject(data.Replace(" \": Script error.", "script error"));
            var logArray=param.logInfo.ToString().Split("$$$");
            #region for
            for (var i=0;i< logArray.Length;i++)
            {
                if(string.IsNullOrEmpty(logArray[i]))
                {
                    continue;
                }
                dynamic logInfo = JsonConvert.DeserializeObject(logArray[i]);
                logInfo.monitorIp = clientIpString;
                logInfo.province = province;
                logInfo.city = city;
                switch (logInfo.uploadType.ToString())
                {
                    case "ELE_BEHAVIOR":
                        _behaviorService.Add(logInfo.ToObject<BehaviorModel>());
                        break;
                    case "JS_ERROR":
                        _javascriptErrorService.Add(logInfo.ToObject<JavascriptErrorModel>());
                        break;
                    case "RESOURCE_LOAD":
                        _resourceLoadService.Add(logInfo.ToObject<ResourceLoadModel>());
                        break;
                    case "HTTP_LOG":
                        _httpLogService.Add(logInfo.ToObject<HttpLogModel>());
                        break;
                    case "SCREEN_SHOT":
                        _screenShotService.Add(logInfo.ToObject<ScreenShotModel>());
                        break;
                    case "CUSTOMER_PV":
                        _customerPVService.Add(logInfo.ToObject<CustomerPVModel>());
                        break;
                    case "LOAD_PAGE":
                        _loadPageService.Add(logInfo.ToObject<LoadPageModel>());
                        break;
                    default:
                        _extendBehaviorService.Add(logInfo.ToObject<ExtendBehaviorModel>());
                        break;
                }
            }
            #endregion
            return Ok(new ResponseViewModel() {});
        }
        #endregion

        #region get image
        /// <summary>
        /// 图片
        /// </summary>
        /// <param name="keyId"></param>
        /// <returns></returns>
        [HttpGet("image")]
        [SwaggerOperation(Tags = new[] { "图片预览" })]
        public IActionResult GetImage(Guid keyId)
        {
            var path= _screenShotService.GetImagePath(keyId);
            if(!global::System.IO.File.Exists(path))
            {
                return base.Ok(new DataResponseViewModel() { Data = new { ErrorMessage = "文件不存在" } });
            }
            var image = global::System.IO.File.OpenRead(path);
            return File(image, "image/jpeg");
        }
        #endregion
    }
    #endregion
}