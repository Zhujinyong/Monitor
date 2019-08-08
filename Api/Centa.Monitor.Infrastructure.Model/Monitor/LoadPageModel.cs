using Dapper.Contrib.Extensions;

namespace Centa.Monitor.Infrastructure.Model.Monitor
{
    /// <summary>
    /// 截图
    /// </summary>
    [Table("[dbo].[ScreenShot]")]
    public partial class ScreenShotModel : LogBaseModel
    {
        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 截屏信息
        /// </summary>
        public string ScreenInfo { get; set; }

        /// <summary>
        /// 图片类型
        /// </summary>
        public string ImgType { get; set; }
    }
}

