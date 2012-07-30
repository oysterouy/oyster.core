using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oyster.Core.Cache
{
    public interface ICache
    {
        void Set(string k, CacheEntry v);
        CacheEntry Get(string k);
        bool ContainsKey(string k);

        string SetValue(string key, object value, TimeSpan tspan = default(TimeSpan)
            , TimeSpan lasttouchspan = default(TimeSpan));

        object GetValue(string k);

        void Remove(string k);
    }
}
