using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oyster.Core.Orm;
using Oyster.Core.Common;
using Oyster.Core.Cache;
using Oyster.Core.Db;

namespace System
{
    public class OyEngine
    {
        public static CacheEngine Cache
        {
            get
            {
                return CacheEngine.Instance;
            }
        }

        public static DbEngine DbHelper
        {
            get
            {
                return DbEngine.Instance;
            }
        }

        public static DbEngineTran DbTran
        {
            get
            {
                return DbEngineTran.Instance;
            }
        }


        public static int AddDbEngine(IModelEngine engine)
        {
            return ModelEngine.Instance.AddDbEngine(engine);
        }
        public static void ClearDbEngine()
        {
            ModelEngine.Instance.ClearDbEngine();
        }
        public static int AddCacheEngine(IModelCacheEngine engine)
        {
            return ModelEngine.Instance.AddCacheEngine(engine);
        }
        public static void ClearCacheEngine()
        {
            ModelEngine.Instance.ClearCacheEngine();
        }

        #region IModelEngine

        public static IModel GetById(Type modeType, long Mid)
        {
            var mode = InstanceHelper.GetInstance(modeType) as IModel;
            if (mode != null)
                return ModelEngine.Instance.GetById(mode, Mid);

            return null;
        }

        public static IDictionary<long, IModel> GetByIds(Type modeType, IList<long> Mids)
        {
            var mode = InstanceHelper.GetInstance(modeType) as IModel;
            if (mode != null)
                return ModelEngine.Instance.GetByIds(mode, Mids);

            return null;
        }

        public static IDictionary<long, IModel> GetByOpGuid(Type modeType, string guid)
        {
            var mode = InstanceHelper.GetInstance(modeType) as IModel;
            if (mode != null)
                return ModelEngine.Instance.GetByOpGuid(mode, guid);

            return null;
        }

        public static IList<IModel> Filter(Type modeType, OyCondition condition, MPager mp = null, OyOrderBy orderby = null)
        {
            var mode = InstanceHelper.GetInstance(modeType) as IModel;
            if (mode != null)
                return ModelEngine.Instance.Filter(mode, condition, mp, orderby);

            return null;
        }

        public static IDictionary<long, IModel> FilterWithId(Type modeType, OyCondition condition, MPager mp = null, OyOrderBy orderby = null)
        {
            var mode = InstanceHelper.GetInstance(modeType) as IModel;
            if (mode != null)
                return ModelEngine.Instance.FilterWithId(mode, condition, mp, orderby);

            return null;
        }

        public static int Update(Type modeType, OyValue val, OyCondition condition)
        {
            var mode = InstanceHelper.GetInstance(modeType) as IModel;
            if (mode != null)
                return ModelEngine.Instance.Update(mode, val, condition);

            return -1;
        }

        public static int Update(Type modeType, OyValue val, OyCondition condition, out string opguid)
        {
            opguid = "";
            var mode = InstanceHelper.GetInstance(modeType) as IModel;
            if (mode != null)
                return ModelEngine.Instance.Update(mode, val, condition, out opguid);

            return -1;
        }

        public static string Insert(IModel mode)
        {
            return ModelEngine.Instance.Insert(mode);
        }

        #endregion

        #region IModelCacheEngine

        public static IModel Get(Type modeType, long id)
        {
            return ModelEngine.Instance.Get(modeType, id);
        }

        public static IModel Get<T>(long id)
        {
            return Get(typeof(T), id);
        }

        public static IModel Get(string cachekey)
        {
            return ModelEngine.Instance.Get(cachekey);
        }

        public static string Set(string cachekey, IModel mode)
        {
            return ModelEngine.Instance.Set(cachekey, mode);
        }

        public static string Set(Type modeType, IModel mode)
        {
            return ModelEngine.Instance.Set(modeType, mode);
        }

        public static string Set(IModel mode)
        {
            return ModelEngine.Instance.Set(mode);
        }
        #endregion

    }

    public class OyEngine<T> : OyEngine
        where T : class,IModel
    {
        #region IModelEngine

        public static T GetById(long Mid)
        {
            return GetById(typeof(T), Mid) as T;
        }

        public static IDictionary<long, T> GetByIds(IList<long> Mids)
        {
            IDictionary<long, T> otdata = new Dictionary<long, T>();
            var data = GetByIds(typeof(T), Mids);
            if (data != null && data.Count > 0)
            {
                foreach (var d in data.Keys)
                {
                    if (data[d] != null && data[d] is T)
                        otdata.Add(d, data[d] as T);
                }
            }
            return otdata;
        }

        public static IDictionary<long, T> GetByOpGuid(string guid)
        {
            IDictionary<long, T> otdata = new Dictionary<long, T>();
            var data = GetByOpGuid(typeof(T), guid); if (data != null && data.Count > 0)
            {
                foreach (var d in data.Keys)
                {
                    if (data[d] != null && data[d] is T)
                        otdata.Add(d, data[d] as T);
                }
            }
            return otdata;
        }

        public static IList<T> Filter(OyCondition condition, MPager mp = null, OyOrderBy orderby = null)
        {
            List<T> outdata = new List<T>();
            var data = Filter(typeof(T), condition, mp, orderby);
            if (data != null && data.Count > 0)
            {
                foreach (var d in data)
                {
                    if (d != null && d is T)
                        outdata.Add(d as T);
                }
            }
            return outdata;
        }

        public static IDictionary<long, T> FilterWithId(OyCondition condition, MPager mp = null, OyOrderBy orderby = null)
        {
            IDictionary<long, T> otdata = new Dictionary<long, T>();
            var data = FilterWithId(typeof(T), condition, mp, orderby);
            if (data != null)
            {
                foreach (var d in data.Keys)
                {
                    if (data[d] != null && data[d] is T)
                        otdata.Add(d, data[d] as T);
                }
            }
            return otdata;
        }

        public static int Update(OyValue val, OyCondition condition)
        {
            return Update(typeof(T), val, condition);
        }

        public static int Update(Type modeType, OyValue val, OyCondition condition, out string opguid)
        {
            return Update(typeof(T), val, condition, out opguid);
        }
        public static string Insert(T mode)
        {
            return ModelEngine.Instance.Insert(mode);
        }
        #endregion

        #region IModelCacheEngine

        public static T Get(long id)
        {
            return ModelEngine.Instance.Get(typeof(T), id) as T; ;
        }

        public static T Get(string cachekey)
        {
            return ModelEngine.Instance.Get(cachekey) as T;
        }

        public static string Set(string cachekey, T mode)
        {
            return ModelEngine.Instance.Set(cachekey, mode);
        }

        public static string Set(Type modeType, T mode)
        {
            return ModelEngine.Instance.Set(modeType, mode);
        }

        public static string Set(T mode)
        {
            return ModelEngine.Instance.Set(mode);
        }
        #endregion
    }
}
