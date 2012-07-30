using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Xml;

namespace Oyster.Web.Mvc.Config
{
    public class ConfigEnginer
    {
        private static ConfigEnginer _instance;
        /// <summary>
        /// 配置管理引擎
        /// </summary>
        public static ConfigEnginer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConfigEnginer();
                }
                return _instance;
            }
        }

        protected ConfigEnginer()
        {
            string configpath = ConfigurationManager.AppSettings["OysterConfig"];
            if (string.IsNullOrEmpty(configpath))
            {
                throw new Exception("You must set the oysterconfig path in you web(app).config appsetings");
            }
            string basedir = System.AppDomain.CurrentDomain.BaseDirectory;
            if (!string.IsNullOrEmpty(basedir) && basedir[basedir.Length - 1] == Path.DirectorySeparatorChar)
            {
                basedir = basedir.Substring(0, basedir.Length - 1);
            }
            string path = basedir + configpath;
            string pathdir = basedir;
            if (!File.Exists(path))
            {
                path = basedir + "/bin" + configpath;
                pathdir = basedir + "/bin";
            }

            if (File.Exists(path))
            {
                XmlDocument xconfig = new XmlDocument();
                var s = File.ReadAllText(path);
                xconfig.LoadXml(s);
                var setings = xconfig.SelectSingleNode("Settings");
                if (setings != null)
                {
                    XmlNode xmvc = setings.SelectSingleNode("Mvc");
                    if (xmvc != null)
                    {
                        MvcConfig.Instance.Init(xmvc);
                    }
                    //xmvc = setings.SelectSingleNode("Log");
                    //if (xmvc != null)
                    //{
                    //    LogConfig.Instance.Init(xmvc);
                    //}
                    //xmvc = setings.SelectSingleNode("ErrorPages");
                    //if (xmvc != null)
                    //{
                    //    ErrorPagesConfig.Instance.Init(xmvc);
                    //}
                }
            }
        }

        public void Init()
        {

        }
    }
}
