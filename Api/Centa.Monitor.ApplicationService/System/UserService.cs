using Centa.Monitor.ApplicationService.Interface.System;
using static Centa.Monitor.Common.CommonHelper;
using Centa.Monitor.Infrastructure.General.Page;
using Centa.Monitor.Infrastructure.Interfaces;
using System.Collections.Generic;
using System;
using System.Text;
using Centa.Monitor.Dto.System.User;
using Centa.Monitor.Infrastructure.Model.System;
using AutoMapper;
using Dapper;
using Novell.Directory.Ldap;
using Centa.Monitor.Common.Interface;
using System.Linq;

namespace Centa.Monitor.ApplicationService.System
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IRepository<UserModel> _repository;

        private IUnitOfWork _unitOfWork;

        public UserService(IRepository<UserModel> userRepository, 
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = userRepository;
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <typeparam name="UserListModel">用户列表实体</typeparam>
        /// <param name="searchDto">用户列表查询实体</param>
        /// <returns></returns>
        public PageDataView<UserListDto> GetList(UserSearchDto searchDto)
        {
            PageDataView<UserListDto> result = new PageDataView<UserListDto>() { };
            StringBuilder select = new StringBuilder();
            StringBuilder where = new StringBuilder();
            DynamicParameters parameters = new DynamicParameters();
            select.Append("SELECT * FROM [User]  ");
            where.Append("WHERE 1=1 ");
            if (!string.IsNullOrEmpty(searchDto.DomainAccount))
            {
                where.Append($"AND  DomainAccount LIKE @DomainAccount ");
                parameters.Add("@DomainAccount", "%" + searchDto.DomainAccount + "%");
            }
            if (!string.IsNullOrEmpty(searchDto.RealName))
            {
                where.Append($"AND  RealName LIKE @RealName ");
                parameters.Add("@RealName", "%" + searchDto.RealName + "%");
            }
            Dictionary<string, bool> ordery = new Dictionary<string, bool>();
            ordery.Add("createdAt", false);
            result = _unitOfWork.GetPageData<UserListDto>(select.ToString() + where.ToString(), ordery, searchDto.PageIndex, searchDto.PageSize, parameters);
            return result;
        }

        /// <summary>
        /// 新增员工
        /// </summary>
        /// <param name="accountModel"></param>
        /// <returns></returns>
        public int Add(UserAddDto userAddDto)
        {
            var userModel = _repository.GetBy(@"SELECT * FROM dbo.[User]  WHERE DomainAccount=@DomainAccount and IsUse=1",
                           new { DomainAccount = userAddDto.DomainAccount }).FirstOrDefault();
            if(userModel!=null)
            {
                return -1;
            }
            var model = Mapper.Map<UserModel>(userAddDto);
            model.KeyId = Guid.NewGuid();
            model.CreatedAt = model.UpdatedAt = DateTime.Now;
            _repository.Add(model);
            return 1;
        }

        /// <summary>
        /// 校验域账号
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetCnName(string userName)
        {
            return "user";
        }

       
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        public void Update(UpdateUserDto updateDto)
        {
            var userModel = _repository.GetByKeyId(updateDto.KeyId);
            var createTime=userModel.CreatedAt;
            userModel = Mapper.Map<UserModel>(updateDto);
            userModel.UpdatedAt = DateTime.Now;
            userModel.CreatedAt= createTime;
            _repository.Update(userModel);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="deleteUserDto"></param>
        /// <returns></returns>
        public void Delete(DeleteUserDto deleteUserDto)
        {
            var userModel = _repository.GetByKeyId(deleteUserDto.KeyId);
            _repository.Remove(userModel);
        }


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns>-1不存在,-2密码错误，1成功</returns>
        public int Login(LoginDto login)
        {
            var loginResult = 0;
            var userModel = _repository.GetBy(@"SELECT * FROM dbo.[User]  WHERE DomainAccount=@DomainAccount ",
                            new { DomainAccount = login.Account }).FirstOrDefault();

            #region 数据库里不存在用户
            if (userModel == null)
            {
                loginResult = -1;
            }
            #endregion
            #region 存在
            else
            {
                #region 用户无效或者删除
                if (userModel.IsUse==0)
                {
                    loginResult = -1;
                }
                #endregion
                else
                {
                    string domainName = "centaline.com.cn";
                    string userDn = $"{login.Account}@{domainName}";
                    IResult<int> result = Execute<int>(() => {
                        using (var connection = new LdapConnection { SecureSocketLayer = false })
                        {
                            connection.Connect(domainName, LdapConnection.DEFAULT_PORT);
                            connection.Bind(userDn, login.Password);
                            if (connection.Bound)
                            {
                                return 1;
                            }
                        }
                        return -1;
                    });
                    if (result.Status == 200 && result.Data == 1)
                    {
                        loginResult = 1;
                    }
                    else
                    {
                        loginResult = -2;
                    }

                }

            }
            #endregion
            return loginResult;
        }

        public UserModel GetAccount(string userName) => _repository.GetBy(
               $@"SELECT * FROM dbo.[User]  WHERE DomainAccount=@DomainAccount AND IsUse=1 "
            , new { DomainAccount = userName }).FirstOrDefault();


    }

}