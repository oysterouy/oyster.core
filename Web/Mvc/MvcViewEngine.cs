using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.IO;

namespace Oyster.Web.Mvc
{
    /// <summary>
    /// 自定义VIEWS目录结构
    /// </summary>
    public class MvcViewEngine : VirtualPathProviderViewEngine
    {
        public MvcViewEngine()
        {
            MasterLocationFormats = MvcViewPathHelper.Instance.MasterLocationFormats;
            ViewLocationFormats = MvcViewPathHelper.Instance.ViewLocationFormats;
            PartialViewLocationFormats = MvcViewPathHelper.Instance.PartialViewLocationFormats;

            AreaMasterLocationFormats = MvcViewPathHelper.Instance.AreaMasterLocationFormats;
            AreaViewLocationFormats = MvcViewPathHelper.Instance.AreaViewLocationFormats;
            AreaPartialViewLocationFormats = MvcViewPathHelper.Instance.AreaPartialViewLocationFormats;
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            BRequestContext concontext = controllerContext.RequestContext as BRequestContext;
            concontext.Controller = controllerContext.Controller;

            string[] vs = viewName.Split(new char[] { ';' });
            string[] ms = masterName.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            string view = viewName;
            string themeV = concontext.ThemeName;

            string master = masterName;

            if (vs.Length > 1)
            {
                view = vs[0];
                themeV = vs[1];
                concontext.CurrentTheme = MvcConfig.Instance.Themes[themeV];
            }
            if (ms.Length > 1)
            {
                master = ms[0];
            }
            string viewpath = MvcViewPathHelper.Instance.FindViewPath(concontext, view);
            string masterpath = MvcViewPathHelper.Instance.FindViewMasterPath(concontext, master);
            if (string.IsNullOrEmpty(viewpath))
            {
                throw new Exception(string.Format("{0} file is no found!", viewName));
            }
            return base.FindView(controllerContext, viewpath, masterpath, useCache);
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            BRequestContext concontext = controllerContext.RequestContext as BRequestContext;
            concontext.Controller = controllerContext.Controller;
            string partviewpath = MvcViewPathHelper.Instance.FindPartialViewPath(concontext, partialViewName);
            if (string.IsNullOrEmpty(partviewpath))
            {
                throw new Exception(string.Format("{0} file is no found!", partialViewName));
            }
            return base.FindPartialView(controllerContext, partviewpath, useCache);
        }
        protected List<string> _razorExtensions;
        public List<string> RazorExtensions
        {
            get
            {
                if (_razorExtensions == null)
                {
                    _razorExtensions = new List<string>();
                    _razorExtensions.AddRange(new string[] { "cshtml", "vbhtml" });
                }
                return _razorExtensions;

            }
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            string ext = Path.GetExtension(partialPath).ToLower();
            ext = ext.Length > 0 ? ext.Substring(1) : ext;
            if (RazorExtensions.Contains(ext))
            {
                return new RazorView(controllerContext, partialPath, null, false, RazorExtensions);
            }
            return new WebFormView(controllerContext, partialPath);
        }
        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            string ext = Path.GetExtension(viewPath).ToLower();
            ext = ext.Length > 0 ? ext.Substring(1) : ext;
            if (RazorExtensions.Contains(ext))
            {
                return new RazorView(controllerContext, viewPath, masterPath, !controllerContext.IsChildAction, RazorExtensions);
            }
            return new WebFormView(controllerContext, viewPath, masterPath);
        }
    }
}
