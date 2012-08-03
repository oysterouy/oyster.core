using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Routing;
using System.Reflection;
using System.Dynamic;
using Oyster.Core.Common;


namespace Oyster.Web.Mvc
{
    public class BRequestContext : RequestContext
    {
        /// <summary>
        /// 扩展类
        /// 当存在扩展类，在系统传递RequestContext的时候将会使用扩展类。
        /// 但是必须在Application_Start()的最前设置，你可以让Global 的后台代码类 继承Oyster.Web.Mvc.MvcApplication
        /// 代码如下：
        /// protected override void Application_Start()
        ///{
        ///    Oyster.Web.Mvc.BRequestContext.ExpandHandle = new Func<RequestContext, Oyster.Web.Mvc.BRequestContext>((q) =>
        ///    {
        ///        return new T(q);
        ///    });
        ///    base.Application_Start();
        ///}
        /// </summary>
        public static Func<RequestContext, BRequestContext> ExpandHandle;

        public static BRequestContext New(RequestContext context)
        {
            BRequestContext t = null;
            if (ExpandHandle != null)
            {
                t = ExpandHandle(context);
            }
            else
            {
                t = new BRequestContext(context);
            }


            ContextHelper.Instance.SetContext<BRequestContext>(t);

            return t;
        }

        protected BRequestContext(RequestContext context)
            : base(context.HttpContext, context.RouteData)
        {
            Uri url = context.HttpContext.Request.Url;
            ContextHost = string.Format("{0}://{1}{2}", new string[] { url.Scheme, url.Host, (url.Port == 80 ? "" : (":" + url.Port.ToString())) });
            SiteEdition = "V" + DateTime.Now.ToString("yyyyMMdd");
        }

        protected BHttpContext _httpcontext;
        public override HttpContextBase HttpContext
        {
            get
            {
                if (_httpcontext == null)
                {
                    _httpcontext = new BHttpContext(System.Web.HttpContext.Current);
                }
                return _httpcontext;
            }
            set
            {
                base.HttpContext = value;
            }
        }

        public static BRequestContext Context
        {
            get
            {
                var v = ContextHelper.Instance.GetContext<BRequestContext>();
                return v as BRequestContext;
            }
        }

        public System.Web.Mvc.IController Controller { get; set; }

        public virtual Theme DefaultTheme
        {
            get
            {
                return MvcConfig.Instance.Themes.Default;
            }
        }
        /// <summary>
        /// 系统默认请求使用的模板
        /// </summary>
        public string DefaultThemeName
        {
            get
            {
                return (DefaultTheme != null) ? DefaultTheme.Name : "Base";
            }
        }
        protected Theme _currenttheme;
        /// <summary>
        /// 当前请求使用的模板
        /// </summary>
        public virtual Theme CurrentTheme
        {
            get
            {
                if (_currenttheme == null)
                {
                    _currenttheme = MvcConfig.Instance.Themes.Current;
                }
                return _currenttheme;
            }
            set
            {
                _currenttheme = value;
            }
        }
        public string ThemeName
        {
            get { return CurrentTheme != null ? CurrentTheme.Name : DefaultThemeName; }
        }

        /// <summary>
        /// dynamic 类型的数据载体
        /// </summary>
        public dynamic Data = new ExpandoObject();

        /// <summary>
        /// 当前请求主机
        /// </summary>
        public string ContextHost { get; set; }
        /// <summary>
        /// 网站版本号
        /// </summary>
        public string SiteEdition { get; set; }

        public string GetContentPath(string path)
        {
            string s = "";
            return GetContentPath(path, ref s);
        }

        /// <summary>
        /// 获取Content资源文件实际URL
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetContentPath(string path, ref string mappath)
        {
            string realpath = "";
            string[] realpathformat = MvcConfig.Instance.Formats.ContentFormats.ToArray();

            foreach (string f in realpathformat)
            {
                realpath = string.Format(f, new string[] { path, ThemeName });
                if (File.Exists(HttpContext.Server.MapPath(realpath)))
                {
                    break;
                }
                realpath = null;
            }
            if (string.IsNullOrEmpty(realpath))
            {
                foreach (string f in realpathformat)
                {
                    realpath = string.Format(f, new string[] { path, DefaultThemeName });
                    if (File.Exists(HttpContext.Server.MapPath(realpath)))
                    {
                        break;
                    }
                    realpath = null;
                }
            }
            string txt = "";
            if (!string.IsNullOrEmpty(realpath))
            {
                //回传实际路径
                mappath = HttpContext.Server.MapPath(realpath);
                realpath = realpath.StartsWith("~/") ? realpath.Substring(2).ToLower() : realpath.ToLower();
                string contenthost = MvcConfig.Instance.Settings.Hosts.GetHost("content");
                txt = string.Format("{0}/{1}?editon={2}", contenthost, realpath, SiteEdition);
            }
            return txt;
        }

        /// <summary>
        /// 格式化输出资源代码
        /// </summary>
        /// <param name="path"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string ContentUrlFormat(string path, string type)
        {
            string txt = "";
            string realpath = "";
            string[] realpathformat = MvcConfig.Instance.Formats.ContentFormats.ToArray();

            foreach (string f in realpathformat)
            {
                realpath = string.Format(f, new string[] { path, ThemeName });
                if (File.Exists(HttpContext.Server.MapPath(realpath)))
                {
                    break;
                }
                realpath = null;
            }
            if (string.IsNullOrEmpty(realpath))
            {
                foreach (string f in realpathformat)
                {
                    realpath = string.Format(f, new string[] { path, DefaultThemeName });
                    if (File.Exists(HttpContext.Server.MapPath(realpath)))
                    {
                        break;
                    }
                    realpath = null;
                }
            }
            string contenthost = MvcConfig.Instance.Settings.Hosts.GetHost("content");

            if (!string.IsNullOrEmpty(realpath))
            {
                realpath = realpath.StartsWith("~/") ? realpath.Substring(2).ToLower() : realpath.ToLower();
                type = string.IsNullOrEmpty(type) ? "" : (type.Trim().ToLower());
                switch (type)
                {
                    case "css":
                        txt = string.Format("\r\n<link type=\"text/css\" rel=\"Stylesheet\" href=\"{0}/{1}?editon={2}\" />", contenthost, realpath, SiteEdition);
                        break;
                    case "js":
                        txt = string.Format("\r\n<script type=\"text/javascript\" language=\"javascript\" src=\"{0}/{1}?editon={2}\"></script>"
                            , contenthost, realpath, SiteEdition);
                        break;
                    case "image":
                        txt = string.Format("{0}/{1}?editon={2}", contenthost, realpath, SiteEdition);
                        break;
                }
            }
            else
            {
                throw new Exception(string.Format("Content File: {0} Can Not Find!", path));
            }
            return txt;
        }
    }
}
