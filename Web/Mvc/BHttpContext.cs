using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Oyster.Web.Mvc
{
    public class BHttpContext : HttpContextWrapper
    {
        public BHttpContext(HttpContext b)
            : base(b)
        {

        }
    }

    public class BHttpResponse : HttpResponseWrapper
    {
        public BHttpResponse(HttpResponse b)
            : base(b)
        {

        }

        protected StringBuilder sbder = new StringBuilder();

        public override void Write(string s)
        {
            sbder.Append(s);
        }
        public override void Write(char ch)
        {
            sbder.Append(ch);
        }
        public override void Write(object obj)
        {
            sbder.Append(obj);
        }
        public override void Write(char[] buffer, int index, int count)
        {
            sbder.Append(buffer, index, count);
        }

        public override void Flush()
        {
            base.Write(sbder.ToString());
            base.Flush();
        }
    }
}
