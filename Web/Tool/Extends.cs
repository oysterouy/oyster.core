using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Oyster.Web.Mvc;

namespace System
{
    public static class OyWebExtends
    {
        #region Mvc 扩展
        public static string Master(this HtmlHelper html, string layout)
        {
            BRequestContext concontext = html.ViewContext.RequestContext as BRequestContext;
            return MvcViewPathHelper.Instance.FindViewMasterPath(concontext, layout);
        }

        /// <summary>
        /// HtmlHelper 扩展输出
        /// </summary>
        /// <param name="html"></param>
        /// <param name="text">要输出到当前文件流的文本</param>
        public static void Echo(this HtmlHelper html, object text)
        {
            string txt = text == null ? string.Empty : text.ToString();
            html.ViewContext.Writer.Write(txt);
        }
        /// <summary>
        /// HtmlHelper 扩展输出Content文件
        /// </summary>
        /// <param name="html"></param>
        /// <param name="path">要输出到当前文件相对Content文件夹的路径,格式如："Javascript/aaa.js"</param>
        /// <param name="type">link,script</param>
        public static void EchoContent(this HtmlHelper html, object path, string type)
        {
            html.Echo(html.Content(path, type));
        }

        public static MvcHtmlString Content(this HtmlHelper html, object path, string type)
        {
            BRequestContext concontext = html.ViewContext.RequestContext as BRequestContext;

            return new MvcHtmlString(concontext.ContentUrlFormat(path.ToString(), type));
        }
        #endregion
    }
}
