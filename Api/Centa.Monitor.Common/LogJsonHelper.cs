using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Centa.Monitor.Common
{
    public class LogJsonHelper
    {
        /// <summary>
        /// 对象转换到日志Json - 修改
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">对象1</param>
        /// <param name="obj2">对象2</param>
        /// <returns></returns>
        public static string ConvertToJson<T>(T obj, T obj2) where T : class
        {
            try
            {
                if (obj == null || obj2 == null)
                {
                    return string.Empty;
                }
                List<object> result = new List<object>();
                Type type = obj.GetType();
                int changeCount = 0;
                foreach (PropertyInfo item in type.GetProperties())
                {
                    DescriptionAttribute attribute = item.GetCustomAttribute<DescriptionAttribute>();
                    if (attribute == null)
                    {
                        continue;
                    }
                    IsChangeFlagAttribute changeAttribute = item.GetCustomAttribute<IsChangeFlagAttribute>();
                    if (changeAttribute == null)
                    {
                        UpdateLogInfo info = new UpdateLogInfo();
                        info.FiledName = item.Name;
                        info.FiledCnName = attribute.Description;
                        info.OldValue = GetFormatValue(item, obj);
                        info.NewValue = GetFormatValue(item, obj2);
                        if (info.OldValue.ToString() != info.NewValue.ToString())
                        {
                            changeCount++;
                            result.Add(info);
                        }
                    }
                    else
                    {
                        OtherLogInfo info = new OtherLogInfo();
                        info.FiledName = item.Name;
                        info.FiledCnName = attribute.Description;
                        info.Value = GetFormatValue(item, obj);
                        result.Add(info);
                    }
                }
                if (changeCount > 0)
                {
                    return JsonConvert.SerializeObject(result);
                }
            }
            catch
            { }
            return string.Empty;
        }

        /// <summary>
        /// 批量操作日志类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static string ConvertToJson<T>(List<T> obj, List<T> obj2) where T : class
        {
            try
            {
                if (obj == null || obj2 == null || obj.Count != obj2.Count)
                {
                    return string.Empty;
                }
                Type type = typeof(T);
                int changeCount = 0;
                List<List<object>> result = new List<List<object>>();
                for (int i = 0; i < obj.Count; i++)
                {
                    List<object> temp = new List<object>();
                    foreach (PropertyInfo item in type.GetProperties())
                    {
                        DescriptionAttribute attribute = item.GetCustomAttribute<DescriptionAttribute>();
                        if (attribute == null)
                        {
                            continue;
                        }
                        IsChangeFlagAttribute changeAttribute = item.GetCustomAttribute<IsChangeFlagAttribute>();
                        if (changeAttribute == null)
                        {
                            UpdateLogInfo info = new UpdateLogInfo();
                            info.FiledName = item.Name;
                            info.FiledCnName = attribute.Description;
                            info.OldValue = GetFormatValue(item, obj[i]);
                            info.NewValue = GetFormatValue(item, obj2[i]);
                            if (info.OldValue.ToString() != info.NewValue.ToString())
                            {
                                changeCount++;
                                temp.Add(info);
                            }
                        }
                        else
                        {
                            OtherLogInfo info = new OtherLogInfo();
                            info.FiledName = item.Name;
                            info.FiledCnName = attribute.Description;
                            info.Value = GetFormatValue(item, obj[i]);
                            temp.Add(info);
                        }
                    }
                    if (temp.Count > 0)
                    {
                        result.Add(temp);
                    }
                }
                if (changeCount > 0)
                {
                    return JsonConvert.SerializeObject(result);
                }
            }
            catch
            {

            }
            return string.Empty;
        }

        /// <summary>
        /// 对象转换到日志Json - 其它
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string ConvertToJson<T>(T obj) where T : class
        {
            try
            {
                if (obj == null)
                {
                    return string.Empty;
                }
                List<OtherLogInfo> result = new List<OtherLogInfo>();
                Type type = obj.GetType();
                foreach (PropertyInfo item in type.GetProperties())
                {
                    DescriptionAttribute attribute = item.GetCustomAttribute<DescriptionAttribute>();
                    if (attribute != null)
                    {
                        OtherLogInfo info = new OtherLogInfo();
                        info.FiledName = item.Name;
                        info.FiledCnName = attribute.Description;
                        info.Value = GetFormatValue(item, obj);
                        result.Add(info);
                    }
                }
                if (result.Count > 0)
                {
                    return JsonConvert.SerializeObject(result);
                }
            }
            catch
            { }
            return string.Empty;
        }

        /// <summary>
        /// List对象转换到日志Json - 其它
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string ConvertToJson<T>(List<T> obj) where T : class
        {
            try
            {
                if (obj == null || obj.Count == 0)
                {
                    return string.Empty;
                }
                Type type = typeof(T);
                List<List<OtherLogInfo>> result = new List<List<OtherLogInfo>>();
                for (int i = 0; i < obj.Count; i++)
                {
                    List<OtherLogInfo> temp = new List<OtherLogInfo>();
                    foreach (PropertyInfo item in type.GetProperties())
                    {
                        DescriptionAttribute attribute = item.GetCustomAttribute<DescriptionAttribute>();
                        if (attribute != null)
                        {
                            OtherLogInfo info = new OtherLogInfo();
                            info.FiledName = item.Name;
                            info.FiledCnName = attribute.Description;
                            info.Value = GetFormatValue(item, obj[i]);
                            temp.Add(info);
                        }
                    }
                    if (temp.Count > 0)
                    {
                        result.Add(temp);
                    }
                }
                if (result.Count > 0)
                {
                    return JsonConvert.SerializeObject(result);
                }
            }
            catch
            { }
            return string.Empty;
        }

        /// <summary>
        /// 获取格式化Value
        /// </summary>
        /// <param name="property"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object GetFormatValue(PropertyInfo property, object obj)
        {
            DataStringFormatAttribute formatAttribute = property.GetCustomAttribute<DataStringFormatAttribute>();
            object result = property.GetValue(obj);
            if (result == null)
            {
                return string.Empty;
            }
            if (formatAttribute != null && !string.IsNullOrEmpty(formatAttribute.DataFormat))
            {
                string format = formatAttribute.DataFormat;
                string typeName = property.PropertyType.FullName.ToLower();
                if (typeName.Contains("system.datetime"))
                {
                    result = Convert.ToDateTime(result).ToString(format);
                }
                else if (typeName.Contains("system.decimal"))
                {
                    result = Convert.ToDecimal(result).ToString(format);
                }
            }
            return result;
        }
    }

    public class UpdateLogInfo
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string FiledName { get; set; }

        /// <summary>
        /// 字段中文描述
        /// </summary>
        public string FiledCnName { get; set; }

        /// <summary>
        /// 原值
        /// </summary>
        public object OldValue { get; set; }

        /// <summary>
        /// 新值
        /// </summary>
        public object NewValue { get; set; }
    }

    public class OtherLogInfo
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string FiledName { get; set; }

        /// <summary>
        /// 字段中文描述
        /// </summary>
        public string FiledCnName { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class IsChangeFlagAttribute : Attribute
    {
        public IsChangeFlagAttribute()
        {

        }
    }

    /// <summary>
    /// 字符串格式特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DataStringFormatAttribute : Attribute
    {
        public string DataFormat { get; set; }

        public DataStringFormatAttribute(string format)
        {
            this.DataFormat = format;
        }
    }
}
