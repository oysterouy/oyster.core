using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace Oyster.Web.Mvc
{
    public class BRoute : Route
    {
        public BRoute(string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        {

        }

        public override RouteData GetRouteData(System.Web.HttpContextBase httpContext)
        {
            var data = base.GetRouteData(httpContext);
            if (data != null && data.Values != null && data.Values.ContainsKey("action") && data.Values.ContainsKey("controller"))
            {
                string action = data.Values["action"].ToString().ToLower();
                string control = data.Values["controller"].ToString().ToLower() + "controller";
                //对于系统中不存在的Action排除掉给之后的路由重新解析
                if (!MvcApplication.Action.ContainsKey(action))
                {
                    return null;
                }
                //对于系统中不存在的Controller排除掉给之后的路由重新解析
                bool valid = false;
                foreach (string k in MvcApplication.Controllers.Keys)
                {
                    string ck = k.ToLower();
                    if (ck.EndsWith(control))
                    {
                        valid = true;
                        break;
                    }
                }
                if (!valid) { return null; }
            }
            return data;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return base.GetVirtualPath(requestContext, values);
        }

        protected override bool ProcessConstraint(System.Web.HttpContextBase httpContext, object constraint, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return base.ProcessConstraint(httpContext, constraint, parameterName, values, routeDirection);
        }
    }
}
