using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oyster.Core.Orm
{
    public class ModelEngine : IModelEngine
    {
        protected IList<IModelEngine> _modelEngine;
        protected ModelEngine()
        {
            Init();
        }
        protected virtual void Init()
        {
            _modelEngine = new List<IModelEngine>();
            _modelEngine.Add(ModelEngineCache.Instance);
            _modelEngine.Add(ModelEngineDb.Instance);
        }

        protected static ModelEngine _instance;
        public static ModelEngine Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ModelEngine();
                }
                return _instance;
            }
        }

        /// <summary>
        /// 添加缓存引擎到引擎适配器
        /// </summary>
        /// <param name="cacher"></param>
        /// <returns></returns>
        public virtual int AddEngine(IModelEngine engine)
        {
            _modelEngine.Add(engine);
            return _modelEngine.Count - 1;
        }

        public IModel GetById(IModel mode, long Mid)
        {
            IModel md = null;
            List<IModelEngine> upsetengine = new List<IModelEngine>();
            foreach (var m in _modelEngine)
            {
                if (OyTran.Current.IsTraning && !(m is ModelEngineDb))
                {
                    continue;
                }
                md = m.GetById(mode, Mid);
                if (!(m is ModelEngineDb))
                {
                    upsetengine.Add(m);
                }
                if (md != null)
                {
                    break;
                }
            }
            if (md != null)
            {
                foreach (var mm in upsetengine)
                {
                    mm.Insert(md);
                }
            }
            return md;
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

        public IDictionary<long, object> FilterWithId(IModel m, OyCondition condition, MPager mp = null, OyOrderBy orderby = null)
        {
            ModelEngineCache mcache = null;
            foreach (var engin in _modelEngine)
            {
                if (OyTran.Current.IsTraning && !(engin is ModelEngineDb))
                {
                    continue;
                }
                if (engin is ModelEngineCache)
                {
                    mcache = engin as ModelEngineCache;
                }
                var data = engin.FilterWithId(m, condition, mp, orderby);
                if (data != null)
                {
                    if (mcache != null && mcache != engin && data.Count > 0)
                    {
                        mcache.SetFilterWithId(m, data, condition, mp, orderby);
                    }
                    return data;
                }
            }
            return null;
        }

        public int Update(IModel mode, OyValue val, OyCondition condition)
        {
            string s = null;
            return Update(mode, val, condition, out s);
        }

        public int Update(IModel mode, OyValue val, OyCondition condition, out string opguid)
        {
            int t = 0, tt = 0;
            opguid = "";
            List<IModelEngine> mds = new List<IModelEngine>();
            foreach (var engin in _modelEngine)
            {
                if (OyTran.Current.IsTraning && !(engin is ModelEngineDb))
                {
                    mds.Add(engin);
                    continue;
                }
                tt = engin.Update(mode, val, condition, out opguid);
                if (tt > t)
                {
                    t = tt;
                    if (mds.Count > 0)
                    {
                        foreach (var eg in mds)
                        {
                            eg.Update(mode, val, condition, out opguid);
                        }
                    }
                }
            }
            return t;
        }

        /// <summary>
        /// 插入除数据库以外的Model保存区
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public string Insert(IModel mode)
        {
            string opguid = "";
            foreach (var engin in _modelEngine)
            {
                if (!(engin is ModelEngineDb))
                {
                    opguid = engin.Insert(mode);
                }
            }
            return opguid;
        }

        /// <summary>
        /// this allways insert to db,not care param justdb
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="justdb"></param>
        /// <returns></returns>
        public string Insert(IModel mode, bool justdb)
        {
            foreach (var engin in _modelEngine)
            {
                if (engin is ModelEngineDb)
                {
                    return engin.Insert(mode);
                }
            }
            return null;
        }
        /// <summary>
        /// 插入数据库 然后返回插入的实例
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="needback"></param>
        /// <returns></returns>
        public IModel Insert(IModel mode, int needback)
        {
            string opguid = "";
            ModelEngineDb dbengine = null;
            foreach (var engin in _modelEngine)
            {
                if (engin is ModelEngineDb)
                {
                    opguid = engin.Insert(mode);
                    dbengine = engin as ModelEngineDb;
                    break;
                }
            }
            if (!string.IsNullOrEmpty(opguid))
            {
                var ls = dbengine.Filter(mode, new OyCondition("OpGuid", opguid));
                if (ls != null && ls.Count > 0)
                {
                    mode = ls[0];
                    Insert(mode);
                }
            }
            return mode;
        }
    }
}
