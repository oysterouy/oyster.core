using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web;
using System.IO;

namespace Oyster.Web.Mvc
{
    public class ContentHandler : IHttpHandler
    {
        public ContentHandler(BRequestContext context)
        {
            ProcessRequest(context);
        }

        public void ProcessRequest(BRequestContext context)
        {
            string mp = "";
            string p = context.HttpContext.Request.Path;
            p = p.StartsWith("/") ? p.Substring(1) : p;
            context.GetContentPath(p, ref mp);
            if (!string.IsNullOrEmpty(mp))
            {
                string ext = Path.GetExtension(mp);
                ext = string.IsNullOrEmpty(ext) ? "" : ext.ToLower();
                switch (ext)
                {
                    case ".js":
                        context.HttpContext.Response.ContentType = "application/x-javascript";
                        break;
                    case ".css":
                        context.HttpContext.Response.ContentType = "text/css";
                        break;
                    case ".jpg":
                        context.HttpContext.Response.ContentType = "image/jpeg";
                        break;
                    case ".gif":
                        context.HttpContext.Response.ContentType = "image/gif";
                        break;
                    case ".png":
                        context.HttpContext.Response.ContentType = "image/png";
                        break;
                    case ".bmp":
                        context.HttpContext.Response.ContentType = "image/bamp";
                        break;
                }
                
                context.HttpContext.Response.Expires = 43200;
                context.HttpContext.Response.WriteFile(mp);
            }
            else
            {
                context.HttpContext.Response.Clear();
                context.HttpContext.Response.StatusCode = 404;
                context.HttpContext.Response.Write("Good Luck!");
                context.HttpContext.Response.End();
            }
        }

        public void ProcessRequest(HttpContext context)
        {
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}
