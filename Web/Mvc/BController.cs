using System.Web.Mvc;
using System.Diagnostics;
using System.Reflection;
using Oyster.Core.Orm;
using System;


namespace Oyster.Web.Mvc
{
    /// <summary>
    /// Controller 基础类
    /// </summary>
    public class BController : Controller
    {
        #region View控制
        protected ExcelResult Excel(string viewName = null, string masterName = null, object model = null)
        {
            if (model != null)
            {
                base.ViewData.Model = model;
            }
            return new ExcelResult { ViewName = viewName, MasterName = masterName, ViewData = base.ViewData, TempData = base.TempData };
        }

        protected override ViewResult View(string viewName, string masterName, object model)
        {
            if (OyNUniting.IsUnitRuning)
            {
                return null;
            }
            string view = viewName;
            string master = masterName;
            string defview = GetDefaultView();

            if (!string.IsNullOrEmpty(Request.Params["theme"]))
            {
                if (string.IsNullOrEmpty(view))
                {
                    view += defview + ";" + Request.Params["theme"];
                }
                else
                {
                    view += ";" + Request.Params["theme"];
                }
            }

            if (string.IsNullOrEmpty(view))
            {
                view = defview;
            }
            if (!ViewData.ContainsKey("ContentHost"))
            {
                ViewData["ContentHost"] = ContentHost;
            }
            if (!ViewData.ContainsKey("StaticHost"))
            {
                ViewData["StaticHost"] = StaticHost;
            }
            return base.View(view, master, model);
        }

        protected virtual string GetDefaultView()
        {
            string viewname = "";
            StackTrace s = new StackTrace(true);
            for (int i = 1; i < 10; i++)
            {
                var mthd = s.GetFrame(i).GetMethod() as MethodInfo;
                if (mthd.ReturnType.Equals(typeof(ActionResult)))
                {
                    viewname = mthd.Name;
                    break;
                }
            }
            return viewname;
        }
        #endregion

        #region Action控制

        protected override void Execute(System.Web.Routing.RequestContext requestContext)
        {
            try
            {
                if (!(requestContext is BRequestContext))
                {
                    requestContext = BRequestContext.New(requestContext);
                }
                base.Execute(requestContext);
            }
            catch (Exception ex)
            {
                string action = requestContext.RouteData.Values.ContainsKey("action") ? requestContext.RouteData.Values["action"].ToString() : "default";
                string controller = GetType().FullName;

                Oyster.Core.Logger.Logger.Error(string.Format("Action Run Error:{0}-{1}", controller, action), ex);
                throw ex;
            }
        }
        #endregion

        #region 扩展

        protected ViewDataDictionary _topViewData;
        protected ViewDataDictionary TopViewData
        {
            get
            {
                if (_topViewData == null)
                {
                    ViewDataDictionary data = ViewData;
                    ViewContext context = ControllerContext.ParentActionViewContext;
                    while (context != null)
                    {
                        if (context.IsChildAction)
                        {
                            context = context.ParentActionViewContext;
                        }
                        else
                        {
                            data = context.ViewData;
                            break;
                        }
                    }
                    _topViewData = data;
                }
                return _topViewData;
            }
        }
        /// <summary>
        /// Content文件存放路径
        /// </summary>
        public string ContentHost
        {
            get
            {
                string ht = "";
                if (MvcConfig.Instance.Settings.Hosts.ContentHost.Count > 0)
                {
                    var host = MvcConfig.Instance.Settings.Hosts.ContentHost[0];
                    if (!string.IsNullOrEmpty(host.Url))
                    {
                        ht = host.Url;
                    }
                    else
                    {
                        ht = string.Format("http://{0}:{1}/", host.Name, host.Port);
                    }
                }
                return ht;
            }
        }

        /// <summary>
        /// 静态文件存放路径
        /// </summary>
        public string StaticHost
        {
            get
            {
                string ht = "";
                if (MvcConfig.Instance.Settings.Hosts.ImageHost.Count > 0)
                {
                    var host = MvcConfig.Instance.Settings.Hosts.ImageHost[0];
                    if (!string.IsNullOrEmpty(host.Url))
                    {
                        ht = host.Url;
                    }
                    else
                    {
                        ht = string.Format("http://{0}:{1}/", host.Name, host.Port);
                    }
                }
                return ht;
            }
        }
        #endregion

    }
}
