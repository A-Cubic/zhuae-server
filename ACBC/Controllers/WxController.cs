﻿using ACBC.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ACBC.Controllers
{
    [Produces("application/json")]
    [Route(Global.ROUTE_PX + "/[controller]/[action]")]
    [EnableCors("AllowSameDomain")]
    public class WxController : Controller
    {
        [HttpPost]
        public ActionResult Open([FromBody]OpenApi openApi)
        {
            if (openApi == null)
                return Json(new ResultsJson(new Message(CodeMessage.PostNull, "PostNull"), null));
            return Json(Global.BUSS.BussResults(this, openApi));
        }

        [HttpPost]
        public ActionResult Mall([FromBody]MallApi mallApi)
        {
            if (mallApi == null)
                return Json(new ResultsJson(new Message(CodeMessage.PostNull, "PostNull"), null));
            return Json(Global.BUSS.BussResults(this, mallApi));
        }

        [HttpPost]
        public ActionResult Order([FromBody]OrderApi orderApi)
        {
            if (orderApi == null)
                return Json(new ResultsJson(new Message(CodeMessage.PostNull, "PostNull"), null));
            return Json(Global.BUSS.BussResults(this, orderApi));
        }

        [HttpPost]
        public ActionResult Member([FromBody]MemberApi memberApi)
        {
            if (memberApi == null)
                return Json(new ResultsJson(new Message(CodeMessage.PostNull, "PostNull"), null));
            return Json(Global.BUSS.BussResults(this, memberApi));
        }

        [HttpPost]
        public ActionResult Remote([FromBody]RemoteApi remoteApi)
        {
            if (remoteApi == null)
                return Json(new ResultsJson(new Message(CodeMessage.PostNull, "PostNull"), null));
            return Json(Global.BUSS.BussResults(this, remoteApi));
        }

        [HttpPost]
        public ActionResult Store([FromBody]StoreApi storeApi)
        {
            if (storeApi == null)
                return Json(new ResultsJson(new Message(CodeMessage.PostNull, "PostNull"), null));
            return Json(Global.BUSS.BussResults(this, storeApi));
        }
        [HttpPost]
        public ActionResult Active([FromBody]ActiveApi activeApi)
        {
            if (activeApi == null)
                return Json(new ResultsJson(new Message(CodeMessage.PostNull, "PostNull"), null));
            return Json(Global.BUSS.BussResults(this, activeApi));
        }
        [HttpPost]
        public ActionResult HomePage([FromBody]HomePageApi homePageApi)
        {
            if (homePageApi == null)
                return Json(new ResultsJson(new Message(CodeMessage.PostNull, "PostNull"), null));
            return Json(Global.BUSS.BussResults(this, homePageApi));
        }
        [HttpPost]
        public ActionResult Payment([FromBody]PaymentApi paymentApi)
        {
            if (paymentApi == null)
                return Json(new ResultsJson(new Message(CodeMessage.PostNull, "PostNull"), null));
            return Json(Global.BUSS.BussResults(this, paymentApi));
        }
    }
}