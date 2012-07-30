using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Oyster.Core.Db;
using System.Data;
using Oyster.Core.Cache;
using Oyster.Core.Tool;

namespace Oyster.Core.Orm
{
    public class MReflection
    {
        public string PName;
        public PropertyInfo PInfo;
        public FieldInfo FInfo;

        public Type PType
        {
            get
            {
                if (FInfo != null)
                {
                    return FInfo.FieldType;
                }
                else if (PInfo != null)
                {
                    return PInfo.PropertyType;
                }
                return typeof(object);
            }
        }

        public object GetValue(object obj, object[] idx = null)
        {
            if (FInfo != null)
            {
                return FInfo.GetValue(obj);
            }
            else if (PInfo != null)
            {
                return PInfo.GetValue(obj, idx);
            }
            return null;
        }

        public bool SetValue(object obj, object val, object[] idx = null)
        {
            object d = null;
            return SetValue(obj, val, out d, idx);
        }
        public bool SetValue(object obj, object val, out object t, object[] idx = null)
        {
            t = obj;
            if (FInfo != null)
            {
                object d = val.ChangeType(FInfo.FieldType);
                FInfo.SetValue(obj, d);
                return true;
            }
            else if (PInfo != null)
            {
                object d = val.ChangeType(PInfo.PropertyType);
                PInfo.SetValue(obj, d, idx);
                return true;
            }
            return false;
        }

        #region 扩展方法

        public static Dictionary<string, MReflection> GetMReflections(Type tp)
        {
            Dictionary<string, MReflection> ps = null;
            if (tp != null)
            {
                ps = CacheEngine.Instance.GetValue(tp.FullName) as Dictionary<string, MReflection>;

                if (ps == null)
                {
                    lock (tp)
                    {
                        Dictionary<string, MReflection> psdic = new Dictionary<string, MReflection>();
                        var fs = tp.GetFields();
                        if (fs != null && fs.Length > 0)
                        {
                            foreach (FieldInfo f in fs)
                            {
                                if (psdic.ContainsKey(f.Name))
                                {
                                    psdic[f.Name].FInfo = f;
                                }
                                else
                                {
                                    psdic.Add(f.Name, new MReflection { PName = f.Name, FInfo = f });
                                }
                            }
                        }

                        var pp = tp.GetProperties();
                        if (pp != null && pp.Length > 0)
                        {
                            foreach (PropertyInfo p in pp)
                            {
                                if (psdic.ContainsKey(p.Name))
                                {
                                    psdic[p.Name].PInfo = p;
                                }
                                else
                                {
                                    psdic.Add(p.Name, new MReflection { PName = p.Name, PInfo = p });
                                }
                            }
                        }
                        CacheEngine.Instance.SetValue(tp.FullName, psdic);
                        ps = psdic;
                    }
                }
            }
            return ps;
        }


        static Dictionary<Type, Dictionary<string, string>> _modelscolumn;
        public static Dictionary<string, string> GetModelColumns(IModel mode)
        {
            if (_modelscolumn == null)
            {
                _modelscolumn = new Dictionary<Type, Dictionary<string, string>>();
            }
            lock (mode.zModelType)
            {
                if (!_modelscolumn.ContainsKey(mode.zModelType))
                {
                    var dt = DbEngine.Instance.GetSchemaTable(string.Format("select * from {0} where id < -999", mode.zTableName));
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Dictionary<string, string> cols = new Dictionary<string, string>();
                        foreach (DataRow r in dt.Rows)
                        {
                            string col = r["ColumnName"].ToString().ToLower();
                            string k = OyTools.PascaName(col);
                            if (!cols.ContainsKey(k))
                            {
                                cols.Add(k, col);
                            }
                        }
                        if (cols.Count > 0)
                        {
                            _modelscolumn.Add(mode.zModelType, cols);
                        }
                    }
                }
            }
            if (_modelscolumn.ContainsKey(mode.zModelType))
            {
                return _modelscolumn[mode.zModelType];
            }
            else
            {
                return new Dictionary<string, string>();
            }
        }

        #endregion
    }
}
