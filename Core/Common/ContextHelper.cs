using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Messaging;

namespace Oyster.Core.Common
{
    public class ContextHelper
    {
        protected static ContextHelper instance;
        public static ContextHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ContextHelper();
                }
                return instance;
            }
        }

        public T GetContext<T>()
        {
            string tname = typeof(T).FullName + "_context";
            object o = GetContextByKey(tname);
            if (o is T)
            {
                return (T)o;
            }
            return default(T);
        }

        public void SetContext<T>(T t)
        {
            string tname = typeof(T).FullName + "_context";
            SetContextByKey(tname, t);
        }
        public void SetContextByKey(string k, object o)
        {
            CallContext.SetData(k, o);
        }
        public object GetContextByKey(string k)
        {
            return CallContext.GetData(k);
        }
    }
}
