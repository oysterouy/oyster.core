using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oyster.Core.Orm;
using System.Collections;

namespace Oyster.Core.Cache
{
    public class ModelCacheEntry
    {
        protected Type _zmodetype;
        public Type zModeType
        {
            get
            {
                return _zmodetype;
            }
        }

        public ModelCacheEntry(Type zmtp)
        {
            _zmodetype = zmtp;
            _cache = new DotNetCache();
        }

        DotNetCache _cache;

        public Hashtable HtData
        {
            get
            {
                return _cache as Hashtable;
            }
        }

        public string SetValue(IModel mode, TimeSpan tspan = default(TimeSpan)
            , TimeSpan lasttouchspan = default(TimeSpan))
        {
            var ps = MReflection.GetMReflections(mode.zModelType);
            object id = ps["Id"].GetValue(mode);
            string k = string.Format("Model-{0}:{1}", mode.zModelType.FullName, id.ToString());

            if (tspan == TimeSpan.Zero && lasttouchspan == TimeSpan.Zero)
            {
                tspan = TimeSpan.FromMilliseconds(AppConfig.Instance.ModelCacheTime);
            }
            var cget = _cache.Get(k);
            if (cget == null)
            {
                _cache.SetValue(k, mode, tspan, lasttouchspan);
            }
            else
            {
                cget.Value = mode;
            }
            return k;
        }

        public IModel GetValue(string k)
        {
            return _cache.GetValue(k) as IModel;
        }
        public CacheEntry Get(string k)
        {
            return _cache.Get(k);
        }

        public int Update(IModel mode, OyValue val, OyCondition condition, out string opguid)
        {
            opguid = "";
            foreach (var v in _cache.Values)
            {

            }

            return -1;
        }
    }
}
