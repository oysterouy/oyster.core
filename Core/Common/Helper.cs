using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Oyster.Core.Orm;
using System.Reflection;
using System.Security.Cryptography;


namespace Oyster.Core.Common
{
    public class Helper
    {
        /// <summary>
        /// Pasca 命名法
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string PascaName(string name)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(name))
            {
                string[] wds = name.Split(new char[] { '-', '_' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < wds.Length; i++)
                {
                    ret += wds[i][0].ToString().ToUpper();
                    ret += wds[i].Substring(1);
                }
            }
            return ret;
        }

        /// <summary>
        /// 获取大写的MD5签名结果
        /// </summary>
        /// <param name="encypStr"></param>
        /// <returns></returns>
        public static string GetMD5(string encypStr, string key)
        {
            return GetMD5(Encoding.UTF8, encypStr, key);
        }
        public static string GetMD5(Encoding encoding, string encypStr, string key)
        {
            encypStr += key;
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            //使用指定编码方式把字符串转化为字节数组．
            inputBye = encoding.GetBytes(encypStr);

            outputBye = m5.ComputeHash(inputBye);

            retStr = BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();
            return retStr;
        }
        public static string GetMD5(string encypStr)
        {
            return GetMD5(encypStr, "");
        }

        public static string To26X(long num)
        {
            StringBuilder d = new StringBuilder();
            long y = num % 26;
            long t = num - y;
            long k = 26, n = 0;
            while (t > k)
            {
                k *= 26;
            }
            while (t < k & k > 26)
            {
                k /= 26;
                n = t / k;
                d.Append((char)(65 + n));

                t = t - n * k;
            }
            d.Append((char)(65 + y));
            return d.ToString();
        }

        public static string NewCodeByNum(long num, long nadd = 8000)
        {
            return To26X(num + nadd);
        }

        public static NameDesc GetNameDesc(MethodInfo _m)
        {
            if (_m != null)
            {
                object[] attributes = _m.GetCustomAttributes(typeof(NameDesc), false);
                if (attributes != null && attributes.Length > 0)
                    return attributes[0] as NameDesc;
            }
            return null;
        }

        public static T Copy<T>(object fobj) where T : IModel
        {
            T t = Activator.CreateInstance<T>();
            if (fobj != null)
            {
                var ps = MReflection.GetMReflections(fobj.GetType());
                if (ps != null && ps.Count > 0)
                {
                    var tps = MReflection.GetMReflections(typeof(T));
                    if (tps != null && tps.Count > 0)
                    {
                        foreach (string key in tps.Keys)
                        {
                            if (ps.ContainsKey(key))
                            {
                                object data = ps[key].GetValue(fobj);
                                if (t is ValueType)
                                {
                                    object dd = null;
                                    tps[key].SetValue(t, data, out dd);
                                    t = (T)dd;
                                }
                                else
                                {
                                    tps[key].SetValue(t, data);
                                }
                            }
                        }
                    }
                }
            }
            return t;
        }

        public static string OyValStr(string str, IDictionary<string, string> parms)
        {
            Regex reg = new Regex("#oyval:([^#]+)#");
            string s = reg.Replace(str, new MatchEvaluator((m) =>
            {
                if (m.Success && m.Groups.Count > 1 && parms.ContainsKey(m.Groups[1].Value))
                {
                    return parms[m.Groups[1].Value];
                }
                return "";
            }));
            return s;
        }
    }
}
