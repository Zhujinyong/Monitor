using Centa.Monitor.Infrastructure.Enum;
using System;
using System.ComponentModel;
using System.Reflection;

namespace Centa.Monitor.Common
{
    public static class EnumExtension
    {
        public static string ToDescription(this StatusCodeEnum myEnum)
        {
            Type t = myEnum.GetType();
            FieldInfo info = t.GetField(myEnum.ToString());
            DescriptionAttribute description = (DescriptionAttribute)Attribute.GetCustomAttribute(info, typeof(DescriptionAttribute));
            if (description != null)
                return description.Description;
            else
                return myEnum.ToString();
        }
    }
}
