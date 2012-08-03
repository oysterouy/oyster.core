using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using Oyster.Core.Orm;

namespace System
{
    public static partial class Extends
    {
        #region 验证

        /// <summary>
        /// 是否是日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsDateTime(this string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return false;
            }
            bool bk = false;
            Regex reg = new Regex("^\\d{4}-\\d{1,2}-\\d{1,2}(\\s*\\d{1,2}(:\\d{1,2}(:\\d{1,2}(\\.\\d{1,7})?)?)?)?$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if (reg.IsMatch(date))
            {
                DateTime dt = DateTime.MinValue;
                if (DateTime.TryParse(date, out dt))
                {
                    bk = true;
                }
            }

            return bk;
        }
        /// <summary>
        /// 是否是16位整数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsInt16(this string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return false;
            }
            bool bk = false;
            Regex reg = new Regex("^-?\\d+$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if (reg.IsMatch(data))
            {
                Int16 dt = 0;
                if (Int16.TryParse(data, out dt))
                {
                    bk = true;
                }
            }

            return bk;
        }

        /// <summary>
        /// 是否是32位整数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsInt32(this string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return false;
            }
            bool bk = false;
            Regex reg = new Regex("^-?\\d+$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if (reg.IsMatch(data))
            {
                Int32 dt = 0;
                if (Int32.TryParse(data, out dt))
                {
                    bk = true;
                }
            }

            return bk;
        }

        /// <summary>
        /// 是否是64位整数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsInt64(this string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return false;
            }
            bool bk = false;
            Regex reg = new Regex("^-?\\d+$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if (reg.IsMatch(data))
            {
                Int64 dt = 0;
                if (Int64.TryParse(data, out dt))
                {
                    bk = true;
                }
            }

            return bk;
        }

        /// <summary>
        /// 是否是Decimal,一般用于验证带小数的数字
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsDecimal(this string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return false;
            }
            bool bk = false;
            Regex reg = new Regex("^-?\\d+(\\.\\d+)?$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if (reg.IsMatch(data))
            {
                Decimal dt = 0;
                if (Decimal.TryParse(data, out dt))
                {
                    bk = true;
                }
            }

            return bk;
        }

        /// <summary>
        /// 是否是IsNumber,验证数字，负数，正数，带小数，不带小数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsNumber(this string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return false;
            }
            bool bk = false;
            Regex reg = new Regex("^-?\\d+(\\.\\d+)?$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if (reg.IsMatch(data))
            {
                Double dt = 0;
                if (Double.TryParse(data, out dt))
                {
                    bk = true;
                }
            }

            return bk;
        }

        /// <summary>
        /// 是否是IsEmail,验证是否是邮箱格式
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsEmail(this string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return false;
            }
            bool bk = false;
            Regex reg = new Regex("^[0-9A-Za-z_\\.\\-]+@[0-9A-Za-z_\\.\\-]+\\.[A-Za-z]{2,4}$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            bk = reg.IsMatch(data);
            return bk;
        }

        /// <summary>
        /// 是否是用户姓名-汉字，英文,.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsUserName(this string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return false;
            }
            bool bk = false;
            Regex reg = new Regex("^[A-Za-z\u3E00-\u9FA5\\.]+$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            bk = reg.IsMatch(data);
            return bk;
        }

        public static bool IsNullableType(this　Type type)
        {
            return (((type != null) && type.IsGenericType) &&
             (type.GetGenericTypeDefinition() == typeof(Nullable<>)));
        }
        #endregion

        #region 转换

        public static object ChangeType(this object o, Type tp)
        {
            if (tp.FullName.Contains("System.Nullable"))
            {
                tp = tp.GetNonNullableType();
            }
            object d = null;
            if (tp.Equals(typeof(string)))
            {
                return o != null ? o.ToString() : "";
            }
            else
            {
                d = Activator.CreateInstance(tp);
            }
            if (Convert.GetTypeCode(o).Equals(Convert.GetTypeCode(d)))
            {
                return o;
            }
            else
            {
                return ChangeType(o, Convert.GetTypeCode(d));
            }
        }

        public static object ChangeType(this object o, TypeCode tp)
        {
            int cd = (int)tp;
            if (o != null)
            {
                int c = (int)Convert.GetTypeCode(o);
                if (c == cd)
                {
                    return o;
                }
                object d = o;
                //Bool
                if (c == 3)
                {
                    if (cd == 3)
                    {
                        return o.Equals(true) ? true : false;
                    }
                    if (cd == 4)
                    {
                        return o.Equals(true) ? (char)1 : (char)0;
                    }
                    if (cd > 4 && cd < 16)
                    {
                        return Convert.ChangeType(o, tp);
                    }
                    if (cd == 16)
                    {
                        return o.Equals(true) ? DateTime.MaxValue : DateTime.MinValue;
                    }
                    if (cd == 18)
                    {
                        return o.Equals(true) ? "true" : "false";
                    }
                }
                if (c == 4)
                {
                    int ic = (int)(char)o;
                    if (cd == 3)
                    {
                        return ic > 0 ? true : false;
                    }
                    if (cd == 4)
                    {
                        return (char)ic;
                    }
                    if (cd > 4 && cd < 16)
                    {
                        return Convert.ChangeType(ic, tp);
                    }
                    if (cd == 16)
                    {
                        return ic > 0 ? DateTime.MaxValue : DateTime.MinValue;
                    }
                    if (cd == 18)
                    {
                        return ic > 0 ? "true" : "false";
                    }
                }
                //数字转换
                if (c > 4 && c < 16)
                {
                    if (cd == 3)
                    {
                        return Convert.ToInt32(o) > 0 ? true : false;
                    }
                    if (cd == 4)
                    {
                        return Convert.ToInt32(o) > 0 ? (char)1 : (char)0;
                    }
                    if (cd > 4 && cd < 16)
                    {
                        double num = Convert.ToDouble(o);
                        return num.ToNumber(tp);
                    }
                    if (cd == 16)
                    {
                        return new DateTime(Convert.ToInt64(o));
                    }
                    if (cd == 18)
                    {
                        return o.ToString();
                    }
                }
                if (c == 16)
                {
                    DateTime dt = (DateTime)o;
                    if (cd == 3)
                    {
                        return dt.Ticks > 0 ? true : false;
                    }
                    if (cd == 4)
                    {
                        return dt.Ticks > 0 ? (char)1 : (char)0;
                    }
                    if (cd > 4 && cd < 16)
                    {
                        return Convert.ChangeType(dt.Ticks, tp);
                    }
                    if (cd == 16)
                    {
                        return dt;
                    }
                    if (cd == 18)
                    {
                        return dt.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
                    }
                }
                if (c == 18)
                {
                    string s = o.ToString();
                    if (!string.IsNullOrEmpty(s))
                    {
                        if (cd == 3)
                        {
                            return s.ToLower().Equals("true") ? true : false;
                        }
                        if (cd == 4)
                        {
                            return s[0];
                        }
                        if ((cd > 4 && cd < 16) && s.IsNumber())
                        {
                            double num = Convert.ToDouble(o);
                            return num.ToNumber(tp);
                        }
                        if (cd == 16 && s.IsDateTime())
                        {
                            return Convert.ToDateTime(s);
                        }
                        if (cd == 18)
                        {
                            return s;
                        }
                    }
                }
            }
            //以上类型都不符合，返回对应类型的默认值
            if (cd < 2)
            {
                //empty,object 不为空返回ToString()值
                return o != null ? o.ToString() : null;
            }
            if (cd == 2)
            {
                return DBNull.Value;
            }
            if (cd == 3)
            {
                return false;
            }
            if (cd == 4)
            {
                return '\0';
            }
            if (cd > 4 && cd < 16)
            {
                return Convert.ChangeType(0, tp);
            }
            if (cd == 16)
            {
                return DateTime.MinValue;
            }
            if (cd == 18)
            {
                return String.Empty;
            }
            //应该是到不了这里的
            return null;
        }

        public static object ToNumber(this double num, TypeCode tpcode)
        {
            switch (tpcode)
            {
                case TypeCode.Byte:
                    if (num >= byte.MinValue && num <= byte.MaxValue)
                    {
                        return Convert.ToByte(num);
                    }
                    break;
                case TypeCode.SByte:
                    if (num >= SByte.MinValue && num <= SByte.MaxValue)
                    {
                        return Convert.ToSByte(num);
                    }
                    break;
                case TypeCode.Single:
                    if (num >= Single.MinValue && num <= Single.MaxValue)
                    {
                        return Convert.ToSingle(num);
                    }
                    break;
                case TypeCode.Int16:
                    if (num >= Int16.MinValue && num <= Int16.MaxValue)
                    {
                        return Convert.ToInt16(num);
                    }
                    break;
                case TypeCode.Int32:
                    if (num >= Int32.MinValue && num <= Int32.MaxValue)
                    {
                        return Convert.ToInt32(num);
                    }
                    break;
                case TypeCode.Int64:
                    if (num >= Int64.MinValue && num <= Int64.MaxValue)
                    {
                        return Convert.ToInt64(num);
                    }
                    break;
                case TypeCode.UInt16:
                    if (num >= UInt16.MinValue && num <= UInt16.MaxValue)
                    {
                        return Convert.ToUInt16(num);
                    }
                    break;
                case TypeCode.UInt32:
                    if (num >= UInt32.MinValue && num <= UInt32.MaxValue)
                    {
                        return Convert.ToUInt32(num);
                    }
                    break;
                case TypeCode.UInt64:
                    if (num >= UInt64.MinValue && num <= UInt64.MaxValue)
                    {
                        return Convert.ToUInt64(num);
                    }
                    break;
                case TypeCode.Decimal:
                    if (num >= -79228162514264337593543950335d && num <= 79228162514264337593543950335d)
                    {
                        return Convert.ToDecimal(num);
                    }
                    break;
                case TypeCode.Double:
                    return num;
            }
            return 0;
        }

        public static Type GetNonNullableType(this　Type type)
        {
            if (IsNullableType(type))
            {
                return type.GetGenericArguments()[0];
            }
            return type;
        }

        #endregion

        #region Common 扩展

        /// <summary>
        /// 输出对象JSON
        /// 只支持对象的第一级基本类型的属性和字段的对象
        /// 以及IDictionary ,IList
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static string ToJson(this object mode)
        {
            if (mode is IDictionary)
            {
                return ToJson(mode as IDictionary);
            }
            if (mode is IEnumerable)
            {
                return ToJson(mode as IEnumerable);
            }
            string ret = "{}";
            var mr = MReflection.GetMReflections(mode.GetType());
            if (mr != null && mr.Count > 0)
            {
                StringBuilder skv = new StringBuilder();
                DateTime dt1970 = new DateTime(1970, 1, 1);
                foreach (var m in mr)
                {
                    var data = m.Value.GetValue(mode);
                    string sdata = data == null ? "" : data.ToString();
                    if (sdata.IsNumber())
                    {
                        skv.AppendFormat(",\"{0}\":{1}", m.Key, sdata);
                    }
                    else if (data is Boolean)
                    {
                        skv.AppendFormat(",\"{0}\":{1}", m.Key, data.Equals(true) ? "true" : "false");
                    }
                    else if (data is DateTime)
                    {
                        DateTime dt = (DateTime)data;
                        long time = (long)(dt - dt1970).TotalMilliseconds;

                        skv.Append(",\"" + m.Key + "\":(function(){var dt=new Date();dt.setTime(" + time
                            + ");return dt;})()");
                    }
                    else
                    {
                        string ss = sdata.Replace("\"", "\\\"").Replace("\'", "\\\'");
                        ss = ss.Replace("\r", "\n").Replace("\n\n", "\n").Replace("\n", "\\\n");
                        skv.AppendFormat(",\"{0}\":\"{1}\"", m.Key, ss);
                    }
                }
                if (skv.Length > 0)
                {
                    ret = "{" + skv.ToString().Substring(1) + "}";
                }
            }
            return ret;
        }
        public static string ToJson(this IDictionary dic)
        {
            string ret = "[]";
            if (dic != null && dic.Count > 0)
            {
                StringBuilder skv = new StringBuilder();
                foreach (var k in dic.Keys)
                {
                    skv.AppendFormat(",\"{0}\":{1}", k.ToString()
                        , ToJson(dic[k]));
                }
                if (skv.Length > 0)
                {
                    ret = "[" + skv.ToString().Substring(1) + "]";
                }
            }
            return ret;
        }
        public static string ToJson(this IEnumerable dic)
        {
            string ret = "[]";
            if (dic != null)
            {
                StringBuilder skv = new StringBuilder();
                foreach (var k in dic)
                {
                    skv.AppendFormat(",{1}", k.ToString()
                        , ToJson(k));
                }
                if (skv.Length > 0)
                {
                    ret = "[" + skv.ToString().Substring(1) + "]";
                }
            }
            return ret;
        }

        #endregion
    }
}
