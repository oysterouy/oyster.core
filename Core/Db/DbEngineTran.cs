using System;
using Oyster.Core.Common;
using System.Data;
using System.Collections.Generic;
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

        public static new DbEngineTran Instance
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
        /// <summary>
        /// 在当前上下文开启一个事务，如果事务已开启则不重复开启
        /// 之后使用OyEngine的操作都将直接走本事务的数据库连接
        /// </summary>
        /// <param name="level"></param>
        public void Begin(IsolationLevel level = IsolationLevel.Unspecified)
        {
            if (!IsTraning)
                BeginTransaction(level);
        }

        /// <summary>
        /// 如有事务，提交事务，无，关闭连接后返回
        /// </summary>
        public new void Commit()
        {
            try
            {
                base.Commit();
                if (_onCommitEvent != null)
                {
                    foreach (var act in _onCommitEvent)
                    {
                        if (act != null)
                            act();
                    }
                }

                _onCommitEvent = null;
            }
            catch (Exception ex)
            {
                Logger.Logger.Error("事务提交时异常!", ex);
                Rollback();
            }
        }

        public List<Action> _onCommitEvent;
        /// <summary>
        /// 添加事务提交后执行操作事件
        /// </summary>
        /// <param name="action"></param>
        public void AddCommitEvent(Action action)
        {
            if (_onCommitEvent == null)
            {
                _onCommitEvent = new List<Action>();
            }
            if (IsTraning)
            {
                _onCommitEvent.Add(action);
            }
            else
            {
                action();
            }
        }
        /// <summary>
        /// 如有事务，回滚事务，无，关闭连接后返回
        /// </summary>
        public new void Rollback()
        {
            _onCommitEvent = null;
            base.Rollback();
        }
    }
}