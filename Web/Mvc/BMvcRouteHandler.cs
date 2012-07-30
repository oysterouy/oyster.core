using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Web;
using Oyster.Core.Tool;

namespace Oyster.Web.Mvc
{
    public class BMvcRouteHandler : MvcRouteHandler
    {
        protected override System.Web.IHttpHandler GetHttpHandler(System.Web.Routing.RequestContext requestContext)
        {
            var Data = requestContext.RouteData.Values;

            if (Data["handler"] != null && !string.IsNullOrEmpty(Data["handler"].ToString()))
            {
                Type handle = Type.GetType(Data["handler"].ToString());
                if (handle != null)
                {
                    var httphandle = Activator.CreateInstance(handle, new object[] { BRequestContext.New(requestContext) }) as IHttpHandler;

                    return httphandle;
                }
                var httphandler = new ContentHandler(BRequestContext.New(requestContext));
                return httphandler;
            }

            return base.GetHttpHandler(BRequestContext.New(requestContext));
        }
    }
}
