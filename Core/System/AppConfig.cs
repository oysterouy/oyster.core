using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oyster
{
    /// <summary>
    /// 需要改变默认配置需要继承本类
    /// 重载属性
    /// 并在Application_start(){ 
    /// AppConfig.Instance.SetInstance(new LocalAppConfig());
    /// }
    /// </summary>
    public class AppConfig
    {
        protected AppConfig() { }

        protected static AppConfig _instance;

        public static AppConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppConfig();
                }
                return _instance;
            }
        }
        /// <summary>
        /// 变更系统配置类的单例的对象实例
        /// </summary>
        /// <param name="instance">继承AppConfig 的子类实例</param>
        /// <returns></returns>
        public AppConfig SetInstance(AppConfig instance)
        {
            _instance = instance;
            return Instance;
        }
        /// <summary>
        /// 查询缓存时间 0.2 秒，默认保证当前页面请求不重复读取数据
        /// </summary>
        public virtual int CacheFilter
        {
            get
            {
                return 200;
            }
        }
        /// <summary>
        /// 数据库连接断开时间 秒
        /// </summary>
        public virtual int DbConnectionTimeOut
        {
            get
            {
                return 10;
            }
        }
        /// <summary>
        /// 数据实例默认缓存0.2秒，同一应用程序的缓存实例会被更新数据库操作更新
        /// </summary>
        public double ModelCacheTime
        {
            get { return 200; }
        }
    }
}
