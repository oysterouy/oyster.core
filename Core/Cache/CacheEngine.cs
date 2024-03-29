﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oyster.Core.Common;

namespace Oyster.Core.Cache
{
    /// <summary>
    /// 需要定义缓存引擎执行顺序的需要继承本类,重载Init方法
    /// 默认已添加DotNetCache，如需替换，请支持Instance.ClearEngine后重新添加
    /// </summary>
    public class CacheEngine : ICache
    {
        protected IList<ICache> _cacheEngine;
        protected CacheEngine()
        {
            Init();
        }

        protected virtual void Init()
        {
            _cacheEngine = new List<ICache>();
            _cacheEngine.Add(DotNetCache.Instance);
        }

        protected static CacheEngine _instance;
        public static CacheEngine Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CacheEngine();
                }
                return _instance;
            }
        }

        /// <summary>
        /// 添加缓存引擎到引擎适配器
        /// </summary>
        /// <param name="cacher"></param>
        /// <returns></returns>
        public virtual int AddEngine(ICache cacher)
        {
            _cacheEngine.Add(cacher);
            return _cacheEngine.Count - 1;
        }

        public void ClearEngine()
        {
            _cacheEngine.Clear();
        }


        public void Set(string k, CacheEntry v)
        {
            foreach (var c in _cacheEngine)
            {
                v.SetEngine(c);
                c.Set(k, v);
            }
        }

        public CacheEntry Get(string k)
        {
            string key = k;

            CacheEntry ce = null;
            foreach (var c in _cacheEngine)
            {
                ce = c.Get(k);
                if (ce != null)
                {
                    return ce;
                }
            }
            return null;
        }

        public bool ContainsKey(string k)
        {
            var ce = Get(k);
            if (ce != null && !ce.Expired)
            {
                return true;
            }
            return false;
        }

        public string SetValue(string key, object value, TimeSpan tspan = default(TimeSpan), TimeSpan lasttouchspan = default(TimeSpan))
        {
            Set(key, new CacheEntry(key, value, tspan, lasttouchspan));
            return key;
        }

        public object GetValue(string k)
        {
            var o = Get(k);
            if (o != null && !o.Expired)
            {
                return o.Value;
            }
            return null;
        }

        /// <summary>
        /// 当前上下文缓存临时过期时间，单位毫秒
        /// 以便一些特殊情况下使用，只会影响本次页面请求。
        /// 如：
        ///  OyEngine.C.CurrentTimeOut = 50;//设置过期 50毫秒，本次请求从数据库获取一次数据
        /// var u = OyEngine.M《ElUsers》.GetById(uid); //获取数据
        /// OyEngine.C.CurrentTimeOut = 0;//设置回默认值
        /// </summary>
        public double CurrentTimeOut
        {
            get
            {
                object tout = ContextHelper.Instance.GetContextByKey("CacheEngine.CurrentTimeOut");
                if (tout != null && tout is Double)
                {
                    return (double)tout;
                }
                return 0;
            }
            set
            {
                ContextHelper.Instance.SetContextByKey("CacheEngine.CurrentTimeOut", value);
            }
        }


        /// <summary>
        /// 删除所有缓存引擎的缓存KEY 对应值
        /// 一般不应该使用本方法，要直接指定到具体引擎进行删除。
        /// </summary>
        /// <param name="k"></param>
        public void Remove(string k)
        {
            foreach (var c in _cacheEngine)
            {
                c.Remove(k);
            }
        }
    }
}
