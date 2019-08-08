
namespace Centa.Monitor.Infrastructure
{
    /// <summary>
    /// 对应appsetting.json的AppSetting节点
    /// </summary>
    public class AppSetting
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 是否使用MiniProfiler
        /// </summary>
        public bool UseMiniProfiling { get; set; }

        /// <summary>
        /// 数据库只读连接字符串
        /// </summary>
        public string ReadOnlyConnectionString { get; set; }
       
        /// <summary>
        /// 文件服务器地址
        /// </summary>
        public string FileServer { set; get; }

        /// <summary>
        /// DB执行超时时间
        /// </summary>
        public int CommandTimeout { get; set; }

        /// <summary>
        /// RAS非对称加密公钥
        /// </summary>
        public string RSAPublicKey { get; set; }

        /// <summary>
        /// RAS非对称加密私钥
        /// </summary>
        public string RSAPrivateKey { get; set; }
    }
}