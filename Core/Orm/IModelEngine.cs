using System.Collections.Generic;


namespace Oyster.Core.Orm
{
    public interface IModelEngine
    {
        Imodel GetById(Imodel mode, long Mid);
        IDictionary<long, Imodel> GetByIds(Imodel mode, IList<long> Mids);
        IDictionary<long, Imodel> GetByOpGuid(Imodel mode, string guid);

        IList<Imodel> Filter(Imodel m, Condition condition, Mpager mp = null, OrderBy orderby = null);
        IDictionary<long, Imodel> FilterWithId(Imodel m, Condition condition, Mpager mp = null, OrderBy orderby = null);

        int Update(Imodel mode, ValuePair val, Condition condition);
        int Update(Imodel mode, ValuePair val, Condition condition, out string opguid);

        /// <summary>
        /// 插入数据 操作成功 返回操作KEY（opguid),失败返回null;
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        string Insert(Imodel mode);
        /// <summary>
        /// 优先级-决定在引擎中的排序，请保证Cache引擎比非Cache优先级高
        /// </summary>
        int Level { get; set; }
    }
}
