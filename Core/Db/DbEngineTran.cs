using System;
using Oyster.Core.Common;
namespace Oyster.Core.Db
{
    public class DbEngineTran : DbEngine, IDisposable
    {
        public DbEngineTran(string tablename = "")
            : base(DbEngine.NewDbConnection(tablename))
        {

        }

        /// <summary>
        /// 必须手动开启，提交事务，否则上下文过期时就会撤销事务
        /// </summary>
        public void Dispose()
        {
            if (IsTraning)
            {
                this.Rollback();
            }
            else
            {
                this.Close(true);
            }
        }

        public static new DbEngine Instance
        {
            get
            {
                DbEngineTran tr = ContextHelper.Instance.GetContext<DbEngineTran>();
                if (tr == null)
                {
                    tr = new DbEngineTran();
                    ContextHelper.Instance.SetContext<DbEngineTran>(tr);
                }
                return tr;
            }
        }
    }
}