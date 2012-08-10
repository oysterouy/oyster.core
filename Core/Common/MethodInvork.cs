using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.Reflection;

namespace Oyster.Core.Common
{
    public class MethodInvork
    {
        public static object Invork(string methodName, ICollection coll, Type type, object obj = null)
        {
            if (coll is NameValueCollection)
            {
                return Invork<Object>(methodName, coll as NameValueCollection, type, obj);
            }
            return Invork<Object>(methodName, coll as IDictionary, type, obj);
        }
        public static BK Invork<BK>(string methodName, NameValueCollection coll, Type type, object obj = null)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (coll != null)
            {
                foreach (string k in coll.Keys)
                {
                    dic.Add(k, coll[k]);
                }
            }
            return Invork<BK>(methodName, dic, type, obj);
        }
        public static BK Invork<BK>(string methodName, IDictionary dic, Type type, object obj = null)
        {
            if (type != null)
            {
                MethodInvorkInfo mi = new MethodInvorkInfo(methodName, type, obj);
                var mmi = ContextHelper.Instance.GetContextByKey(mi.Key) as MethodInvorkInfo;
                if (mmi != null)
                {
                    mmi.Invork<BK>(dic);
                }
                else
                {
                    var ms = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                    if (ms != null && ms.Length > 0)
                    {
                        foreach (var m in ms)
                        {
                            if (m.Name == methodName)
                            {
                                mi.MethodList.Add(m);
                            }
                        }
                        if (mi.MethodList.Count < 1)
                        {
                            foreach (var m in ms)
                            {
                                if (m.Name.ToLower() == methodName)
                                {
                                    mi.MethodList.Add(m);
                                }
                            }
                        }
                    }
                    ContextHelper.Instance.SetContextByKey(mi.Key, mi);
                    mi.Invork<BK>(dic);
                }
            }
            return default(BK);
        }
    }
    public class MethodInvorkInfo
    {
        public string Key { get; private set; }
        string _methodName;
        Type _type;
        object _obj;
        public MethodInvorkInfo(string methodName, Type type, object obj = null)
        {
            _methodName = methodName;
            _type = type;
            _obj = obj;

            Key = Helper.GetMD5(string.Format("method:{0}-{1}-{2}", type.FullName, methodName, obj == null ? "static" : "object"));

            MethodList = new List<MethodInfo>();
        }
        public List<MethodInfo> MethodList { get; private set; }

        public object Invork(IDictionary data)
        {
            return Invork<Object>(data);
        }

        public T Invork<T>(IDictionary data)
        {
            Dictionary<MethodInfo, int> methodSort = new Dictionary<MethodInfo, int>();
            Dictionary<MethodInfo, List<object>> methodParms = new Dictionary<MethodInfo, List<object>>();
            foreach (var m in MethodList)
            {
                if (Helper.IsBaseType(m.ReturnType, typeof(T)))
                {
                    var pstp = m.GetParameters();
                    if (pstp != null && pstp.Length > 0)
                    {
                        foreach (var p in pstp)
                        {
                            object pobj = null;
                            if (p.ParameterType.Equals(typeof(string)))
                            {
                                pobj = "";
                            }
                            else
                            {
                                pobj = Activator.CreateInstance(p.ParameterType, new object[] { });
                            }
                            object d = null;
                            d = data.Contains(p.Name) ? data[p.Name] : null;
                            d = d == null && data.Contains(p.Name.ToLower()) ? data[p.Name.ToLower()] : d;
                            if (d != null)
                            {
                                if (pobj != null)
                                {
                                    var ptp = pobj.GetType();
                                    if (ptp.IsValueType || ptp.Equals(typeof(String)))
                                    {
                                        var f = d.ChangeType(ptp);
                                        pobj = f != null ? f : pobj;
                                    }
                                    else if (pobj is IList && d is IList)
                                    {
                                        var gtps = ptp.GetGenericArguments();
                                        if (gtps.Length > 0 && (gtps[0].IsValueType || gtps.Equals(typeof(string))))
                                        {
                                            var polist = pobj as IList;
                                            if (polist != null)
                                            {
                                                foreach (var item in d as IList)
                                                {
                                                    polist.Add(item.ChangeType(gtps[0]));
                                                }
                                                pobj = polist;
                                            }
                                        }
                                    }
                                    else if (d is IDictionary)
                                    {
                                        var f = BindData.BindDataByDic(ptp, d as IDictionary);
                                        pobj = f != null ? f : pobj;
                                    }
                                }
                            }
                            if (!methodSort.ContainsKey(m))
                            {
                                methodSort.Add(m, 0);
                            }
                            if (pobj != null)
                            {
                                methodSort[m]++;
                            }
                            if (!methodParms.ContainsKey(m))
                            {
                                methodParms.Add(m, new List<object>());
                            }
                            methodParms[m].Add(pobj);
                        }
                    }
                }
            }
            if (methodSort.Count > 0)
            {
                var m = methodSort.OrderBy((kv) => { return kv.Value; }).First().Key;
                m.Invoke(_obj, methodParms[m].ToArray());
            }

            return default(T);
        }
    }
}
