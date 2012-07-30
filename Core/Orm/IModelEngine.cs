using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oyster.Core.Orm
{
    public interface IModelEngine
    {
        IModel GetById(IModel mode, long Mid);
        IList<IModel> Filter(IModel m, OyCondition condition, MPager mp = null, OyOrderBy orderby = null);
        IDictionary<long, object> FilterWithId(IModel m, OyCondition condition, MPager mp = null, OyOrderBy orderby = null);

        int Update(IModel mode, OyValue val, OyCondition condition);
        int Update(IModel mode, OyValue val, OyCondition condition, out string opguid);

        /// <summary>
        /// 插入数据 操作成功 返回操作KEY（database => opguid;cache => cache key),失败返回null;
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        string Insert(IModel mode);
    }
}
