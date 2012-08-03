using System;
using System.Collections.Generic;


namespace Oyster.Core.Orm
{
    public interface IModelCacheEngine
    {
        Imodel GetById(Imodel mode, long Mid);
        IDictionary<long, Imodel> GetByIds(Imodel mode, IList<long> Mids);
        IList<Imodel> Filter(Imodel m, Condition condition, Mpager mp = null, OrderBy orderby = null);
        IDictionary<long, Imodel> FilterWithId(Imodel m, Condition condition, Mpager mp = null, OrderBy orderby = null);
        string SetFilterWithId(Imodel m, IDictionary<long, Imodel> dic, Condition condition, Mpager mp = null, OrderBy orderby = null);
        /// <summary>
        /// 优先级-决定在引擎中的排序，请保证Cache引擎比非Cache优先级高
        /// </summary>
        int Level { get; set; }

        Imodel Get(Type type, long id);
        Imodel Get<T>(long id);
        Imodel Get(string cachekey);

        /// <summary>
        /// 压入缓存
        /// </summary>
        /// <param name="k"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        string Set(string k, Imodel mode);
        /// <summary>
        /// 压入缓存
        /// </summary>
        /// <param name="type"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        string Set(Type type, Imodel mode);

        /// <summary>
        /// 压入缓存
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="timeout">默认缓存50秒</param>
        /// <returns></returns>
        string Set(Imodel mode);

        double CacheTimeOut { get; set; }
    }
}
