using System;
using System.Collections.Generic;
using System.Linq;
using Oyster.Core.Common;
using Oyster.Core.Cache;


namespace Oyster.Core.Orm
{
    /// <summary>
    /// 使用CacheEngine 作为缓存提供引擎的模型提供引擎
    /// </summary>
    public class ModelCacheEngine : IModelCacheEngine
    {
        string filterMd5Key(IModel m, Condition condition, MPager mp = null, OrderBy orderby = null)
        {
            string kval = string.Format("{0}-{1}-{2}-{3}", new string[]{ m.zModelType.FullName,condition.ToString(m)
                , mp==null?"": mp.ToString(),orderby==null?"":orderby.ToString(m)});
            string k = Helper.GetMD5(kval);
            return k;
        }
        string GetKeyById(Type modeltype, long Mid)
        {
            string k = string.Format("Model-{0}:{1}", modeltype.FullName, Mid.ToString());
            return k;
        }

        public IModel GetById(IModel mode, long Mid)
        {
            string k = GetKeyById(mode.GetType(), Mid);
            return Get(k);
        }

        public string SetById(IModel mode, long Mid)
        {
            string k = GetKeyById(mode.GetType(), Mid);
            return Set(k, mode);
        }

        public IDictionary<long, IModel> GetByIds(IModel mode, IList<long> Mids)
        {
            Dictionary<long, IModel> dic = new Dictionary<long, IModel>();
            foreach (var id in Mids)
            {
                if (!dic.ContainsKey(id))
                {
                    var m = GetById(mode, id);
                    if (m != null)
                    {
                        dic.Add(id, m);
                    }
                }
            }
            return dic;
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
            string k = filterMd5Key(m, condition, mp, orderby);
            var data = CacheEngine.Instance.GetValue(k) as long[];
            if (data != null && data.Length > 0)
            {
                if (mp != null)
                {
                    var mpage = CacheEngine.Instance.GetValue(k + "_MPager");
                    if (mpage != null && mpage is MPager)
                    {
                        mp = mpage as MPager;
                    }
                }

                var dic = GetByIds(m, data);
                List<long> nocacheids = new List<long>();
                nocacheids.AddRange(data);
                foreach (var lid in dic.Keys)
                {
                    if (nocacheids.Contains(lid))
                    {
                        nocacheids.Remove(lid);
                    }
                }
                //所有数据都还在，则成功!
                if (nocacheids.Count < 1)
                {
                    return dic;
                }
            }
            return null;
        }

        public string SetFilterWithId(IModel m, IDictionary<long, IModel> dic, Condition condition, MPager mp = null, OrderBy orderby = null)
        {
            string k = filterMd5Key(m, condition, mp, orderby);
            if (mp != null)
            {
                CacheEngine.Instance.SetValue(k + "_MPager", mp, TimeSpan.FromMilliseconds(CacheTimeOut));
            }
            if (dic != null && dic.Count > 0)
            {
                CacheEngine.Instance.SetValue(k, dic.Keys.ToArray(), TimeSpan.FromMilliseconds(CacheTimeOut));
                foreach (long id in dic.Keys)
                {
                    var item = dic[id];
                    if (item != null)
                    {
                        SetById(item, id);
                    }
                }
            }
            return k;
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
                _level = value;
            }
        }

        public IModel Get(Type type, long id)
        {
            string k = GetKeyById(type, id);
            return Get(k);
        }

        public IModel Get<T>(long id)
        {
            return Get(typeof(T), id);
        }

        public IModel Get(string cachekey)
        {
            return CacheEngine.Instance.GetValue(cachekey) as IModel;
        }

        public string Set(string k, IModel mode)
        {
            return CacheEngine.Instance.SetValue(k, mode, TimeSpan.FromMilliseconds(CacheTimeOut));
        }

        public string Set(Type type, IModel mode)
        {
            var ps = MReflection.GetMReflections(mode.zModelType);
            if (ps.ContainsKey("Id"))
            {
                object obj = ps["Id"].GetValue(mode);
                if (obj is long)
                {
                    string k = GetKeyById(type, Convert.ToInt64(obj));
                    return Set(k, mode);
                }
            }
            return null;
        }

        public string Set(IModel mode)
        {
            return Set(mode.GetType(), mode);
        }

        double _cacheTimeOut = 50000;
        public double CacheTimeOut
        {
            get
            {
                return _cacheTimeOut;
            }
            set
            {
                _cacheTimeOut = value;
            }
        }
    }
}