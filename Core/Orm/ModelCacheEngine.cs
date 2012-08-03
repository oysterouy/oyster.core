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
        string filterMd5Key(Imodel m, Condition condition, Mpager mp = null, OrderBy orderby = null)
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

        public Imodel GetById(Imodel mode, long Mid)
        {
            string k = GetKeyById(mode.GetType(), Mid);
            return Get(k);
        }

        public IDictionary<long, Imodel> GetByIds(Imodel mode, IList<long> Mids)
        {
            Dictionary<long, Imodel> dic = new Dictionary<long, Imodel>();
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

        public IList<Imodel> Filter(Imodel m, Condition condition, Mpager mp = null, OrderBy orderby = null)
        {
            throw new NotImplementedException();
        }

        public IDictionary<long, Imodel> FilterWithId(Imodel m, Condition condition, Mpager mp = null, OrderBy orderby = null)
        {
            string k = filterMd5Key(m, condition, mp, orderby);
            var data = CacheEngine.Instance.GetValue(k) as long[];
            if (data != null && data.Length > 0)
            {
                if (mp != null)
                {
                    var mpage = CacheEngine.Instance.GetValue(k + "_mpager");
                    if (mpage != null && mpage is Mpager)
                    {
                        mp = mpage as Mpager;
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

        public string SetFilterWithId(Imodel m, IDictionary<long, Imodel> dic, Condition condition, Mpager mp = null, OrderBy orderby = null)
        {
            string k = filterMd5Key(m, condition, mp, orderby);
            if (mp != null)
            {
                CacheEngine.Instance.SetValue(k + "_mpager", mp, TimeSpan.FromMilliseconds(CacheTimeOut));
            }
            if (dic != null && dic.Count > 0)
            {
                CacheEngine.Instance.SetValue(k, dic.Keys.ToArray(), TimeSpan.FromMilliseconds(CacheTimeOut));
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

        public Imodel Get(Type type, long id)
        {
            string k = GetKeyById(type, id);
            return Get(k);
        }

        public Imodel Get<T>(long id)
        {
            return Get(typeof(T), id);
        }

        public Imodel Get(string cachekey)
        {
            return CacheEngine.Instance.GetValue(cachekey) as Imodel;
        }

        public string Set(string k, Imodel mode)
        {
            return CacheEngine.Instance.SetValue(k, mode, TimeSpan.FromMilliseconds(CacheTimeOut));
        }

        public string Set(Type type, Imodel mode)
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

        public string Set(Imodel mode)
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