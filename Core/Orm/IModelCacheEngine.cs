using System;
using System.Collections.Generic;


namespace Oyster.Core.Orm
{
    public interface IModelCacheEngine
    {
        IModel GetById(IModel mode, long Mid);
        IDictionary<long, IModel> GetByIds(IModel mode, IList<long> Mids);
        IList<IModel> Filter(IModel m, Condition condition, MPager mp = null, OrderBy orderby = null);
        IDictionary<long, IModel> FilterWithId(IModel m, Condition condition, MPager mp = null, OrderBy orderby = null);
        string SetFilterWithId(IModel m, IDictionary<long, IModel> dic, Condition condition, MPager mp = null, OrderBy orderby = null);
        /// <summary>
        /// 优先级-决定在引擎中的排序，请保证Cache引擎比非Cache优先级高
        /// </summary>
        int Level { get; set; }

        IModel Get(Type type, long id);
        IModel Get<T>(long id);
        IModel Get(string cachekey);

        /// <summary>
        /// 压入缓存
        /// </summary>
        /// <param name="k"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        string Set(string k, IModel mode);
        /// <summary>
        /// 压入缓存
        /// </summary>
        /// <param name="type"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        string Set(Type type, IModel mode);

        /// <summary>
        /// 压入缓存
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="timeout">默认缓存50秒</param>
        /// <returns></returns>
        string Set(IModel mode);

        double CacheTimeOut { get; set; }
    }
}
