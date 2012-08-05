using System.Collections.Generic;
using System;


namespace Oyster.Core.Orm
{
    public interface IModelEngine
    {
        IModel GetById(IModel mode, long Mid);
        IDictionary<long, IModel> GetByIds(IModel mode, IList<long> Mids);
        IDictionary<long, IModel> GetByOpGuid(IModel mode, string guid);

        IList<IModel> Filter(IModel m, Condition condition, MPager mp = null, OrderBy orderby = null);
        IDictionary<long, IModel> FilterWithId(IModel m, Condition condition, MPager mp = null, OrderBy orderby = null);

        int Update(IModel mode, ValuePair val, Condition condition);
        int Update(IModel mode, ValuePair val, Condition condition, out string opguid);

        /// <summary>
        /// 插入数据 操作成功 返回操作KEY（opguid),失败返回null;
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        string Insert(IModel mode);
        /// <summary>
        /// 优先级-决定在引擎中的排序，请保证Cache引擎比非Cache优先级高
        /// </summary>
        int Level { get; set; }
    }
}
