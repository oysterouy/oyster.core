using System;
using System.Collections.Generic;
using System.Linq;


namespace Oyster.Core.Orm
{
    /// <summary>
    /// 数据库模型操作引擎
    /// 默认添加了（ModelDbEngine,ModelCacheEngine) 如需替换，请使用ClearDbEngine(),ClearCacheEngine()
    /// </summary>
    public class ModelEngine
    {
        protected IList<IModelEngine> _modelEngine;
        protected IList<IModelCacheEngine> _modelCacheEngine;
        protected ModelEngine()
        {
            Init();
        }
        protected virtual void Init()
        {
            _modelEngine = new List<IModelEngine>();
            _modelCacheEngine = new List<IModelCacheEngine>();

            _modelEngine.Add(new ModelDbEngine());
            _modelCacheEngine.Add(new ModelCacheEngine());
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

        private void UpdateCacheByOpGuid(IModel mode, string opguid)
        {
            IDictionary<long, IModel> dic = null;
            foreach (var m in _modelEngine)
            {
                dic = m.FilterWithId(mode, new Condition("OpGuid", opguid));
                if (dic != null && dic.Count > 0)
                {
                    break;
                }
            }
            if (dic != null && dic.Count > 0 && _modelCacheEngine.Count > 0)
            {
                foreach (long k in dic.Keys)
                {
                    var m = _modelCacheEngine[0];
                    m.Set(dic[k]);
                }
            }
        }

        /// <summary>
        /// 添加模型引擎到引擎适配器
        /// </summary>
        /// <param name="cacher"></param>
        /// <returns></returns>
        public virtual int AddDbEngine(IModelEngine engine)
        {
            int idx = -1;
            for (int i = 0; i < _modelEngine.Count; i++)
            {
                var m = _modelEngine[i];
                if (m.Level < engine.Level)
                {
                    idx = i;
                    _modelEngine.Insert(idx, engine);
                }
            }
            if (idx < 0)
            {
                _modelEngine.Add(engine);
            }

            return idx;
        }

        public void ClearDbEngine()
        {
            _modelEngine.Clear();
        }

        /// <summary>
        /// 添加模型缓存引擎到引擎适配器
        /// </summary>
        /// <param name="cacher"></param>
        /// <returns></returns>
        public virtual int AddCacheEngine(IModelCacheEngine engine)
        {
            int idx = -1;
            for (int i = 0; i < _modelCacheEngine.Count; i++)
            {
                var m = _modelCacheEngine[i];
                if (m.Level < engine.Level)
                {
                    idx = i;
                    _modelCacheEngine.Insert(idx, engine);
                }
            }
            if (idx < 0)
            {
                _modelCacheEngine.Add(engine);
            }

            return idx;
        }

        public void ClearCacheEngine()
        {
            _modelCacheEngine.Clear();
        }

        #region modelengine


        public virtual IModel GetById(IModel mode, long Mid)
        {
            var ls = GetByIds(mode, new long[] { Mid });
            if (ls != null && ls.Count > 0)
            {
                return ls[0];
            }
            return null;
        }

        public virtual IDictionary<long, IModel> GetByIds(IModel mode, IList<long> Mids)
        {
            var dics = new Dictionary<long, IModel>();
            List<long> nocahceids = new List<long>();
            nocahceids.AddRange(Mids);
            foreach (var m in _modelCacheEngine)
            {
                var dic = m.GetByIds(mode, nocahceids);
                if (dic != null && dic.Count > 0)
                {
                    foreach (var id in dic.Keys)
                    {
                        if (dic[id] != null)
                        {
                            nocahceids.Remove(id);
                            dics.Add(id, dic[id]);
                        }
                    }
                }
                if (nocahceids.Count < 1)
                {
                    break;
                }
            }
            if (nocahceids.Count > 0)
            {

            }

            return null;
        }
        public virtual IDictionary<long, IModel> GetByIdsFromDb(IModel mode, IList<long> Mids)
        {
            var dics = new Dictionary<long, IModel>();
            List<long> nocahceids = new List<long>();
            nocahceids.AddRange(Mids);
            foreach (var m in _modelEngine)
            {
                var dic = m.GetByIds(mode, nocahceids);
                if (dic != null && dic.Count > 0)
                {
                    foreach (var id in dic.Keys)
                    {
                        if (dic[id] != null)
                        {
                            nocahceids.Remove(id);
                            dics.Add(id, dic[id]);
                        }
                    }
                }
                if (nocahceids.Count < 1)
                {
                    break;
                }
            }
            //数据库引擎还取不到的ID就算了
            return dics;
        }

        public virtual IDictionary<long, IModel> GetByOpGuid(IModel mode, string guid)
        {
            foreach (var m in _modelEngine)
            {
                var ls = m.GetByOpGuid(mode, guid);
                if (ls != null && ls.Count > 0)
                {
                    return ls;
                }
            }
            return null;
        }

        public virtual IList<IModel> Filter(IModel mode, Condition condition, MPager mp = null, OrderBy orderby = null)
        {
            var dic = FilterWithId(mode, condition, mp, orderby);
            if (dic != null && dic.Count > 0)
            {
                return dic.Values.ToList();
            }
            return null;
        }

        public virtual IDictionary<long, IModel> FilterWithId(IModel mode, Condition condition, MPager mp = null, OrderBy orderby = null)
        {
            foreach (var m in _modelCacheEngine)
            {
                var dic = m.FilterWithId(mode, condition, mp, orderby);
                if (dic != null && dic.Count > 0)
                {
                    return dic;
                }
            }

            foreach (var m in _modelEngine)
            {
                var dic = m.FilterWithId(mode, condition, mp, orderby);
                if (dic != null && dic.Count > 0)
                {
                    if (_modelCacheEngine.Count > 0)
                    {
                        _modelCacheEngine[0].SetFilterWithId(mode, dic, condition, mp, orderby);
                    }
                    return dic;
                }
            }
            return null;
        }

        public virtual int Update(IModel mode, ValuePair val, Condition condition)
        {
            string op = "";
            return Update(mode, val, condition, out op);
        }

        public virtual int Update(IModel mode, ValuePair val, Condition condition, out string opguid)
        {
            opguid = "";
            foreach (var m in _modelEngine)
            {
                int t = m.Update(mode, val, condition, out opguid);

                if (!string.IsNullOrEmpty(opguid))
                {
                    UpdateCacheByOpGuid(mode, opguid);
                }
            }
            return 0;
        }

        public string Insert(IModel mode)
        {
            string opguid = "";
            foreach (var m in _modelEngine)
            {
                opguid = m.Insert(mode);
                if (!string.IsNullOrEmpty(opguid))
                {
                    UpdateCacheByOpGuid(mode, opguid);
                }
            }
            return opguid;
        }

        #endregion

        #region modeCacheEngine

        public IModel Get(Type type, long id)
        {
            if (type == null || id < 1)
            {
                return null;
            }

            string cachekey = string.Format("{0}:{1}", type.FullName, id.ToString());
            return Get(cachekey);
        }

        public IModel Get<T>(long id)
        {
            return Get(typeof(T), id);
        }

        public IModel Get(string cachekey)
        {
            IModel mode = null;

            if (_modelCacheEngine.Count > 0)
            {
                int i = 0;

                for (i = 0; i < _modelCacheEngine.Count; i++)
                {
                    var m = _modelCacheEngine[i];
                    mode = m.Get(cachekey);
                    if (mode != null)
                    {
                        break;
                    }
                }
                if (i > 0)
                {
                    _modelCacheEngine[0].Set(mode);
                }
            }
            return mode;
        }

        public string Set(string cachekey, IModel mode)
        {
            if (_modelCacheEngine.Count > 0)
            {
                cachekey = _modelCacheEngine[0].Set(cachekey, mode);
            }
            return cachekey;
        }

        public string Set(Type type, IModel mode)
        {
            var dic = MReflection.GetMReflections(mode.zModelType);
            if (dic != null && dic.ContainsKey("Id"))
            {
                object id = dic["Id"].GetValue(mode);
                string cachekey = string.Format("{0}:{1}", type.FullName, id.ToString());
                return Set(cachekey, mode);
            }
            return null;
        }

        public string Set(IModel mode)
        {
            return Set(mode.GetType(), mode);
        }
        #endregion


    }
}