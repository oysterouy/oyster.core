using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oyster.Core.Orm;
using System.Collections;

namespace Oyster.Core.Common
{
    public class BindData
    {

        #region BindData


        public static object BindDataByRow(IModel m, DataRow dr)
        {
            try
            {
                var dicols = MReflection.GetModelColumns(m);
                var ps = MReflection.GetMReflections(m.zModelType);
                if (ps == null)
                {
                    return m;
                }
                foreach (string p in dicols.Keys)
                {
                    if (dr.Table.Columns.Contains(dicols[p]))
                    {
                        if (ps.ContainsKey(p))
                        {
                            var mr = ps[p];
                            if (mr != null)
                            {
                                object data = dr[dicols[p]].ChangeType(mr.PType);
                                data = data.Equals(DBNull.Value) ? null : data;
                                if (data != null)
                                {
                                    mr.SetValue(m, data);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.Error("BindDataByRow:" + m.zTableName, ex);
            }
            return m;
        }

        public static Dictionary<long, object> BindDataByTableWithId(Type type, DataTable dt)
        {
            Dictionary<long, object> dic = new Dictionary<long, object>();

            var ps = MReflection.GetMReflections(type);
            if (ps == null)
            {
                return dic;
            }
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var tm = Activator.CreateInstance(type) as IModel;
                        if (tm != null)
                        {
                            var d = BindDataByRow(tm, dr);
                            MReflection mr = ps["Id"];
                            if (mr == null)
                            {
                                throw new Exception("IModel Models Must Use field 'Id' as the primary key!");
                            }
                            long v = (long)mr.GetValue(d);
                            if (!dic.ContainsKey(v))
                            {
                                dic.Add(v, d);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.Error("BindDataByTableWithId:" + type.FullName, ex);
            }
            return dic;
        }

        public static Dictionary<long, V> BindDataByTableWithId<V>(DataTable dt)
        {
            var dic = BindDataByTableWithId(typeof(V), dt);
            Dictionary<long, V> d = new Dictionary<long, V>();
            foreach (long k in dic.Keys)
            {
                d.Add(k, (V)dic[k]);
            }
            return d;
        }

        public static object BindDataByDic(Type tp, IDictionary data)
        {
            object m = Activator.CreateInstance(tp);
            if (m != null)
            {
                try
                {
                    var ps = MReflection.GetMReflections(tp);
                    if (ps == null)
                    {
                        return null;
                    }
                    foreach (string p in ps.Keys)
                    {
                        var dat = data.Contains(p) ? data[p] : null;
                        dat = dat == null && data.Contains(p.ToLower()) ? data[p.ToLower()] : null;
                        if (dat != null)
                        {
                            var mr = ps[p];
                            if (mr != null)
                            {
                                object d = data[p].ChangeType(mr.PType);
                                if (d != null)
                                {
                                    mr.SetValue(m, data);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Logger.Error("BindDataByDic:" + tp.FullName, ex);
                }
            }
            return m;
        }
        #endregion
    }
}
