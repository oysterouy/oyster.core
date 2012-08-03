using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Oyster.Core.Db
{
    public partial class DbEngine
    {

        #region ExecuteNonQuery
        /// <summary>
        /// 执行SQL语句，返回影响行数
        /// </summary>
        /// <returns></returns>
        public virtual int ExecuteNonQuery(string sql)
        {
            int rtvalue = -1;
            try
            {
                var ocmd = NewCommand();
                ocmd.CommandText = sql;
                ocmd.CommandType = CommandType.Text;
                Open();
                rtvalue = ocmd.ExecuteNonQuery();
            }
            catch (Exception ee)
            {
                rtvalue = -1;
                Logger.Logger.Error(string.Format("DB:{0}", sql), ee);
            }
            finally { Close(); }

            return rtvalue;
        }
        /// <summary>
        /// 执行SQL语句，返回影响行数
        /// </summary>
        /// <returns></returns>
        public virtual int ExecuteNonQuery(string sql, string[] args)
        {
            StringBuilder sbder = new StringBuilder();
            sbder.AppendFormat(sql, args);
            return ExecuteNonQuery(sbder.ToString());
        }
        /// <summary>
        /// 执行SQL语句，返回影响行数
        /// </summary>
        /// <returns></returns>
        public virtual int ExecuteNonQuery(string sql, IDataParameter param)
        {
            return ExecuteNonQuery(sql, new IDataParameter[] { param });
        }
        /// <summary>
        /// 执行SQL语句，返回影响行数
        /// </summary>
        /// <returns></returns>
        public virtual int ExecuteNonQuery(string sql, IList<IDataParameter> paramters)
        {
            int rtvalue = -1;
            try
            {
                var ocmd = NewCommand(sql, paramters);
                Open();
                rtvalue = ocmd.ExecuteNonQuery();
            }
            catch (Exception ee)
            {
                rtvalue = -1;
                Logger.Logger.Error(string.Format("DB:{0}", sql), ee);
            }
            finally { Close(); }

            return rtvalue;
        }

        #endregion

        #region ExecuteScalar
        /// <summary>
        /// 执行SQL语句，返回查询数据
        /// </summary>
        /// <returns></returns>
        public virtual object ExecuteScalar(string sql)
        {
            object rtvalue = null;
            try
            {
                var ocmd = NewCommand(sql);
                Open();
                rtvalue = ocmd.ExecuteScalar();
            }
            catch (Exception ee)
            {
                rtvalue = null;
                Logger.Logger.Error(string.Format("DB:{0}", sql), ee);
            }
            finally { Close(); }

            return rtvalue;
        }
        /// <summary>
        /// 执行SQL语句，返回查询数据
        /// </summary>
        /// <returns></returns>
        public virtual object ExecuteScalar(string sql, string[] args)
        {
            StringBuilder sbder = new StringBuilder();
            sbder.AppendFormat(sql, args);
            return ExecuteScalar(sbder.ToString());
        }
        /// <summary>
        /// 执行SQL语句，返回查询数据
        /// </summary>
        /// <returns></returns>
        public virtual object ExecuteScalar(string sql, IDataParameter param)
        {
            return ExecuteScalar(sql, new IDataParameter[] { param });
        }
        /// <summary>
        /// 执行SQL语句，返回查询数据
        /// </summary>
        /// <returns></returns>
        public virtual object ExecuteScalar(string sql, IList<IDataParameter> paramters)
        {
            object rtvalue = null;
            try
            {
                var ocmd = NewCommand(sql, paramters);
                Open();
                rtvalue = ocmd.ExecuteScalar();
            }
            catch (Exception ee)
            {
                rtvalue = null;
                Logger.Logger.Error(string.Format("DB:{0}", sql), ee);
            }
            finally { Close(); }

            return rtvalue;
        }

        #endregion

        #region ExecuteReader
        /// <summary>
        /// 执行SQL语句，返回查询数据
        /// </summary>
        /// <returns></returns>
        public virtual IDataReader ExecuteReader(string sql)
        {
            IDataReader rtvalue = null;
            try
            {
                var ocmd = NewCommand(sql);
                Open();
                rtvalue = ocmd.ExecuteReader();
            }
            catch (Exception ee)
            {
                rtvalue = null;
                Logger.Logger.Error(string.Format("DB:{0}", sql), ee);
            }
            finally
            {
                //Reader需要外面关闭连接
            }
            return rtvalue;
        }

        public virtual IDataReader ExecuteReader(string sql, string[] args)
        {
            StringBuilder sbder = new StringBuilder();
            sbder.AppendFormat(sql, args);
            return ExecuteReader(sbder.ToString());
        }

        public virtual IDataReader ExecuteReader(string sql, IDataParameter param)
        {
            return ExecuteReader(sql, new IDataParameter[] { param });
        }

        public virtual IDataReader ExecuteReader(string sql, IList<IDataParameter> paramters)
        {
            IDataReader rtvalue = null;
            try
            {
                var ocmd = NewCommand(sql, paramters);
                Open();
                rtvalue = ocmd.ExecuteReader();
            }
            catch (Exception ee)
            {
                rtvalue = null;
                Logger.Logger.Error(string.Format("DB:{0}", sql), ee);
            }
            finally
            {
                //Reader需要外面关闭连接
            }
            return rtvalue;
        }

        public virtual DataTable GetSchemaTable(string sql)
        {
            IDataReader rd = null;
            DataTable dt = null;
            try
            {
                rd = ExecuteReader(sql);
                dt = rd.GetSchemaTable();
            }
            catch (Exception ex) { }
            finally
            {
                if (rd != null)
                {
                    rd.Close();
                }
            }
            return dt == null ? new DataTable() : dt;
        }

        #endregion

        #region ExecuteQuery
        /// <summary>
        /// 执行SQL语句，返回查询数据
        /// </summary>
        /// <returns></returns>
        public virtual DataSet ExecuteQuery(string sql)
        {
            DataSet rtvalue = new DataSet();
            try
            {
                var adapter = NewDataAdapter(sql);
                Open();
                adapter.Fill(rtvalue);
            }
            catch (Exception ee)
            {
                rtvalue = null;
                Logger.Logger.Error(string.Format("DB:{0}", sql), ee);
            }
            finally { Close(); }

            return rtvalue;
        }
        /// <summary>
        /// 执行SQL语句，返回查询数据
        /// </summary>
        /// <returns></returns>
        public virtual DataSet ExecuteQuery(string sql, string[] args)
        {
            StringBuilder sbder = new StringBuilder();
            sbder.AppendFormat(sql, args);
            return ExecuteQuery(sbder.ToString());
        }
        /// <summary>
        /// 执行SQL语句，返回查询数据
        /// </summary>
        /// <returns></returns>
        public virtual DataSet ExecuteQuery(string sql, IDataParameter param)
        {
            return ExecuteQuery(sql, new IDataParameter[] { param });
        }
        /// <summary>
        /// 执行SQL语句，返回查询数据
        /// </summary>
        /// <returns></returns>
        public virtual DataSet ExecuteQuery(string sql, IList<IDataParameter> paramters)
        {
            DataSet rtvalue = new DataSet();
            try
            {
                var adapter = NewDataAdapter(sql, paramters);
                Open();
                adapter.Fill(rtvalue);
            }
            catch (Exception ee)
            {
                rtvalue = null;
                Logger.Logger.Error(string.Format("DB:{0}", sql), ee);
            }
            finally { Close(); }

            return rtvalue;
        }
        /// <summary>
        /// 执行SQL语句，返回查询数据
        /// </summary>
        /// <returns></returns>
        public virtual DataSet ExecuteQuery(string sql, int pageindex, int pagesize, out int rowscount)
        {
            return ExecuteQuery(GetPagerSql(sql, pageindex, pagesize, out rowscount));
        }
        /// <summary>
        /// 执行SQL语句，返回查询数据
        /// </summary>
        /// <returns></returns>
        public virtual DataSet ExecuteQuery(string sql, string[] args, int pageindex, int pagesize, out int rowscount)
        {
            return ExecuteQuery(GetPagerSql(sql, pageindex, pagesize, out rowscount), args);
        }
        /// <summary>
        /// 执行SQL语句，返回查询数据
        /// </summary>
        /// <returns></returns>
        public virtual DataSet ExecuteQuery(string sql, IDataParameter param, int pageindex, int pagesize, out int rowscount)
        {
            return ExecuteQuery(GetPagerSql(sql, pageindex, pagesize, out rowscount), param);
        }
        /// <summary>
        /// 执行SQL语句，返回查询数据
        /// </summary>
        /// <returns></returns>
        public virtual DataSet ExecuteQuery(string sql, IList<IDataParameter> paramters, int pageindex, int pagesize, out int rowscount)
        {
            return ExecuteQuery(GetPagerSql(sql, pageindex, pagesize, out rowscount), paramters);
        }
        #endregion

        #region Pager
        /// <summary>
        /// 获取带分页的SQL
        /// </summary>
        /// <returns></returns>
        public virtual string GetPagerSql(string sql, int pageindex, int pagesize, out int rowscount, IList<IDataParameter> plist = null)
        {
            switch (Providertype)
            {
                default:
                case "Mysql":
                    return GetMySqlPagerSql(sql, pageindex, pagesize, out rowscount, plist);
                case "Mssql":
                    return GetMsSqlPagerSql(sql, pageindex, pagesize, out rowscount, plist);
                case "Oracle":
                    return GetOraclePagerSql(sql, pageindex, pagesize, out rowscount, plist);
            }
        }

        public virtual string GetMySqlPagerSql(string sql, int pageindex, int pagesize, out int rowscount, IList<IDataParameter> plist = null)
        {
            string rtvalue = null;
            rowscount = 0;
            try
            {
                string countsql = string.Format("select count(id) as count_ from ({0}) ct", sql);
                rowscount = Convert.ToInt32(ExecuteScalar(countsql, plist));
                string querysql = "{0} limit {1},{2} ";
                pageindex = pageindex < 1 ? 1 : pageindex;

                int row_min = pageindex * pagesize - pagesize, row_max = pageindex * pagesize;
                StringBuilder querybuilder = new StringBuilder();
                querybuilder.AppendFormat(querysql, new string[] { sql, row_min.ToString(), row_max.ToString() });

                rtvalue = querybuilder.ToString();
            }
            catch (Exception ee)
            {
                rtvalue = null;
                Logger.Logger.Error(string.Format("DB:{0}", sql), ee);
            }
            finally { }
            return rtvalue;
        }

        public virtual string GetMsSqlPagerSql(string sql, int pageindex, int pagesize, out int rowscount, IList<IDataParameter> plist = null)
        {
            throw new Exception("未实现");
        }

        public virtual string GetOraclePagerSql(string sql, int pageindex, int pagesize, out int rowscount, IList<IDataParameter> plist = null)
        {
            string rtvalue = null;
            rowscount = 0;
            try
            {
                string countsql = string.Format("select count(id) as count_ from ({0}) ct", sql);
                rowscount = Convert.ToInt32(ExecuteScalar(countsql, plist));

                bool onleft = true;
                //判断当前取的记录是在整个集合的靠前还是靠后，以便稍微提高一点点查询速度
                if ((pageindex * pagesize) > (rowscount / 2))
                {
                    onleft = false;
                }
                //{0}原SQL，{1}记录小于的ROWNUM，{2}记录大于等于的ROWNUM。

                string leftquerysql = "select * from (select q1.*,ROWNUM AS ROWNUM_ from ({0}) q1 where ROWNUM<={1}) q2 where ROWNUM>={2}";
                string rightquerysql = "select * from (select q1.*,ROWNUM AS ROWNUM_ from ({0}) q1 where ROWNUM>={2}) q2 where ROWNUM<={1}";
                string querysql = onleft ? leftquerysql : rightquerysql;

                //Index从1开始
                int row_min = pageindex * pagesize - pagesize + 1, row_max = pageindex * pagesize;
                StringBuilder querybuilder = new StringBuilder();
                querybuilder.AppendFormat(querysql, new string[] { sql, row_max.ToString(), row_min.ToString() });

                rtvalue = querybuilder.ToString();
            }
            catch (Exception ee)
            {
                rtvalue = null;
                Logger.Logger.Error(string.Format("DB:{0}", sql), ee);
            }
            finally { }
            return rtvalue;
        }

        #endregion
    }
}
