using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Oyster.Web.Mvc
{
    public class MvcEnginer
    {
        private static MvcEnginer _instance;
        /// <summary>
        /// 配置
        /// </summary>
        public static MvcEnginer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MvcEnginer();
                }
                return _instance;
            }
        }
    }
}
