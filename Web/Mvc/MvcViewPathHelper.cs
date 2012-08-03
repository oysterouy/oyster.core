using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Oyster.Core.Common;

namespace Oyster.Web.Mvc
{
    public class MvcViewPathHelper : InstanceHelper<MvcViewPathHelper>
    {
        public MvcViewPathHelper()
        {
            MasterLocationFormats = MvcConfig.Instance.Formats.MasterLocationFormats.ToArray();
            ViewLocationFormats = MvcConfig.Instance.Formats.ViewLocationFormats.ToArray();
            if (MvcConfig.Instance.Formats.PartialViewLocationFormats.Count > 0)
            {
                var list = new List<string>();
                list.AddRange(MvcConfig.Instance.Formats.PartialViewLocationFormats.ToArray());
                list.AddRange(ViewLocationFormats);
                PartialViewLocationFormats = list.ToArray();
            }
            else
            {
                PartialViewLocationFormats = ViewLocationFormats;
            }

            AreaMasterLocationFormats = MvcConfig.Instance.Formats.AreaMasterLocationFormats.ToArray();
            AreaViewLocationFormats = MvcConfig.Instance.Formats.AreaViewLocationFormats.ToArray();
            if (MvcConfig.Instance.Formats.AreaPartialViewLocationFormats.Count > 0)
            {
                var list = new List<string>();
                list.AddRange(MvcConfig.Instance.Formats.AreaPartialViewLocationFormats.ToArray());
                list.AddRange(AreaViewLocationFormats);
                AreaPartialViewLocationFormats = list.ToArray();
            }
            else
            {
                AreaPartialViewLocationFormats = AreaViewLocationFormats;
            }
        }

        #region 属性

        public string[] MasterLocationFormats { get; set; }

        public string[] ViewLocationFormats { get; set; }

        public string[] PartialViewLocationFormats { get; set; }

        public string[] AreaMasterLocationFormats { get; set; }

        public string[] AreaViewLocationFormats { get; set; }

        public string[] AreaPartialViewLocationFormats { get; set; }

        #endregion

        #region 方法
        public string FindViewPath(BRequestContext context, string viewName)
        {
            string viewpath = viewName;
            string view = viewName;
            string controlname = context.Controller.GetType().Name.Replace("Controller", "");

            foreach (string f in ViewLocationFormats)
            {
                viewpath = string.Format(f, new string[] { view, context.ThemeName, controlname });
                if (File.Exists(context.HttpContext.Server.MapPath(viewpath)))
                {
                    break;
                }
                viewpath = null;
            }
            if (string.IsNullOrEmpty(viewpath))
            {
                foreach (string f in ViewLocationFormats)
                {
                    viewpath = string.Format(f, new string[] { view, context.DefaultThemeName, controlname });
                    if (File.Exists(context.HttpContext.Server.MapPath(viewpath)))
                    {
                        break;
                    }
                    viewpath = null;
                }
            }
            return viewpath;
        }

        public string FindViewMasterPath(BRequestContext context, string masterName)
        {
            string master = Path.GetFileName(masterName);
            string masterpath = masterName;
            string controlname = context.Controller.GetType().Name.Replace("Controller", "");

            if (!string.IsNullOrEmpty(master))
            {
                foreach (string f in MasterLocationFormats)
                {
                    masterpath = string.Format(f, new string[] { master, context.ThemeName, controlname });
                    if (File.Exists(context.HttpContext.Server.MapPath(masterpath)))
                    {
                        break;
                    }
                    masterpath = null;
                }
                if (string.IsNullOrEmpty(masterpath))
                {
                    foreach (string f in MasterLocationFormats)
                    {
                        masterpath = string.Format(f, new string[] { master, context.DefaultThemeName, controlname });
                        if (File.Exists(context.HttpContext.Server.MapPath(masterpath)))
                        {
                            break;
                        }
                        masterpath = null;
                    }
                }
            }
            return masterpath;
        }

        public string FindPartialViewPath(BRequestContext context, string viewName)
        {
            string viewpath = viewName;
            string view = viewName;
            string controlname = context.Controller.GetType().Name.Replace("Controller", "");

            foreach (string f in PartialViewLocationFormats)
            {
                viewpath = string.Format(f, new string[] { view, context.ThemeName, controlname });
                if (File.Exists(context.HttpContext.Server.MapPath(viewpath)))
                {
                    break;
                }
                viewpath = null;
            }
            if (string.IsNullOrEmpty(viewpath))
            {
                foreach (string f in PartialViewLocationFormats)
                {
                    viewpath = string.Format(f, new string[] { view, context.DefaultThemeName, controlname });
                    if (File.Exists(context.HttpContext.Server.MapPath(viewpath)))
                    {
                        break;
                    }
                    viewpath = null;
                }
            }
            return viewpath;
        }


        #endregion
    }
}
