using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oyster.Core.Cache
{
    public class CacheEntry : IDisposable
    {
        protected object _value;
        protected ICache _engine;

        public string Key;
        public object Value
        {
            get
            {
                TouchTime = DateTime.Now;
                return _value;
            }
            internal set
            {
                TouchTime = DateTime.Now;
                _value = value;
            }
        }
        public TimeSpan TimeOut;
        public TimeSpan LastTouchTimeOut;

        public DateTime CreateTime;
        public DateTime TouchTime;

        public bool Expired
        {
            get
            {
                bool ret = false;
                if (TimeOut != TimeSpan.Zero)
                {
                    if ((DateTime.Now - CreateTime) > TimeOut)
                    {
                        ret = true;
                    }
                }
                else if (LastTouchTimeOut != TimeSpan.Zero)
                {
                    if ((DateTime.Now - TouchTime) > LastTouchTimeOut)
                    {
                        ret = true;
                    }
                }
                //实际过期，释放资源
                if (ret)
                {
                    Dispose();
                    return true;
                }
                else
                {
                    double crtmout = CacheEngine.Instance.CurrentTimeOut;
                    if (crtmout > 0)
                    {
                        if ((DateTime.Now - CreateTime) > TimeSpan.FromMilliseconds(crtmout))
                        {
                            //当前过期
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 缓存实体
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="val">缓存数据</param>
        /// <param name="tspan">固定过期间隔时间</param>
        /// <param name="lasttouchspan">最后访问过期间隔时间</param>
        public CacheEntry(ICache engine, string key, object val, TimeSpan tspan = default(TimeSpan)
            , TimeSpan lasttouchspan = default(TimeSpan))
        {
            Key = key;
            _value = val;
            _engine = engine;
            CreateTime = DateTime.Now;
            TouchTime = DateTime.Now;

            TimeOut = tspan;
            LastTouchTimeOut = lasttouchspan;
        }

        public void Dispose()
        {
            _value = null;
            _engine.Remove(Key);
        }
    }
}
