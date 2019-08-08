using Dapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using static Dapper.SqlMapper;

namespace Centa.Monitor.Common
{
    public static class Extensions
    {
        public static dynamic ToDynamic(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
            {
                expando.Add(property.Name, property.GetValue(value));
            }
            return expando as ExpandoObject;
        }

        public static string GetFormatValue(this decimal value, string format = "f2")
        {
            if (value == default(decimal))
            {
                return "0.00";
            }
            return value.ToString(format);
        }

        public static ICustomQueryParameter AsTableValuedParameter<T>(this IEnumerable<T> enumerable, string typeName, IEnumerable<string> orderedColumnNames = null)
        {
            var dataTable = new DataTable();
            if (typeof(T).IsValueType || typeof(T).FullName.Equals("System.String"))
            {
                dataTable.Columns.Add(orderedColumnNames == null ?
                    "NONAME" : orderedColumnNames.First(), typeof(T));
                foreach (T obj in enumerable)
                {
                    dataTable.Rows.Add(obj);
                }
            }
            else
            {
                PropertyInfo[] properties = typeof(T).GetProperties
                    (BindingFlags.Public | BindingFlags.Instance);
                PropertyInfo[] readableProperties = properties.Where
                    (w => w.CanRead).ToArray();
                //if (readableProperties.Length > 1 && orderedColumnNames == null)
                //    throw new ArgumentException("Ordered list of column names  must be provided when TVP contains more than one column");

                var columnNames = (orderedColumnNames ??
                    readableProperties.Select(s => s.Name)).ToArray();
                foreach (string name in columnNames)
                {
                    dataTable.Columns.Add(name, readableProperties.Single
                        (s => s.Name.Equals(name)).PropertyType);
                }

                foreach (T obj in enumerable)
                {
                    dataTable.Rows.Add(
                        columnNames.Select(s => readableProperties.Single
                            (s2 => s2.Name.Equals(s)).GetValue(obj))
                            .ToArray());
                }
            }
            return dataTable.AsTableValuedParameter(typeName);
        }


        public static ICustomQueryParameter MakeListParam(object parameterValue)
        {
            ICustomQueryParameter result = null;
            Type type = parameterValue.GetType();
            if (type.IsGenericType)//判斷是否是泛型
            {
                string t = type.GetGenericArguments()[0].Name; //泛型的類型
                switch (t)
                {
                    case "Guid":
                        List<Guid> guidList = ((List<Guid>)parameterValue).Distinct().ToList();
                        result = AsTableValuedParameter(guidList, "GuidCollectionTVP", new List<string> { "Item" });
                        break;
                    case "Int32":
                        List<int> intList = ((List<int>)parameterValue).Distinct().ToList();
                        result = AsTableValuedParameter(intList, "IntCollectionTVP", new List<string> { "Item" });
                        break;
                    case "String":
                        List<string> stringList = ((List<string>)parameterValue).Distinct().ToList();
                        result = AsTableValuedParameter(stringList, "StringCollectionTVP", new List<string> { "Item" });
                        break;
                }
            }
            return result;
        }


        public static DataTable AsToDataTable<T>(this List<T> list)
        {
            if (list == null)
            {
                throw new Exception("需转换的集合为空");
            }
            Type entityType = list[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();
            DataTable dt = new DataTable();
            foreach (PropertyInfo propertyInfo in entityProperties)
            {
                dt.Columns.Add(propertyInfo.Name, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);
            }
            foreach (object entity in list)
            {
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }

        public static string GetClientUserIp(this HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }
    }
}
