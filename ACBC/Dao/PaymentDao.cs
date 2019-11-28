using ACBC.Buss;
using Com.ACBC.Framework.Database;
using System;
using System.Collections;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace ACBC.Dao
{
    public class PaymentDao
    {
        /// <summary>
        /// 写入prepayid
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="prePayId"></param>
        /// <returns></returns>
        public bool writePrePayId(string billId, string prePayId)
        {
            StringBuilder builder1 = new StringBuilder();
            builder1.AppendFormat(PaymentSqls.UPDATE_BILLLIST_FOR_PAYID, prePayId, billId);
            string sql1 = builder1.ToString();

            return DatabaseOperationWeb.ExecuteDML(sql1);
        }
        /// <summary>
        /// 获取Bill
        /// </summary>
        /// <param name="billId"></param>
        /// <param name="Bill"></param>
        /// <returns></returns>
        public Bill getBillByBillId(string billId)
        {
            Bill bill = null;
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(PaymentSqls.SELECT_BILL_BY_BILLID, billId);
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt.Rows.Count > 0)
            {

                bill = new Bill
                {
                    id = dt.Rows[0]["id"].ToString(),
                    billId = dt.Rows[0]["bill_id"].ToString(),
                    memberId = dt.Rows[0]["member_id"].ToString(),
                    billTime = dt.Rows[0]["bill_time"].ToString(),
                    billState = dt.Rows[0]["bill_state"].ToString(),
                    billPrice =Convert.ToDouble( dt.Rows[0]["bill_price"]),
                    goodsId = dt.Rows[0]["goods_id"].ToString(),
                    barcode = dt.Rows[0]["barcode"].ToString(),
                    goodsName = dt.Rows[0]["goods_name"].ToString(),
                    goodsImg = dt.Rows[0]["goods_img"].ToString(),
                    goods_num =Convert.ToInt16( dt.Rows[0]["goods_num"]),
                    buyerName = dt.Rows[0]["buyer_name"].ToString(),
                    buyerPhone = dt.Rows[0]["buyer_phone"].ToString(),
                    buyerAddr = dt.Rows[0]["buyer_addr"].ToString(),
                    prePayId = dt.Rows[0]["prePayId"].ToString(),
                    prePayTime = dt.Rows[0]["prePayTime"].ToString(),
                    payNo = dt.Rows[0]["payNo"].ToString(),
                    payType = dt.Rows[0]["payType"].ToString(),
                    payTime = dt.Rows[0]["payTime"].ToString(),
                    formId = dt.Rows[0]["formId"].ToString(),
                };
            }
            return bill;
        }



        /// <summary>
        /// 修改支付状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="payNo"></param>
        public bool updateOrderForPay(string billId, string payNo)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(PaymentSqls.SELECT_BILL_BY_BILLID, billId);
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt.Rows.Count > 0)
            {
                StringBuilder builder1 = new StringBuilder();
                builder1.AppendFormat(PaymentSqls.UPDATE_PAYINFO_BY_BILLLIST, payNo, billId);
                string sql1 = builder1.ToString();

                if (DatabaseOperationWeb.ExecuteDML(sql1))
                {
                    StringBuilder builder2 = new StringBuilder();
                    builder2.AppendFormat(PaymentSqls.UPDATE_RESELLERGOODS , dt.Rows[0]["MEMBER_ID"].ToString());
                    string sql2 = builder2.ToString();
                    return DatabaseOperationWeb.ExecuteDML(sql2);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 保存支付日志
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="payNo"></param>
        /// <param name="totalPrice"></param>
        /// <param name="openid"></param>
        /// <param name="status"></param>
        public void insertPayLog(string orderId, string payNo, string totalPrice, string openid, string status)
        {
            StringBuilder builder1 = new StringBuilder();
            builder1.AppendFormat(PaymentSqls.INSERT_PAYLOG, orderId, payNo, totalPrice, openid, status);
            string sql1 = builder1.ToString();

            DatabaseOperationWeb.ExecuteDML(sql1);
        }

        public Bill AddBill(string memberId, ResellerGoods resellerGoods, PaymentParam paymentParam)
        {
            Bill bill = null;
            string billId = "LB"+DateTime.Now.ToString("yyyyMMddHHmmssfff");
            
            StringBuilder builder1 = new StringBuilder();
            builder1.AppendFormat(PaymentSqls.INSERT_BILL, billId, memberId, resellerGoods.goodsPrice,
                                resellerGoods.goodsId, resellerGoods.barcode, resellerGoods.goodsName, 
                                resellerGoods.goodsImg, resellerGoods.goodsNum,paymentParam.buyerName,
                                paymentParam.buyerPhone, paymentParam.buyerAddr);
            string sql1 = builder1.ToString();

            if (DatabaseOperationWeb.ExecuteDML(sql1))
            {
                bill = new Bill
                {
                    id = "",
                    billId = billId,
                    memberId = memberId,
                    billTime ="",
                    billState ="1",
                    billPrice =Convert.ToDouble( resellerGoods.goodsPrice),
                    goodsId = resellerGoods.goodsId,
                    barcode = resellerGoods.barcode,
                    goodsName = resellerGoods.goodsName,
                    goodsImg = resellerGoods.goodsImg,
                    goods_num =Convert.ToInt16( resellerGoods.goodsNum),
                };
            }
            return bill;
        }


        public class PaymentSqls
        {
            public const string UPDATE_BILLLIST_FOR_PAYID = 
                "UPDATE T_BILL_LIST " +
                "SET PREPAYID='{0}',prePayTime = now() " +
                "WHERE BILL_ID = '{1}' ";
            public const string SELECT_BILL_BY_BILLID = 
                "SELECT * " +
                "FROM T_BILL_LIST  " +
                "WHERE BILL_ID = '{0}'   ";
            public const string UPDATE_PAYINFO_BY_BILLLIST = 
                "UPDATE T_BILL_LIST " +
                "SET PAYNO='{0}',PAYTYPE='微信支付', PAYTIME=NOW(),BILL_STATE ='2' " +
                "WHERE BILL_ID = '{1}' ";
            public const string UPDATE_RESELLERGOODS =
                "UPDATE T_BASE_MEMBER " +
                "SET IF_BUY_GOODS='1' " +
                "WHERE MEMBER_ID='{0}' ";
            

            public const string INSERT_PAYLOG = 
                "INSERT INTO T_LOG_PAY(ORDERID,PAYTYPE,PAYNO,TOTALPRICE,OPENID,CREATETIME,STATUS) " +
                "VALUES('{0}','微信支付','{1}',{2},'{3}',NOW(),'{4}')";

            public const string INSERT_BILL =
                "INSERT INTO T_BILL_LIST(BILL_ID, MEMBER_ID, BILL_TIME, BILL_STATE, BILL_PRICE, " +
                                      "GOODS_ID, BARCODE, GOODS_NAME, GOODS_IMG, GOODS_NUM," +
                                      "BUYER_NAME,BUYER_PHONE,BUYER_ADDR) " +
                "VALUES('{0}','{1}',NOW(),'1',{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')";
        }
    }
}
