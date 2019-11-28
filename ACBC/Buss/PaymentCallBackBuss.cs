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
    public class PaymentCallBackBuss  
    {
        private TenPayV3Info tenPayV3Info;
        private PaymentDao pDao = new PaymentDao();

        public PaymentCallBackBuss()
        {
            tenPayV3Info = new TenPayV3Info(
                Global.APPID,
                Global.APPSECRET,
                Global.MCHID,
                Global.PaymentKey,
                "",
                "",
                Global.CallBackUrl,"");
        }

        public string GetPaymentResult(ResponseHandler resHandler)
        {
            OpenDao openDao = new OpenDao();
            string return_code = "SUCCESS";
            string return_msg1 = "OK";
            try
            {
                return_code = resHandler.GetParameter("return_code");
                string return_msg = resHandler.GetParameter("return_msg");
                string openid = resHandler.GetParameter("openid");
                string total_fee = resHandler.GetParameter("total_fee");
                string time_end = resHandler.GetParameter("time_end");
                string out_trade_no = resHandler.GetParameter("out_trade_no");
                string transaction_id = resHandler.GetParameter("transaction_id");
                //openDao.writeLog(Global.POSCODE, openid, "payCallBack", return_msg + "#" + out_trade_no + "#" + transaction_id + "#" + total_fee + "#" + time_end);
                openDao.add_log("payBack", return_msg + "#" + out_trade_no + "#" + transaction_id + "#" + total_fee + "#" + time_end, openid);
                resHandler.SetKey(tenPayV3Info.Key);
                //��֤�����Ƿ��΢�ŷ���������ȫ��
                if (resHandler.IsTenpaySign() && return_code.ToUpper() == "SUCCESS")
                {
                    /* ������Խ��ж���������߼� */
                    // transaction_id:΢��֧������
                    // out_trade_no:�̳�ʵ�ʶ�����
                    // openId:�û���Ϣ
                    // total_fee:ʵ��֧���۸�

                    if (checkOrderTotalPrice(out_trade_no, Convert.ToDouble(total_fee)))
                    {
                        if (pDao.updateOrderForPay(out_trade_no, transaction_id))
                        {
                            
                        }
                        else
                        {
                            pDao.insertPayLog(out_trade_no, transaction_id, total_fee, openid, "֧�����-�޸Ķ���״̬ʧ��");
                        }
                    }
                    else
                    {
                        pDao.insertPayLog(out_trade_no, transaction_id, total_fee, openid, "֧�����-����״̬��Ϊ1��֧������붩���ܽ���");
                    }
                }
                else
                {
                    return_code = "FAIL";
                    return_msg1 = "���Ǵ�΢�ŷ�����";

                    //openDao.writeLog(Global.POSCODE, openid, "payCallBack", return_msg + "#" + out_trade_no + "#" + transaction_id + "#" + total_fee + "#" + time_end);
                }
                return string.Format(@"<xml><return_code><![CDATA[{0}]]></return_code><return_msg><![CDATA[{1}]]></return_msg></xml>", return_code, return_msg1);

            }
            catch (Exception ex)
            {
                return_code = "FAIL";
                return_msg1 = ex.ToString();
                //openDao.writeLog(Global.POSCODE, "", "payCallBack", return_msg);
                return string.Format(@"<xml><return_code><![CDATA[{0}]]></return_code><return_msg><![CDATA[{1}]]></return_msg></xml>", return_code, return_msg1);
            }
        }
        /// <summary>
        /// �˶Զ����ܽ���֧�����
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="totalPrice"></param>
        /// <returns></returns>
        public bool checkOrderTotalPrice(string billId, double totalPrice)
        {
            //#if DEBUG
            //            return true;
            //#endif

            PaymentDao paymentDao = new PaymentDao();
            Bill billList = paymentDao.getBillByBillId(billId);
            if (billList != null)
            {
                if (billList.billState == "1")
                {
                    if (billList.billPrice * 100 == totalPrice)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }

    }
}
