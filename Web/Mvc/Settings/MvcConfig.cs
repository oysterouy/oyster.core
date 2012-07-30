using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web.Mvc;

namespace Oyster.Web.Mvc.Config
{
    public class MvcConfig
    {
        #region 方法

        private static MvcConfig _instance;
        /// <summary>
        /// 配置
        /// </summary>
        public static MvcConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MvcConfig();
                }
                return _instance;
            }
        }

        protected MvcConfig()
        {
            HadConfig = false;
        }
        /// <summary>
        /// 是否有配置文件
        /// </summary>
        public bool HadConfig
        {
            get;
            protected set;
        }
        /// <summary>
        /// OysterConfig 中的MVC节点
        /// </summary>
        /// <param name="xdoc"></param>
        public void Init(XmlNode xdoc)
        {
            if (xdoc != null)
            {
                #region Routes
                var routes = xdoc.SelectNodes("Routes/Route");
                if (routes != null && routes.Count > 0)
                {
                    foreach (XmlNode route in routes)
                    {
                        RouteClass r = new RouteClass();
                        if (route.Attributes["Name"] != null && route.Attributes["Format"] != null)
                        {
                            r.Name = route.Attributes["Name"].Value;
                            r.Format = route.Attributes["Format"].Value;

                            if (route.Attributes["NameSpaces"] != null)
                            {
                                r.NameSpaces.AddRange(route.Attributes["NameSpaces"].Value.Split(
                                    new char[] { ';', ',', '|' }, StringSplitOptions.RemoveEmptyEntries));
                            }
                            var parms = route.SelectSingleNode("DefaultParams");
                            if (parms != null)
                            {
                                foreach (XmlAttribute att in parms.Attributes)
                                {
                                    if (!r.DefaultParams.ContainsKey(att.Name))
                                    {
                                        if (att.Value.Contains("UrlParameter.Optional"))
                                        {
                                            r.DefaultParams.Add(att.Name, UrlParameter.Optional);
                                        }
                                        else
                                        {
                                            r.DefaultParams.Add(att.Name, att.Value);
                                        }
                                    }
                                }
                            }
                        }
                        if (!Routes.ContainsKey(r.Name))
                        {
                            Routes.Add(r.Name, r);
                        }
                    }
                }


                #endregion

                #region Formats
                var formats = xdoc.SelectSingleNode("Formats");
                if (formats != null)
                {
                    var SetNodeFunc = new Func<string, List<string>, List<string>>((xp, list) =>
                    {
                        var FormatNodes = formats.SelectNodes(xp);
                        if (FormatNodes != null)
                        {
                            foreach (XmlNode node in FormatNodes)
                            {
                                list.Add(node.InnerText);
                            }
                        }
                        return list;
                    });

                    SetNodeFunc("ContentFormats/Format", Formats.ContentFormats);

                    SetNodeFunc("MasterLocationFormats/Format", Formats.MasterLocationFormats);
                    SetNodeFunc("ViewLocationFormats/Format", Formats.ViewLocationFormats);
                    SetNodeFunc("PartialViewLocationFormats/Format", Formats.PartialViewLocationFormats);

                    SetNodeFunc("AreaMasterLocationFormats/Format", Formats.AreaMasterLocationFormats);
                    SetNodeFunc("AreaViewLocationFormats/Format", Formats.AreaViewLocationFormats);
                    SetNodeFunc("AreaPartialViewLocationFormats/Format", Formats.AreaPartialViewLocationFormats);
                }
                #endregion

                HadConfig = true;
            }
        }

        #endregion

        #region 路由与模板

        public Dictionary<string, RouteClass> Routes = new Dictionary<string, RouteClass>();

        public class RouteClass
        {
            public string Name;
            public string Format;
            public IDictionary<string, object> DefaultParams = new Dictionary<string, object>();
            public List<string> NameSpaces = new List<string>();
        }


        public FormatsClass Formats = new FormatsClass();

        public class FormatsClass : Dictionary<string, List<string>>
        {
            public List<string> ContentFormats = new List<string>();
            public List<string> MasterLocationFormats = new List<string>();
            public List<string> ViewLocationFormats = new List<string>();
            public List<string> PartialViewLocationFormats = new List<string>();
            public List<string> AreaMasterLocationFormats = new List<string>();
            public List<string> AreaViewLocationFormats = new List<string>();
            public List<string> AreaPartialViewLocationFormats = new List<string>();
        }
        #endregion
    }
}
