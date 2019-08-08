using Centa.Monitor.Infrastructure.Model;
using Centa.Monitor.Infrastructure.Model.System;
using Centa.Monitor.WebApi.ModelBinder;

namespace Centa.Monitor.WebApi.Token
{
    /// <summary>
    /// token的生成和解析
    /// </summary>
    public interface ITokenVerify
    {
        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string WriteToken(UserModel user);

        /// <summary>
        /// 解析token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        TokenModel ReadToken(string token);
    }
}