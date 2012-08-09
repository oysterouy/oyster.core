using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;

namespace ExampleCore
{
    public class Progroum
    {
        static void Main()
        {
            //NewSQLiteDB sl = new NewSQLiteDB();
            //sl.TestSQLite();

            var ls = OyEngine<TCustomerDefaultvaluesTest>.FilterWithId(new OyCondition(TCustomerDefaultvaluesTest.iD, ConditionOperator.Greater, 0)
                , new MPager { PageIndex = 2, PageSize = 20 }, new OyOrderBy(TCustomerDefaultvaluesTest.iD));
            if (ls.Count > 0)
            {
                try
                {
                    OyEngine.DbTran.Begin();
                    OyEngine<TCustomerDefaultvaluesTest>.Update(new OyValue(TCustomerDefaultvaluesTest.cReateTime, DateTime.Now)
                    , new OyCondition(TCustomerDefaultvaluesTest.iD, ConditionOperator.In, ls.Keys.ToArray()));

                    ls = OyEngine<TCustomerDefaultvaluesTest>.FilterWithId(new OyCondition(TCustomerDefaultvaluesTest.iD, ConditionOperator.Greater, 0)
                , new MPager { PageIndex = 2, PageSize = 20 }, new OyOrderBy(TCustomerDefaultvaluesTest.iD));

                    OyEngine.DbTran.Commit();
                }
                finally
                {
                    OyEngine.DbTran.Rollback();
                }
            }
        }
    }
    [OyTestFixture]
    public class NewSQLiteDB
    {
        [OyTest]
        public void TestSQLite()
        {
            string dbPath = ".\\Example.db3";
            //如果不存在改数据库文件，则创建该数据库文件 
            if (!System.IO.File.Exists(dbPath))
            {
                CreateDB(dbPath);
            }

            var u = new ExUsers();
            u.Name = "张三";
            u.Age = 23;
            u.Email = "www@eeee.com";
            u.Status = 2;
            //OyEngine<ExUsers>.Insert(u);

            var cd = new OyCondition(ExUsers.sTatus, ConditionOperator.NotEqual, 9)
                & new OyCondition(ExUsers.aGe, ConditionOperator.Greater, 1);
            var mp = new MPager { PageIndex = 1, PageSize = 2 };
            var us = OyEngine<ExUsers>.Filter(cd, mp);
            if (us != null && us.Count > 0)
            {
                u = us[0];
            }

            OyEngine<ExUsers>.Update(
                new OyValue(ExUsers.nAme, "张山是个好名字")
                & new OyValue(ExUsers.eMail, "zhangsanisgood@qq.com")
                , new OyCondition(ExUsers.iD, u.Id));
        }

        /// <summary>     
        /// 创建SQLite数据库文件     
        /// </summary>     
        /// <param name="dbPath">要创建的SQLite数据库文件路径</param>     
        public void CreateDB(string dbPath)
        {
            //数据库连接只能走配置文件的连接语句
            OyEngine.DbHelper.ExecuteNonQuery(@"CREATE TABLE ex_users(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE
,name varchar(50),age int,sex int,email varchar(200),create_time datatime,lastchange_time,op_guid varchar(100),status
)");
        }
    }
}
