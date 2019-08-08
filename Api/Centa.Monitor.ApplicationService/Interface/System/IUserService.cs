using Centa.Monitor.Infrastructure.General.Page;
using Centa.Monitor.Dto.System.User;
using Centa.Monitor.Infrastructure.Model.System;

namespace Centa.Monitor.ApplicationService.Interface.System
{
    public interface IUserService
    {
        PageDataView<UserListDto> GetList(UserSearchDto searchDto);

        /// <summary>
        /// 新增员工
        /// </summary>
        /// <param name="accountModel"></param>
        /// <returns></returns>
        int Add(UserAddDto userAddDto);

        /// <summary>
        /// 校验域账号
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        string GetCnName(string userName);


        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        void Update(UpdateUserDto updateDto);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="deleteUserDto"></param>
        /// <returns></returns>
        void Delete(DeleteUserDto deleteUserDto);


        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        int Login(LoginDto login);

        /// <summary>
        /// 获取账号
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        UserModel GetAccount(string userName);
    }
}
