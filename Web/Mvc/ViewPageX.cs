using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Oyster.Web.Mvc
{
    public class ViewPageX : ViewPage
    {
        public override string MasterPageFile
        {
            get
            {
                return base.MasterPageFile;
            }
            set
            {
                base.MasterPageFile = Html.Master(value);
            }
        }
    }
}
