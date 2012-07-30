using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oyster.Core.Cache;
using Oyster.Core.Tool;

namespace Oyster.Core.Orm
{
    public class ModelEngineCache : IModelEngine
    {
        protected static ModelEngineCache _instance;

        public static ModelEngineCache Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ModelEngineCache();
                }
                return _instance;
            }
        }

        string filterMd5Key(IModel m, OyCondition condition, MPager mp = null, OyOrderBy orderby = null)
        {
            string kval = string.Format("{0}-{1}-{2}-{3}", new string[]{ m.zModelType.FullName,condition.ToString(m)
                , mp==null?"": mp.ToString(),orderby==null?"":orderby.ToString(m)});
            string k = OyTools.GetMD5(kval);
            return k;
        }

        public IModel GetById(IModel mode, long Mid)
        {
            string k = string.Format("Model-{0}:{1}", mode.zModelType.FullName, Mid.ToString());
            return CacheEngine.Instance.GetValue(k) as IModel;
        }

        public IList<IModel> Filter(IModel m, OyCondition condition, MPager mp = null, OyOrderBy orderby = null)
        {
            var dic = FilterWithId(m, condition, mp, orderby);
            IList<IModel> ls = new List<IModel>();
            if (dic != null && dic.Count > 0)
            {
                foreach (var v in dic.Values)
                {
                    ls.Add(v as IModel);
                }
            }
            return ls.Count > 0 ? ls : null;
        }

        /// <summary>
        /// 获取存到了自定义查询条件的查询数据集
        /// </summary>
        /// <param name="m"></param>
        /// <param name="condition"></param>
        /// <param name="mp"></param>
        /// <param name="orderby">无意义</param>
        /// <returns></returns>
        public IDictionary<long, object> FilterWithId(IModel m, OyCondition condition, MPager mp = null, OyOrderBy orderby = null)
        {
            string k = filterMd5Key(m, condition, mp, orderby);
            var data = CacheEngine.Instance.GetValue(k) as IDictionary<long, object>;

            if (data != null && data.Count > 0 && mp != null)
            {
                var mpage = CacheEngine.Instance.GetValue(k + "_mpager");
                if (mpage != null && mpage is MPager)
                {
                    mp = mpage as MPager;
                }
            }

            return data;
        }

        public void SetFilterWithId(IModel m, IDictionary<long, object> dic, OyCondition condition, MPager mp = null, OyOrderBy orderby = null)
        {
            string k = filterMd5Key(m, condition, mp, orderby);
            if (mp != null)
            {
                CacheEngine.Instance.SetValue(k + "_mpager", mp, TimeSpan.FromMilliseconds(AppConfig.Instance.CacheFilter));
            }
            if (dic != null && dic.Count > 0)
            {
                CacheEngine.Instance.SetValue(k, dic, TimeSpan.FromMilliseconds(AppConfig.Instance.CacheFilter));
            }
        }

        public int Update(IModel mode, OyValue val, OyCondition condition)
        {
            string op = null;
            return Update(mode, val, condition, out op);
        }

        /// <summary>
        /// 更新缓存中的Model对象实例数据
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="val"></param>
        /// <param name="condition"></param>
        /// <param name="opguid">update cache 取到的opguid 没有意义</param>
        /// <returns>影响的实例数</returns>
        public int Update(IModel mode, OyValue val, OyCondition condition, out string opguid)
        {
            int t = 0;
            string k = string.Format("Model-{0}", mode.zModelType.FullName);
            opguid = k;
            var me = CacheEngine.Instance.GetValue(k) as ModelCacheEntry;
            if (me != null)
            {
                foreach (var vd in me.HtData.Values)
                {
                    if (vd != null && vd is CacheEntry)
                    {
                        var vvd = (vd as CacheEntry).Value;
                        if (condition.IsMatch(vvd as IModel))
                        {
                            val.Update(vvd as IModel);
                            t++;
                        }
                    }
                }
            }

            return t > 0 ? t : -1;
        }

        public string Insert(IModel mode)
        {
            string k = string.Format("Model-{0}", mode.zModelType.FullName);

            string kname = CacheEngine.Instance.SetValue(k, mode);

            return kname;
        }
    }
}
