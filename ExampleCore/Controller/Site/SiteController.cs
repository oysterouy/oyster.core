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
        #region Index
        public ActionResult Index(CxActivitySendOy cxact = null, decimal ysmin = 0, decimal ysmax = 0)
        {
            return View();
            var cond = new OyCondition(CxActivitySendOy.sTatus, ConditionOperator.NotEqual, -1);
            if (cxact != null)
            {
                if (!string.IsNullOrEmpty(cxact.ActivityType) && cxact.ActivityType != "-1")
                {
                    cond &= new OyCondition(CxActivitySendOy.aCtivityType, cxact.ActivityType);
                }
                if (!string.IsNullOrEmpty(cxact.ActivityName))
                {
                    cond &= new OyCondition(CxActivitySendOy.aCtivityName, ConditionOperator.Like, cxact.ActivityName);
                }
                if (!string.IsNullOrEmpty(cxact.CxDiscount))
                {
                    cond &= new OyCondition(CxActivitySendOy.cXDiscount, ConditionOperator.Like, cxact.CxDiscount);
                }
                if (ysmin > 0 && ysmax > 0)
                {
                    cond &= (new OyCondition(CxActivitySendOy.aCtivityBudget, ConditionOperator.GreaterThanOrEqual, ysmin)
                        & new OyCondition(CxActivitySendOy.aCtivityBudget, ConditionOperator.Less, ysmax));
                }
                else
                {
                    if (ysmax > 0)
                    {
                        cond &= new OyCondition(CxActivitySendOy.aCtivityBudget, ConditionOperator.Less, ysmax);
                    } if (ysmin > 0)
                    {
                        cond &= new OyCondition(CxActivitySendOy.aCtivityBudget, ConditionOperator.GreaterThanOrEqual, ysmin);
                    }
                }
            }
            var _mp = ViewData["_MP"] as MPager;
            _mp = _mp == null ? new MPager { PageSize = 20, PageIndex = 1 } : _mp;

            var ls = OyEngine<CxActivitySendOy>.Filter(cond, _mp);


            return View();
        }
        #region Test
        [OyTest]
        public void Index_Default_Test()
        {
            using (new OyNUniting())
            {
                Index();
            }
        }
        #endregion
        #endregion

        public ActionResult Views(long id = -1)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Views(CxActivitySendOy cxact)
        {
            if (cxact.Id > 0)
            {
                OyEngine<CxActivitySendOy>.Update(new OyValue(CxActivitySendOy.sTatus, cxact.Status)
                , new OyCondition(CxActivitySendOy.iD, cxact.Id));
            }
            else
            {
                try
                {
                    OyEngine.DbTran.Begin();
                    string opguid = OyEngine<CxActivitySendOy>.Insert(cxact);
                    var ls = OyEngine<CxActivitySendOy>.GetByOpGuid(opguid);
                    if (ls != null && ls.Count > 0)
                    {
                        cxact = ls[0];
                        for (int i = 0; i < 5; i++)
                        {
                            CxDiscountBatchOy cxd = new CxDiscountBatchOy();
                            cxd.ActivitySendId = cxact.Id;
                        }
                    }
                    else
                    {
                        throw new Exception("插入失败：" + cxact.ToJson());
                    }
                    OyEngine.DbTran.Commit();
                }
                finally
                {
                    OyEngine.DbTran.Rollback();
                }
            }
            return View();
        }
    }
}
