using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oyster.Core.Common
{
    public class InstanceHelper
    {
        protected static Dictionary<Type, object> _instances = new Dictionary<Type, object>();
        public static object GetInstance(Type tname)
        {
            object t = null;
            lock (tname)
            {
                if (!_instances.ContainsKey(tname))
                {
                    t = Activator.CreateInstance(tname);
                    _instances.Add(tname, t);
                }
                else
                {
                    t = _instances[tname];
                }
            }
            return t;
        }
    }
    public class InstanceHelper<T> : InstanceHelper
    {
        public static T Instance
        {
            get
            {
                return (T)GetInstance(typeof(T));
            }
        }
    }
}
