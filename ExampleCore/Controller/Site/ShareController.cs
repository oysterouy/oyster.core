using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Models;

namespace ExampleCore.Controller.Site
{
    [OyTestFixture]
    public partial class SiteController : Oyster.Web.Mvc.BController
    {
        public ActionResult _MPage(int _mpindex = 1, int _mpsize = 20)
        {
            ViewData["_MP"] = new MPager { PageIndex = _mpindex, PageSize = _mpsize };
            return View();
        }
    }
}
