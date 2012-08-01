﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Diagnostics;
using System.Reflection;
using System.Dynamic;
using System.Collections;
using Oyster.Core.Tool;

namespace Oyster.Web.Mvc
{
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
            if (!(requestContext is BRequestContext))
            {
                requestContext = BRequestContext.New(requestContext);
            }
            base.Execute(requestContext);
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
