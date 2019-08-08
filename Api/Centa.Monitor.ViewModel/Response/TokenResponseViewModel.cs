
namespace Centa.Monitor.ViewModel.Response
{
    /// <summary>
    /// token
    /// </summary>
    public class TokenResponseViewModel
    {
        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 有效期，单位秒
        /// </summary>
        public int ExpireIn { get; set; }
    }
}