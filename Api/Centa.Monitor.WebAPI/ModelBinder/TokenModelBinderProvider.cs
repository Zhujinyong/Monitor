using Centa.Monitor.ApplicationService.Interface;
using Centa.Monitor.ApplicationService.Interface.System;
using Centa.Monitor.WebApi.Token;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Centa.Monitor.WebApi.ModelBinder
{
    /// <summary>
    /// Provider
    /// </summary>
    public class TokenModelBinderProvider : IModelBinderProvider
    {
        private ITokenVerify _tokenService;

        private IUserService _userService;

        public TokenModelBinderProvider(ITokenVerify tokenService, IUserService accountService)
        {
            _tokenService = tokenService;
            _userService = accountService;
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(TokenModel))
                return new TokenModelBinder(_tokenService, _userService);
            return null;
        }
    }
}