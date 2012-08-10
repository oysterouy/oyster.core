using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oyster.Core.Db;
using System.Data;
using Oyster.Core.Common;


namespace Oyster.Core.Orm
{
    public class ModelDbEngine : IModelEngine
    {
        public IModel GetById(IModel mode, long Mid)
        {
            var dic = FilterWithId(mode, new Condition("Id", Mid), null, null);
            if (dic != null && dic.ContainsKey(Mid))
            {
                return dic[Mid] as IModel;
            }
            return null;
        }

        public IDictionary<long, IModel> GetByIds(IModel mode, IList<long> Mids)
        {
            var dic = FilterWithId(mode, new Condition("Id", ConditionOperator.In, Mids), null, null);
            if (dic != null && dic.Count > 0)
            {
                return dic;
            }
            return null;
        }

        public IDictionary<long, IModel> GetByOpGuid(IModel mode, string guid)
        {
            var dic = FilterWithId(mode, new Condition("OpGuid", guid), null, null);
            if (dic != null && dic.Count > 0)
            {
                return dic;
            }
            return null;
        }

        public IList<IModel> Filter(IModel m, Condition condition, MPager mp = null, OrderBy orderby = null)
        {
            var dic = FilterWithId(m, condition, mp, orderby);
            if (dic != null && dic.Count > 0)
            {
                return dic.Values.ToList();
            }
            return null;
        }

        public IDictionary<long, IModel> FilterWithId(IModel m, Condition condition, MPager mp = null, OrderBy orderby = null)
        {
            if (m == null)
            {
                throw new Exception("Model Class Must Implements the interface IModel");
            }
            string sqlexp = "select {0} from {1} where {2} ";
            var dicols = MReflection.GetModelColumns(m);
            string columns = string.Join(",", dicols.Values);

            var plist = Db.DbEngine.NewParameters();
            string condstr = condition.ToString(m, plist);
            string orderbystr = orderby == null ? "" : orderby.ToString(m);
            string sql = string.Format(sqlexp, new string[] { columns, m.zTableName, condstr });

            if (mp != null)
            {
                sql = DbEngine.Instance.GetPagerSql(sql, orderbystr, mp.PageIndex, mp.PageSize, out mp.TotalCount, plist.Values.ToArray());
            }
            else
            {
                sql += orderbystr;
            }

            var ds = DbEngine.Instance.ExecuteQuery(sql, plist.Values.ToArray());

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var dic = BindData.BindDataByTableWithId(m.zModelType, ds.Tables[0]);
                if (dic != null && dic.Count > 0)
                {
                    Dictionary<long, IModel> ddd = new Dictionary<long, IModel>();
                    foreach (var d in dic.Keys)
                    {
                        ddd.Add(d, dic[d] as IModel);
                    }
                    return ddd;
                }
            }
            return new Dictionary<long, IModel>();
        }

        public int Update(IModel mode, ValuePair val, Condition condition)
        {
            string op = "";
            return Update(mode, val, condition, out op);
        }

        public int Update(IModel mode, ValuePair val, Condition condition, out string opguid)
        {
            var parms = new ParameterCollection();
            opguid = Guid.NewGuid().ToString();
            string valstr = val.ToString(mode, parms);
            if (!string.IsNullOrEmpty(valstr))
            {
                string condstr = condition.ToString(mode, parms);
                if (!string.IsNullOrEmpty(condstr))
                {
                    var p = DbEngine.Instance.NewDataParameter("lastchange_time");
                    p.Value = DateTime.Now;
                    parms.Add(p.ParameterName, p);

                    string sql = string.Format("update {0} set {1},op_guid='{2}',lastchange_time={3} where {4}"
                        , new string[] { mode.zTableName, valstr, opguid, p.ParameterName, condstr });
                    return DbEngine.Instance.ExecuteNonQuery(sql, parms.Values.ToArray());
                }
            }
            return 0;
        }

        public string Insert(IModel m)
        {
            var ps = MReflection.GetMReflections(m.zModelType);
            var diccols = MReflection.GetModelColumns(m);

            StringBuilder sdcol = new StringBuilder();
            StringBuilder sdval = new StringBuilder();
            List<IDataParameter> parmts = new List<IDataParameter>();

            if (ps != null)
            {
                string opguid = Guid.NewGuid().ToString();
                foreach (string key in ps.Keys)
                {
                    if (!diccols.ContainsKey(key))
                    {
                        continue;
                    }
                    string colname = diccols[key];
                    var pter = DbEngine.Instance.NewDataParameter(colname);

                    sdcol.AppendFormat(",{0}", colname);
                    sdval.AppendFormat(",{0}", pter.ParameterName);

                    switch (colname)
                    {
                        case "id":
                            //ID 列最好是自增，否则得自己传入数值
                            if (ps.ContainsKey(key))
                            {
                                var mr = ps[key];
                                if (mr != null)
                                {
                                    var idval = mr.GetValue(m);
                                    if (idval != null && Convert.ToInt64(idval) > 0)
                                    {
                                        pter.Value = idval;
                                        parmts.Add(pter);
                                        break;
                                    }
                                }
                            }
                            pter.Value = DBNull.Value;
                            parmts.Add(pter);
                            break;
                        case "create_time":
                            pter.Value = DateTime.Now;
                            parmts.Add(pter);
                            break;
                        case "lastchange_time":
                            pter.Value = DateTime.Now;
                            parmts.Add(pter);
                            break;
                        case "op_guid":
                            pter.Value = opguid;
                            parmts.Add(pter);
                            break;
                        default:
                            if (ps.ContainsKey(key))
                            {
                                var mr = ps[key];
                                if (mr != null)
                                {
                                    pter.Value = mr.GetValue(m);
                                    if (pter.Value == null)
                                    {
                                        pter.Value = "";
                                    }
                                    parmts.Add(pter);
                                }
                            }
                            break;
                    }
                }
                string sql = string.Format("insert into {0} ({1}) values ({2})", new string[] { m.zTableName, sdcol.ToString().Substring(1), sdval.ToString().Substring(1) });

                int t = DbEngine.Instance.ExecuteNonQuery(sql, parmts);
                if (t > 0)
                {
                    return opguid;
                }
            }
            return null;
        }
        int _level = 0;
        public int Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = 0;
            }
        }
    }
}
