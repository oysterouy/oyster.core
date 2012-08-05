using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oyster.Core.Orm;
using Oyster.Core.Common;

namespace System
{
    public class OyEngine
    {
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
    {
        #region IModelEngine

        public static IModel GetById(long Mid)
        {
            return GetById(typeof(T), Mid);
        }

        public static IDictionary<long, IModel> GetByIds(IList<long> Mids)
        {
            return GetByIds(typeof(T), Mids);
        }

        public static IDictionary<long, IModel> GetByOpGuid(string guid)
        {
            return GetByOpGuid(typeof(T), guid);
        }

        public static IList<IModel> Filter(OyCondition condition, MPager mp = null, OyOrderBy orderby = null)
        {
            return Filter(typeof(T), condition, mp, orderby);
        }

        public static IDictionary<long, IModel> FilterWithId(OyCondition condition, MPager mp = null, OyOrderBy orderby = null)
        {
            return FilterWithId(typeof(T), condition, mp, orderby);
        }

        public static int Update(OyValue val, OyCondition condition)
        {
            return Update(typeof(T), val, condition);
        }

        public static int Update(Type modeType, OyValue val, OyCondition condition, out string opguid)
        {
            return Update(typeof(T), val, condition, out opguid);
        }

        #endregion

        #region IModelCacheEngine

        public static IModel Get(long id)
        {
            return ModelEngine.Instance.Get(typeof(T), id);
        }
        #endregion
    }
}
