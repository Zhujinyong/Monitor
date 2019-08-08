using System.ComponentModel;

namespace Centa.Monitor.Infrastructure.Enum
{
    /// <summary>
    /// 状态码
    /// </summary>
    public enum StatusCodeEnum
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Ok = 200,

        /// <summary>
        /// 内部错误
        /// </summary>
        [Description("内部错误")]
        InternalError = 500,

        /// <summary>
        /// 账号不存在或密码错误
        /// </summary>
        [Description("账号不存在或密码错误")]
        AccountError = 801,

        /// <summary>
        /// token过期
        /// </summary>
        [Description("token过期")]
        TokenExpired = 802,

        /// <summary>
        /// token无效
        /// </summary>
        [Description("token无效")]
        TokenInvalid = 803,

        /// <summary>
        /// 用户被禁用
        /// </summary>
        [Description("用户被禁用")]
        InvalidUser = 804,


        /// <summary>
        /// 参数错误
        /// </summary>
        [Description("参数错误")]
        ParamInvalid = 805,

        /// <summary>
        /// 没有权限
        /// </summary>
        [Description("没有权限")]
        UnAuthority = 806,

        /// <summary>
        /// 模板文件不存在
        /// </summary>
        [Description("模板文件不存在")]
        TemplateFileNotFound = 807,

        /// <summary>
        /// 账号不存在
        /// </summary>
        [Description("账号不存在")]
        AccountNotExist = 808,

        /// <summary>
        /// 密码错误
        /// </summary>
        [Description("密码错误")]
        PasswordError = 809,

        /// <summary>
        /// 用户名重复
        /// </summary>
        [Description("用户名重复")]
        AccountRepetition = 901,

        /// <summary>
        /// 此用户不能删除
        /// </summary>
        [Description("此用户不能删除")]
        AccountNoDelete = 903,

        /// <summary>
        /// 角色名称重复
        /// </summary>
        [Description("角色名称重复")]
        RoleNameRepetition = 904,

        /// <summary>
        /// 用户名不能为空
        /// </summary>
        [Description("用户名不能为空")]
        UserNameIsNULL = 905,

        /// <summary>
        /// 用户账号不能为空
        /// </summary>
        [Description("用户账号不能为空")]
        AccountIsNULL = 906,

        /// <summary>
        /// 所属角色不能为空
        /// </summary>
        [Description("所属角色不能为空")]
        UserRoleKeyIdIsNULL = 907,

        /// <summary>
        /// 所属公司不能为空
        /// </summary>
        [Description("所属公司不能为空")]
        SubCompanyKeyIsNULL = 908,

        /// <summary>
        ///域账号不存在
        /// </summary>
        [Description("域账号不存在")]
        AccountNo = 909

    }
}