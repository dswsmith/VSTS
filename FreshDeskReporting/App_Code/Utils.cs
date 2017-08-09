using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FreshDeskReporting
{
    class Utils
    {
        public static string GetEnumDescription(object enumValue)
        {
            string defDesc = enumValue.ToString();

            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());
            if (null != fi)
            {
                object[] attrs = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return defDesc;
        }
    }
}
