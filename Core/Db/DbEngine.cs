using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data.OracleClient;
using System.Configuration;

namespace Oyster.Core.Db
{
    public partial class DbEngine
    {
        /// <summary>
        /// 使用本构造函数必须创建一个新类继承本类，并对DbConnection赋值!
        /// 如：
        /// class A:DbEngine{
        ///     protected DbEngine(string connectionstr)
        ///     {
        ///         DbConnection=new *********;
        ///     }
        /// }
        /// 否则后续操作都会报错
        /// </summary>
        /// <param name="connectionstr"></param>
        protected DbEngine(string connectionstr)
        {

        }

        protected DbEngine(IDbConnection _dbcon)
        {
            DbConnection = _dbcon;
        }

        private static DbEngine _instance;
        /// <summary>
        /// 数据库操作引擎
        /// </summary>
        public static DbEngine Instance
        {
            get
            {
                //启动了事务的数据库操作，全部走当前上下文的数据库操作引擎
                if (DbEngineTran.Instance.IsTraning)
                {
                    return DbEngineTran.Instance;
                }
                if (_instance == null)
                {
                    _instance = NewEngine();
                }
                return _instance;
            }
        }

        public static IDbConnection NewDbConnection(string tablename = null)
        {
            var cset = ConnetionSetting(tablename);
            IDbConnection connect = null;
            switch (cset.ProviderName.ToLower())
            {
                case "mysql":
                    connect = new MySqlConnection(cset.ConnectionString);
                    break;
                case "mssql":
                    connect = new SqlConnection(cset.ConnectionString);
                    break;
                case "oracle":
                    connect = new OracleConnection(cset.ConnectionString);
                    break;
            }
            return connect;
        }

        public static DbEngine NewEngine(string tablename = null)
        {
            DbEngine engine = null;
            try
            {
                IDbConnection connect = NewDbConnection(tablename);
                if (connect != null)
                {
                    engine = new DbEngine(connect);
                }
                else
                {
                    var cset = ConnetionSetting(tablename);
                    Type t = Type.GetType(cset.ProviderName);
                    if (t != null)
                    {
                        engine = Activator.CreateInstance(t, new object[] { cset.ConnectionString }) as DbEngine;
                    }
                    else
                    {
                        throw new Exception("请在配置文件connectionStrings节点中配置连接语句DbEngineConnectionString的ProviderName项为“mysql or mssql or oracle or 继承DbEngine的子类完全类名”,以明确当前使用的引擎!");
                    }
                }

                if (engine == null)
                {
                    throw new Exception("请在配置文件connectionStrings节点中配置连接语句DbEngineConnectionString的ProviderName项为“mysql or mssql or oracle or 继承DbEngine的子类完全类名”,以明确当前使用的引擎!");
                }
            }
            catch (Exception ee)
            {
                throw ee;
            }
            return engine;
        }

        /// <summary>
        /// 获取当前使用数据库引擎的数据参数类型实例
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IDataParameter NewDataParameter(string name)
        {
            IDataParameter p = null;
            switch (Instance.Providertype)
            {
                case "Mssql":
                    p = new SqlParameter("@" + name, null);
                    break;
                case "MySql":
                    p = new MySqlParameter("?" + name, null);
                    break;
                case "Oracle":
                    p = new OracleParameter(":" + name, null);
                    break;
                default:
                    p = new MySqlParameter("?" + name, null);
                    break;
            }
            return p;
        }

        /// <summary>
        /// <add name="DbEngineConnectionString" connectionString="Data Source=datasouce;Persist Security Info=True;User ID=dbuser;Password=dbuserpwd;Unicode=True;Pooling=true;Min Pool Size=1;Max Pool Size=10" providerName="Oyster.Core.VoEngine.Db.OracleDbEngine"/>
        /// </summary>
        public static ConnectionStringSettings DefaultConnetionSetting
        {
            get
            {
                return ConnetionSetting(null);
            }
        }

        /// <summary>
        /// <add name="DbEngineConnectionString(t_user,t_custem)" connectionString="Data Source=datasouce;Persist Security Info=True;User ID=dbuser;Password=dbuserpwd;Unicode=True;Pooling=true;Min Pool Size=1;Max Pool Size=10" providerName="Oyster.Core.VoEngine.Db.OracleDbEngine"/>
        /// </summary>
        public static ConnectionStringSettings ConnetionSetting(string tablename)
        {
            ConnectionStringSettings csset = null;
            foreach (ConnectionStringSettings cs in ConfigurationManager.ConnectionStrings)
            {
                if (cs != null && cs.Name.StartsWith("DbEngineConnectionString"))
                {
                    if (string.IsNullOrEmpty(tablename))
                    {
                        csset = cs;
                        break;
                    }
                    else
                    {
                        csset = csset == null ? cs : csset;
                        if (cs.Name.Contains(tablename + ",") || cs.Name.Contains(tablename + ")") || cs.Name.Contains(tablename + " "))
                        {
                            csset = cs;
                            break;
                        }
                    }
                }
            }
            if (csset == null)
            {
                throw new Exception("must set DbEngineConnectionString in your web.config  <connectionStrings>!");
            }
            return csset;
        }


        /// <summary>
        /// 当前的引擎类型 - Mysql Mssql Oracle
        /// </summary>
        public string Providertype
        {
            get
            {
                if (DbConnection is MySqlConnection)
                {
                    return "Mysql";
                }
                if (DbConnection is SqlConnection)
                {
                    return "Mssql";
                }
                if (DbConnection is OracleConnection)
                {
                    return "Oracle";
                }
                return "Mysql";
            }
        }

        protected IDbConnection DbConnection;

        protected IDbTransaction DbTransaction;

        public bool IsTraning
        {
            get
            {
                return DbTransaction != null;
            }
        }

        protected DateTime LastOpConnection = DateTime.MinValue;

        public void BeginTransaction(IsolationLevel il = IsolationLevel.Unspecified)
        {
            DbConnection.Open();
            DbTransaction = DbConnection.BeginTransaction(il);
        }

        public void Commit()
        {
            if (IsTraning)
            {
                DbTransaction.Commit();
                DbTransaction.Dispose();
                DbTransaction = null;
                Close(true);
            }
        }

        public void Rollback()
        {
            if (IsTraning)
            {
                DbTransaction.Rollback();
                DbTransaction.Dispose();
                DbTransaction = null;
                Close(true);
            }
            else
            {
                Close(true);
            }
        }

        public void Open()
        {
            try
            {
                if (DbConnection.State != ConnectionState.Open)
                {
                    DbConnection.Open();
                    LastOpConnection = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Close(bool f = false)
        {
            try
            {
                if (IsTraning)
                {
                    return;
                }
                if (f || DateTime.Now > LastOpConnection.AddSeconds(AppConfig.Instance.DbConnectionTimeOut))
                {
                    if (DbConnection.State != ConnectionState.Closed)
                    {
                        DbConnection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected IDbCommand NewCommand(string sql = "", IList<IDataParameter> paramters = null)
        {
            var cmd = DbConnection.CreateCommand();
            if (IsTraning)
            {
                cmd.Transaction = DbTransaction;
            }
            if (!string.IsNullOrEmpty(sql))
            {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                if (paramters != null && paramters.Count > 0)
                {
                    foreach (var v in paramters)
                    {
                        cmd.Parameters.Add(v);
                    }
                }
            }
            return cmd;
        }

        protected IDataAdapter NewDataAdapter(string sql, IList<IDataParameter> paramters = null)
        {
            IDataAdapter adapter = null;

            var cmd = NewCommand(sql, paramters);

            switch (Providertype)
            {
                case "Mysql":
                    adapter = new MySqlDataAdapter(cmd as MySqlCommand);
                    break;
                case "Mssql":
                    adapter = new SqlDataAdapter(cmd as SqlCommand);
                    break;
                case "Oracle":
                    adapter = new OracleDataAdapter(cmd as OracleCommand);
                    break;
            }

            return adapter == null ? new MySqlDataAdapter(cmd as MySqlCommand) : adapter;
        }

        /// <summary>
        /// 使用ParameterCollection，自动根据数据引擎转换参数格式和名称，和避免重名
        /// </summary>
        /// <returns></returns>
        public static ParameterCollection NewParameters()
        {
            return new ParameterCollection();
        }
    }
}
