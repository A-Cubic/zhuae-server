using ACBC.Common;
using ACBC.Dao;
using Newtonsoft.Json;
using Senparc.Weixin.MP;
using Senparc.Weixin.TenPay.V3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACBC.Buss
{
    public class PaymentBuss : IBuss
    {
        private TenPayV3Info tenPayV3Info;
        private PaymentDao pDao = new PaymentDao();

        public PaymentBuss()
        {
            tenPayV3Info = new TenPayV3Info(
                Global.APPID,
                Global.APPSECRET,
                Global.MCHID,
                Global.PaymentKey, 
                "", 
                "",
                Global.CallBackUrl,
                "");
        }

        public ApiType GetApiType()
        {
            return ApiType.PaymentApi;
        }

        public object Do_Payment(BaseApi baseApi)
        {
            PaymentParam paymentParam = JsonConvert.DeserializeObject<PaymentParam>(baseApi.param.ToString());
            if (paymentParam == null)
            {
                throw new ApiException(CodeMessage.InvalidParam, "InvalidParam");
            }
            if (paymentParam.id == null || paymentParam.id == "")
            {
                throw new ApiException(CodeMessage.InterfaceValueError, "InterfaceValueError");
            }

            string openId = Utils.GetOpenID(baseApi.token);
            string memberId = Utils.GetMemberID(baseApi.token);
            OpenDao openDao = new OpenDao();
            PaymentDao paymentDao = new PaymentDao();
            MemberDao memberDao = new MemberDao();
            Bill bill =paymentDao.AddBill(memberId,memberDao.getResellerGoodsById(paymentParam.id));
            int totalPrice = Convert.ToInt32(bill.billPrice * 100);
            if (totalPrice <= 0)
            {
                throw new ApiException(CodeMessage.PaymentTotalPriceZero, "PaymentTotalPriceZero");
            }
            
            try
            {
                var timeStamp = TenPayV3Util.GetTimestamp();
                var nonceStr = TenPayV3Util.GetNoncestr();
                var product = "礼包";
                var xmlDataInfo =
                    new TenPayV3UnifiedorderRequestData(
                        tenPayV3Info.AppId,
                        tenPayV3Info.MchId,
                        product,
                        bill.billId,
                        totalPrice,
                        "127.0.0.1",
                        tenPayV3Info.TenPayV3Notify,
                        Senparc.Weixin.TenPay.TenPayV3Type.JSAPI,
                        openId,
                        tenPayV3Info.Key,
                        nonceStr);

                var result = TenPayV3.Html5Order(xmlDataInfo);
                if (result.ResultXml != "")
                {
                    openDao.add_log("pay", result.ResultXml, "");
                }

                pDao.writePrePayId(bill.billId, result.prepay_id);
                var package = string.Format("prepay_id={0}", result.prepay_id);
                var paySign = TenPayV3.GetJsPaySign(tenPayV3Info.AppId, timeStamp, nonceStr, package, tenPayV3Info.Key);

                PaymentResults paymentResults = new PaymentResults();
                paymentResults.appId = tenPayV3Info.AppId;
                paymentResults.nonceStr = nonceStr;
                paymentResults.package = package;
                paymentResults.paySign = paySign;
                paymentResults.timeStamp = timeStamp;
                paymentResults.product = product;
                paymentResults.billId = bill.billId;

                return paymentResults;
            }
            catch (Exception ex)
            {
                throw new ApiException(CodeMessage.PaymentError, "PaymentError");
            }
        }

        //public object Do_SendPaymentMsg(BaseApi baseApi)
        //{
        //    try
        //    {
        //        SendPaymentMsg sendPaymentMsg = JsonConvert.DeserializeObject<SendPaymentMsg>(baseApi.param.ToString());
        //        if (sendPaymentMsg == null)
        //        {
        //            throw new ApiException(CodeMessage.InvalidParam, "InvalidParam");
        //        }
        //        if (this.sendTemplateMessage(sendPaymentMsg.orderId))
        //        {
        //            return new { };
        //        }
        //        else
        //        {
        //            throw new ApiException(CodeMessage.PaymentMsgError, "PaymentMsgError");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiException(CodeMessage.PaymentMsgError, "PaymentMsgError");
        //    }


        //}
        /// <summary>
        /// 支付成功
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <returns></returns>
        //private bool sendTemplateMessage(string out_trade_no)
        //{
        //    try
        //    {
        //        PaymentDataResults paymentDataResults = pDao.getPayData(out_trade_no);
        //        WxJsonResult wxJsonResult = TemplateApi.SendTemplateMessage(Global.APPID,
        //            paymentDataResults.openId,
        //            Global.PaySuccessTemplate,
        //            new
        //            {
        //                keyword1 = new { value = paymentDataResults.billid },
        //                keyword2 = new { value = paymentDataResults.billPrice },
        //                keyword3 = new { value = paymentDataResults.billValue },
        //                keyword4 = new { value = paymentDataResults.bookingTime },
        //                keyword5 = new { value = paymentDataResults.bookingState }
        //            },
        //            paymentDataResults.prePayId, "/pages/orderList/orderList?num=1", "keyword4.DATA");
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

    }
}
