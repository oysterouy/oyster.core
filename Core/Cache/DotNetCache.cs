using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;

namespace Oyster.Core.Cache
{
    public class DotNetCache : Hashtable, ICache
    {
        public DotNetCache()
        {
        }

        protected static DotNetCache _instance;
        public static DotNetCache Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DotNetCache();
                }
                return _instance;
            }
        }

        public string SetValue(string key, object value, TimeSpan tspan = default(TimeSpan)
            , TimeSpan lasttouchspan = default(TimeSpan))
        {
            Set(key, new CacheEntry(key, value, tspan, lasttouchspan));
            return key;
        }

        public void Set(string k, CacheEntry v)
        {
            lock (this)
            {
                if (v != null)
                {
                    v.SetEngine(this);
                    this[k] = v;
                }
            }
        }

        public CacheEntry Get(string k)
        {
            var o = this[k] as CacheEntry;
            if (o != null && !o.Expired)
            {
                return o;
            }
            return null;
        }

        public object GetValue(string k)
        {
            var o = Get(k);
            if (o != null)
            {
                if (o.Expired)
                {
                    this.Remove(k);
                    return null;
                }
                return o.Value;
            }
            return null;
        }

        public bool ContainsKey(string k)
        {
            var o = this[k] as CacheEntry;
            if (o != null)
            {
                if (o.Expired)
                {
                    this.Remove(k);
                    return false;
                }
                return true;
            }
            return false;
        }


        public void Remove(string k)
        {
            base.Remove(k);
        }
    }
}
