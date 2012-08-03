using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.Mvc;
using Oyster.Core.Common;
using System.Web.Routing;
using System.Text.RegularExpressions;

namespace Oyster.Web.Mvc
{
    public class ControllerFactory : DefaultControllerFactory
    {
        public override IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            IController ctroler = null;
            try
            {
                #region  Old

                //var parms = requestContext.RouteData;

                //if (!string.IsNullOrEmpty(parms.GetRequiredString("action")))
                //{
                //    string action = parms.GetRequiredString("action").ToLower();
                //    if (MvcApplication.Action.ContainsKey(action))
                //    {
                //        var ls = MvcApplication.Action[action];
                //        if (ls != null && ls.Count > 0)
                //        {
                //            //Controller 存在请求的Action 时返回Action 对应的Controller组的第一个
                //            var ct = from m in ls where m.ToLower().Contains(controllerName.ToLower()) select m;
                //            foreach (var c in ct)
                //            {
                //                if (MvcApplication.Controllers.ContainsKey(c))
                //                {
                //                    Type controller = MvcApplication.Controllers[c];
                //                    ctroler = Activator.CreateInstance(controller) as IController;
                //                    break;
                //                }
                //            }
                //            if (MvcApplication.Controllers.ContainsKey(ls[0]))
                //            {
                //                ctroler = Activator.CreateInstance(MvcApplication.Controllers[ls[0]]) as IController;
                //            }
                //        }
                //    }
                //}

                #endregion
                Type cttype = GetControllerType(requestContext, controllerName);
                if (cttype != null)
                {
                    ctroler = Activator.CreateInstance(cttype) as IController;
                }
                if (ctroler == null)
                {
                    ctroler = base.CreateController(requestContext, controllerName);
                }
                (requestContext as BRequestContext).Controller = ctroler;
            }
            catch (Exception ex)
            {
               //TODO LOG
            }
            return ctroler;
        }

        protected override Type GetControllerType(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            var parms = requestContext.RouteData;
            if (!string.IsNullOrEmpty(parms.GetRequiredString("action")))
            {
                string action = parms.GetRequiredString("action").ToLower();
                Type cttype = GetControllerType(action, controllerName);
                if (cttype != null)
                {
                    return cttype;
                }
            }
            return base.GetControllerType(requestContext, controllerName);
        }

        public Type GetControllerType(string action, string controller = null)
        {
            string actionName = action != null ? action.ToLower().Trim() : "", controllerName = controller != null ? controller.ToLower().Trim() : "";
            if (MvcApplication.Action.ContainsKey(action))
            {
                var ls = MvcApplication.Action[action];
                if (ls != null && ls.Count > 0)
                {
                    //Controller 存在请求的Action 时返回Action 对应的Controller组的第一个
                    if (!string.IsNullOrEmpty(controllerName))
                    {
                        var ct = from m in ls where m.ToLower().Contains(controllerName) select m;
                        foreach (var c in ct)
                        {
                            if (MvcApplication.Controllers.ContainsKey(c))
                            {
                                Type cttype = MvcApplication.Controllers[c];
                                return cttype;
                            }
                        }
                    }
                    return MvcApplication.Controllers[ls[0]];
                }
            }
            return null;
        }

        public string GetActionLink(string action, string controller, string[] plist = null)
        {
            Regex regact = new Regex("{action}", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Regex regctl = new Regex("{controller}", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Regex regpmts = new Regex("{[^}]+}", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            string href = "#";
            string actionName = action != null ? action.Trim().ToLower() : "";
            System.Web.Routing.RequestContext rtcontext = new System.Web.Routing.RequestContext();
            rtcontext.RouteData.Values.Add("action", actionName);
            var cttp = GetControllerType(rtcontext, controller);
            if (cttp != null)
            {
                for (int i = 0; i < RouteTable.Routes.Count; i++)
                {
                    Route route = RouteTable.Routes[i] as Route;
                    if (route != null)
                    {
                        string hf = regctl.Replace(route.Url, cttp.Name.ToLower().Replace("controller", ""));
                        hf = regact.Replace(hf, actionName);
                        int t = 0;
                        hf = regpmts.Replace(hf, new MatchEvaluator((m) =>
                        {
                            if (plist != null && plist.Length > t)
                            {
                                return plist[t++];
                            }
                            return "";
                        }));

                    }
                }
            }
            return href;
        }
    }
}
