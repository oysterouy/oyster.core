using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ExampleCore.Controller.Site
{
    [OyTestFixture]
    public partial class SiteController : Oyster.Web.Mvc.BController
    {
        #region Index
        public ActionResult Index(string name = null)
        {
            Proxy.UsersProxy p = new Proxy.UsersProxy();
            OyAssert.AreEqual(p.GetUserName(), name);
            return View();
        }
        #region Test
        [OyTest]
        public void Index_Default_Test()
        {
            using (new OyNUniting())
            {
                Index("AAA");
            }
        }
        #endregion
        #endregion
    }
}
