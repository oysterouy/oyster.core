using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oyster.Core.Orm;
using Oyster.Core.Cache;
using Oyster.Core.Db;

namespace System
{
    public partial class OyEngine
    {
        public class M : ModelEngine
        {
            public static IModel GetById(Type zmodeType, long Mid)
            {
                return ModelEngine.Instance.GetById(Activator.CreateInstance(zmodeType) as IModel, Mid);
            }

            public static IList<IModel> Filter(Type zmodeType, OyCondition condition, MPager mp, OyOrderBy orderby)
            {
                return ModelEngine.Instance.Filter(Activator.CreateInstance(zmodeType) as IModel
                    , condition, mp, orderby);
            }

            public static IDictionary<long, object> FilterWithId(Type zmodeType, OyCondition condition, MPager mp, OyOrderBy orderby)
            {
                return ModelEngine.Instance.FilterWithId(Activator.CreateInstance(zmodeType) as IModel, condition, mp, orderby);
            }

            public static int Update(Type zmodeType, OyValue val, OyCondition condition)
            {
                string s = null;
                return Update(zmodeType, val, condition, out s);
            }

            public static int Update(Type zmodeType, OyValue val, OyCondition condition, out string opguid)
            {
                return ModelEngine.Instance.Update(Activator.CreateInstance(zmodeType) as IModel, val, condition, out opguid);
            }

            public static string Insert(IModel mode, bool justdb = false)
            {
                if (justdb)
                {
                    return ModelEngine.Instance.Insert(mode, true);
                }
                else
                {
                    return ModelEngine.Instance.Insert(mode);
                }
            }

            public static IModel Insert(IModel mode, int t)
            {
                return ModelEngine.Instance.Insert(mode, t);
            }
        }

        public class M<T> : M where T : IModel
        {
            public static T GetById(long Mid)
            {
                var d = GetById(typeof(T), Mid);
                if (d is T)
                    return (T)d;

                return default(T);
            }

            public static IList<T> Filter(IModel m, OyCondition condition, MPager mp = null, OyOrderBy orderby = null)
            {
                var d = ModelEngine.Instance.Filter(m, condition, mp, orderby);
                IList<T> ls = new List<T>();
                if (d != null && d.Count > 0)
                {
                    foreach (var dd in d)
                    {
                        if (dd is T)
                        {
                            ls.Add((T)dd);
                        }
                    }
                }
                return ls;
            }
            public static IList<T> Filter(OyCondition condition, MPager mp = null, OyOrderBy orderby = null)
            {
                var d = FilterWithId(condition, mp, orderby);
                if (d != null && d.Count > 0)
                {
                    return d.Values.ToList();
                }
                return new List<T>();
            }


            public static IDictionary<long, T> FilterWithId(OyCondition condition, MPager mp = null, OyOrderBy orderby = null)
            {
                var dic = FilterWithId(typeof(T), condition, mp, orderby);
                if (dic != null && dic.Count > 0)
                {
                    if (dic.Values.First() is T)
                    {
                        Dictionary<long, T> dc = new Dictionary<long, T>();
                        foreach (var k in dic.Keys)
                        {
                            dc.Add(k, (T)dic[k]);
                        }
                        return dc;
                    }
                }
                return new Dictionary<long, T>();
            }

            public static int Update(OyValue val, OyCondition condition)
            {
                string s = null;
                return Update(val, condition, out s);
            }

            public static int Update(OyValue val, OyCondition condition, out string opguid)
            {
                return Update(typeof(T), val, condition, out opguid);
            }
        }
    }
}
