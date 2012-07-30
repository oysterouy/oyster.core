using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Oyster.Core.Db;
using System.Data;
using Oyster.Core.Tool;

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
    }
}

namespace System
{
    public class OyTran
    {
        public static OyTran Current
        {
            get
            {
                OyTran tr = OyContext.Instance.GetContext<OyTran>();
                if (tr == null)
                {
                    tr = new OyTran();
                    OyContext.Instance.SetContext<OyTran>(tr);
                }
                return tr;
            }
        }
        public bool IsTraning
        {
            get
            {
                return DbEngineTran.IsTraning;
            }
        }
        public DbEngine DbEngineTran
        {
            get
            {
                DbEngineTran dne = OyContext.Instance.GetContext<DbEngineTran>();
                if (dne == null)
                {
                    dne = new DbEngineTran();
                    OyContext.Instance.SetContext<DbEngineTran>(dne);
                }
                return dne;
            }
        }


        public void Begin(IsolationLevel level = IsolationLevel.Unspecified)
        {
            if (!IsTraning)
                DbEngineTran.BeginTransaction(level);
        }

        public void Commit()
        {
            DbEngineTran.Commit();
        }

        public void Rollback()
        {
            DbEngineTran.Rollback();
        }
    }
}