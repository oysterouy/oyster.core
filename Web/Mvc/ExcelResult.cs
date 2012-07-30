using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Globalization;
using System.IO;

namespace Oyster.Web.Mvc
{
    public class ExcelResult : ViewResultBase
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (string.IsNullOrEmpty(this.ViewName))
            {
                this.ViewName = context.RouteData.GetRequiredString("action");
            }
            ViewEngineResult result = null;
            if (this.View == null)
            {
                result = this.FindView(context);
                this.View = result.View;
            }
            string outfilename = ViewData["OutFileName"] != null ? ViewData["OutFileName"].ToString() : ViewName;
            var Response = context.HttpContext.Response;
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "UTF-8";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + ToHexString(outfilename) + ".xls");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");

            TextWriter output = Response.Output;
            ViewContext viewContext = new ViewContext(context, this.View, this.ViewData, this.TempData, output);
            this.View.Render(viewContext, output);

            Response.Flush();
            Response.End();
        }

        #region View field

        // Fields
        private string _masterName;

        // Methods
        protected override ViewEngineResult FindView(ControllerContext context)
        {
            ViewEngineResult result = base.ViewEngineCollection.FindView(context, base.ViewName, this.MasterName);
            if (result.View != null)
            {
                return result;
            }
            StringBuilder builder = new StringBuilder();
            foreach (string str in result.SearchedLocations)
            {
                builder.AppendLine();
                builder.Append(str);
            }
            throw new InvalidOperationException(string.Format("{0} is not found.<br />{1}<br/>", new object[] { base.ViewName, builder }));
        }

        // Properties
        public string MasterName
        {
            get
            {
                return (this._masterName ?? string.Empty);
            }
            set
            {
                this._masterName = value;
            }
        }


        #endregion

        /// <summary>
        /// Encodes non-US-ASCII characters in a string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string ToHexString(string s)
        {
            char[] chars = s.ToCharArray();
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < chars.Length; index++)
            {
                bool needToEncode = NeedToEncode(chars[index]);
                if (needToEncode)
                {
                    string encodedString = ToHexString(chars[index]);
                    builder.Append(encodedString);
                }
                else
                {
                    builder.Append(chars[index]);
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// Determines if the character needs to be encoded.
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private bool NeedToEncode(char chr)
        {
            string reservedChars = "$-_.+!*'(),@=&";

            if (chr > 127)
                return true;
            if (char.IsLetterOrDigit(chr) || reservedChars.IndexOf(chr) >= 0)
                return false;

            return true;
        }
        /// <summary>
        /// Encodes a non-US-ASCII character.
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private string ToHexString(char chr)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] encodedBytes = utf8.GetBytes(chr.ToString());
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < encodedBytes.Length; index++)
            {
                builder.AppendFormat("%{0}", Convert.ToString(encodedBytes[index], 16));
            }

            return builder.ToString();
        }
    }
}
