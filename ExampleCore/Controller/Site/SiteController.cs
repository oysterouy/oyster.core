using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ExampleCore.Controller.Site
{
    public class SiteController : Oyster.Web.Mvc.BController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
