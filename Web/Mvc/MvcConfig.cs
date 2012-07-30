using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Web.Mvc;
using System.Dynamic;
using System.Linq.Expressions;
using Oyster.Core.Tool;

namespace Oyster.Web.Mvc
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

        public MvcConfig()
        {
            Init();
        }

        protected void Init()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "/OysterConfig.xml";
            string pathdir = System.AppDomain.CurrentDomain.BaseDirectory;
            if (!File.Exists(path))
            {
                path = System.AppDomain.CurrentDomain.BaseDirectory + "/bin/OysterConfig.xml";
                pathdir = System.AppDomain.CurrentDomain.BaseDirectory + "/bin";
            }
            if (File.Exists(path))
            {
                XmlDocument xdoc = new XmlDocument();
                var s = File.ReadAllText(path);
                xdoc.LoadXml(s);
                var xsett = xdoc.SelectSingleNode("Settings");

                #region Routes
                var routes = xsett.SelectNodes("Mvc/Routes/Route");
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
                var formats = xsett.SelectSingleNode("Mvc/Formats");
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

                #region Settings Hosts
                var settings = xsett;//.SelectSingleNode("Mvc/Settings");
                if (settings != null)
                {
                    Settings = new Settings(settings);
                }
                #endregion

                #region Themes
                var themes = xsett.SelectSingleNode("Mvc/Themes");
                if (themes != null)
                {
                    Themes = new Themes(themes);
                }
                #endregion
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

        #region Settings
        public Settings Settings;
        #endregion

        #region Themes
        public Themes Themes { get; set; }
        #endregion
    }

    public class Settings
    {
        public Settings(XmlNode settings)
        {
            if (settings != null && settings.SelectSingleNode("Hosts") != null)
            {
                Hosts = new Hosts(settings.SelectSingleNode("Hosts"));
            }
        }
        public Hosts Hosts;
    }

    public class Hosts
    {
        public Hosts(XmlNode hosts)
        {
            if (hosts != null)
            {
                SetHosts(ContentHost, hosts.SelectSingleNode("ContentHost"));
                SetHosts(ImageHost, hosts.SelectSingleNode("StaticHost"));
                SetHosts(BigfileHost, hosts.SelectSingleNode("WebHost"));
            }
        }

        void SetHosts(List<Host> hts, XmlNode childhosts)
        {
            if (childhosts != null)
            {
                var nodes = childhosts.SelectNodes("Host");
                if (nodes != null && nodes.Count > 0)
                {
                    for (int i = 0; i < nodes.Count; i++)
                    {
                        XmlNode nd = nodes[i];
                        Host ht = new Host();
                        if (nd.Attributes["Url"] != null)
                        {
                            ht.Url = nd.Attributes["Url"].Value;
                        }
                        if (nd.Attributes["Name"] != null)
                        {
                            ht.Name = nd.Attributes["Name"].Value;
                            if (nd.Attributes["Port"] != null && nd.Attributes["Port"].Value.IsInt32())
                            {
                                ht.Port = Convert.ToInt32(nd.Attributes["Port"].Value);
                            }
                        }
                        hts.Add(ht);
                    }
                }
            }
        }

        public List<Host> ContentHost = new List<Host>();
        public List<Host> ImageHost = new List<Host>();
        public List<Host> BigfileHost = new List<Host>();

        /// <summary>
        /// 获取对应类型的服务器地址
        /// </summary>
        /// <param name="type">content,image,bigfile</param>
        /// <returns></returns>
        public string GetHost(string type = "content")
        {
            var hc = OyContext.Instance.GetContext<HostCount>() as HostCount;
            if (hc == null)
            {
                hc = new HostCount(this);
                OyContext.Instance.SetContext<HostCount>(hc);
            }
            return hc.GetHost(type);
        }

        class HostCount
        {
            int Contentcount = 0;
            int Imagecount = 0;
            int Bigfilecount = 0;

            private HostCount() { }

            Hosts hosts;
            public HostCount(Hosts ht)
            {
                hosts = ht;
            }

            public string GetHost(string type)
            {
                Host h = null;

                switch (type)
                {
                    case "content":
                        if (hosts.ContentHost.Count > 1)
                        {
                            Contentcount = Contentcount < hosts.ContentHost.Count ? Contentcount + 1 : 0;
                            h = hosts.ContentHost[Contentcount];
                        }
                        else if (hosts.ContentHost.Count == 1)
                        {
                            h = hosts.ContentHost[0];
                        }
                        break;
                    case "image":
                        if (hosts.ContentHost.Count > 1)
                        {
                            Imagecount = Imagecount < hosts.ImageHost.Count ? Imagecount + 1 : 0;
                            h = hosts.ImageHost[Imagecount];
                        }
                        else if (hosts.ImageHost.Count == 1)
                        {
                            h = hosts.ImageHost[0];
                        }
                        break;
                    case "bigfile":
                        if (hosts.ContentHost.Count > 1)
                        {
                            Bigfilecount = Bigfilecount < hosts.BigfileHost.Count ? Bigfilecount + 1 : 0;
                            h = hosts.BigfileHost[Bigfilecount];
                        }
                        else if (hosts.BigfileHost.Count == 1)
                        {
                            h = hosts.BigfileHost[0];
                        }
                        break;
                }
                if (h != null)
                {
                    if (h.UseUrl)
                    {
                        return h.Url;
                    }
                    else
                    {
                        return string.Format("{0}:{1}{2}", h.Name, h.Port, h.Url);
                    }
                }
                return "";
            }
        }
    }
    public class Host
    {
        public string Url;

        public string Name;

        public int Port;

        public bool UseUrl = true;
    }

    public class Themes : Dictionary<string, Theme>
    {
        public Themes(XmlNode themes)
        {
            if (themes != null)
            {
                var ls = themes.SelectNodes("Theme");
                if (ls != null && ls.Count > 0)
                {
                    foreach (XmlNode nd in ls)
                    {
                        Theme t = new Theme();
                        t.Name = nd.InnerText;
                        if (nd.Attributes["Default"] != null && nd.Attributes["Default"].Value.ToLower() == "true")
                        {
                            t.Default = true;
                        }
                        else
                        {
                            t.Default = false;
                        }
                        if (nd.Attributes["Current"] != null && nd.Attributes["Current"].Value.ToLower() == "true")
                        {
                            t.Current = true;
                        }
                        else
                        {
                            t.Current = false;
                        }
                        if (nd.Attributes["Engine"] != null && nd.Attributes["Engine"].Value.ToLower() == "razor")
                        {
                            t.ViewEnginer = "Razor";
                        }
                        else
                        {
                            t.ViewEnginer = "WebForm";
                        }
                        Add(t);
                    }
                }
            }
        }
        public Theme Default { get; protected set; }
        public Theme Current { get; protected set; }
        public void Add(Theme theme)
        {
            Add(theme.Name, theme);
        }
        public new void Add(string key, Theme theme)
        {
            if (theme.Current)
            {
                Current = theme;
            }
            if (theme.Default)
            {
                Default = theme;
            }
            base.Add(key.ToLower(), theme);
        }
        public new Theme this[string key]
        {
            get
            {
                key = key.ToLower();
                if (base.ContainsKey(key))
                {
                    return base[key];
                }
                return Current;
            }
            set
            {
                string valkey = value.Name.ToLower();
                if (base.ContainsKey(valkey))
                {
                    base[valkey] = value;
                }
                else
                {
                    base.Add(valkey, value);
                }
            }
        }
    }
    public class Theme
    {
        public bool Default = false;
        public bool Current = false;
        public string Name;
        public string ViewEnginer = "Razor";
    }
}
