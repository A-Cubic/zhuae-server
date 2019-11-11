using ACBC.Buss;
using ACBC.Common;
using Com.ACBC.Framework.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ACBC.Dao
{
    public class MemberDao
    {
        public List<GMember> GetCardList(string memberId)
        {
            List<GMember> list = new List<GMember>();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(MemberSqls.SELECT_PHONE_LIST_BY_MEMBER_ID, memberId);
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                string phone = "";
                string appPhone = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["shop_type"].ToString() == "1")
                    {
                        if (phone == "")
                        {
                            phone = "'" + dt.Rows[i]["phone"].ToString() + "'";
                        }
                        else
                        {
                            phone += ",'" + dt.Rows[i]["phone"].ToString() + "'";
                        }
                    }
                    else
                    {
                        if (appPhone == "")
                        {
                            appPhone = "'" + dt.Rows[i]["phone"].ToString() + "'";
                        }
                        else
                        {
                            appPhone += ",'" + dt.Rows[i]["phone"].ToString() + "'";
                        }
                    }
                }
                //线下店
                if (phone != "")
                {
                    list = WSGetMemberInfo(phone);
                }

                //app
                if (appPhone != "")
                {
                    DatabaseOperationWeb.TYPE = new DBManagerZE();
                    try
                    {
                        StringBuilder builder2 = new StringBuilder();
                        builder2.AppendFormat(MemberSqls.SELECT_APPUSER_LIST_BY_PHONES, appPhone);
                        string sql2 = builder2.ToString();
                        DataTable dt2 = DatabaseOperationWeb.ExecuteSelectDS(sql2, "T").Tables[0];
                        if (dt2 != null && dt2.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt2.Rows.Count; i++)
                            {
                                GMember gMember = new GMember
                                {
                                    ME_ID = dt2.Rows[i]["USER_NICKNAME"].ToString(),
                                    ME_Type = "APP",
                                    ME_Score = Convert.ToInt32(dt2.Rows[i]["GIFT_BALANCE"]),
                                    ME_Point = Convert.ToInt32(dt2.Rows[i]["TRUE_BALANCE"]),
                                    ME_MobileNum = dt2.Rows[i]["CHAT_USER_ID"].ToString(),
                                };
                                list.Add(gMember);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw new ApiException(CodeMessage.ZEMemberPhoneExists, "ZEMemberPhoneExists");
                    }
                    finally
                    {
                        DatabaseOperationWeb.TYPE = new DBManager();
                    }
                }

            }
            return list;
        }

        public MemberInfo GetMemberInfo(string memberId)
        {
            MemberInfo memberInfo = new MemberInfo();
            memberInfo.score = 0;
            memberInfo.point = 0;
            memberInfo.price = 0;
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(MemberSqls.SELECT_PHONE_LIST_BY_MEMBER_ID, memberId);
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                string phone = "";
                string appPhone = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["shop_type"].ToString() == "1")
                    {
                        if (phone == "")
                        {
                            phone = "'" + dt.Rows[i]["phone"].ToString() + "'";
                        }
                        else
                        {
                            phone += ",'" + dt.Rows[i]["phone"].ToString() + "'";
                        }
                    }
                    else
                    {
                        if (appPhone == "")
                        {
                            appPhone = "'" + dt.Rows[i]["phone"].ToString() + "'";
                        }
                        else
                        {
                            appPhone += ",'" + dt.Rows[i]["phone"].ToString() + "'";
                        }
                    }
                }
                //线下店
                if (phone != "")
                {
                    string txt = GetMemberPoint(phone);
                    string[] sts = txt.Split("#");
                    if (sts.Length == 2)
                    {
                        int.TryParse(sts[0], out memberInfo.point);
                        int.TryParse(sts[1], out memberInfo.score);
                    }
                    else
                    {
                        memberInfo.point = 0;
                        memberInfo.score = 0;
                    }
                }

                //app
                if (appPhone != "")
                {
                    DatabaseOperationWeb.TYPE = new DBManagerZE();
                    try
                    {
                        StringBuilder builder2 = new StringBuilder();
                        builder2.AppendFormat(MemberSqls.SELECT_APPUSER_POINT_LIST_BY_PHONES, appPhone);
                        string sql2 = builder2.ToString();
                        DataTable dt2 = DatabaseOperationWeb.ExecuteSelectDS(sql2, "T").Tables[0];
                        if (dt2 != null && dt2.Rows.Count > 0)
                        {
                            memberInfo.point += Convert.ToInt32(dt2.Rows[0]["point"]);
                            memberInfo.score += Convert.ToInt32(dt2.Rows[0]["score"]);
                        }
                    }
                    catch (Exception)
                    {
                        throw new ApiException(CodeMessage.ZEMemberPhoneExists, "ZEMemberPhoneExists");
                    }
                    finally
                    {
                        DatabaseOperationWeb.TYPE = new DBManager();
                    }
                }
            }
            StringBuilder builder1 = new StringBuilder();
            builder1.AppendFormat(MemberSqls.SELECT_MEMBER_BY_MEMBER_ID, memberId);
            string sql1 = builder1.ToString();
            DataTable dt1 = DatabaseOperationWeb.ExecuteSelectDS(sql1, "T").Tables[0];
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                memberInfo.price = Convert.ToInt32(dt1.Rows[0]["reseller_price"]);
            }
            else
            {
                memberInfo.price = 0;
            }
            return memberInfo;
        }

        public bool checkSQLMemberPhone(string phone)
        {
            List<GMember> list = new List<GMember>();
            var gdb = new SqlServerDB();
            list = gdb.GMember.Where(b => b.ME_MobileNum == phone).ToList();
            if (list.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string checkMemberPhone(string phone, string shopType)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(MemberSqls.SELECT_MEMBER_PHONE_BY_PHONE_AND_SHOPTYPE, phone, shopType);
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0]["member_id"].ToString();
            }
            return "";
        }

        public bool addMemberPhone(string memberId, string phone, string shopType)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(MemberSqls.ADD_MEMBER_PHONE, memberId, phone, shopType);
            string sql = builder.ToString();
            return DatabaseOperationWeb.ExecuteDML(sql);
        }

        public string checkMemberLeek(string memberId, string leekMemberId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(MemberSqls.SELECT_MEMBERLEEK_BY_LEEKMEMBER_ID, leekMemberId);
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["member_id"].ToString() == memberId)
                {
                    return "20005";
                }
                else
                {
                    return "20006";
                }
            }
            return "";
        }

        public bool addLeek(string memberId, string leekMemberId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(MemberSqls.SELECT_MEMBER_BY_MEMBER_ID, leekMemberId);
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                StringBuilder builder1 = new StringBuilder();
                builder1.AppendFormat(MemberSqls.ADD_MEMBER_LEEK, memberId, leekMemberId, dt.Rows[0]["MEMBER_NAME"]);
                string sql1 = builder1.ToString();
                return DatabaseOperationWeb.ExecuteDML(sql1);
            }
            else
            {
                return false;
            }
        }

        public List<GRechargeDetail> getExchangeList(string memberId)
        {
            List<GRechargeDetail> list1 = new List<GRechargeDetail>();
            List<GMember> list = new List<GMember>();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(MemberSqls.SELECT_PHONE_LIST_BY_MEMBER_ID, memberId);
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                //string[] phones = new string[dt.Rows.Count];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    phones[i] = dt.Rows[i]["phone"].ToString();
                //}
                //var gdb = new SqlServerDB();
                //list = gdb.GMember.Where(b => phones.Contains(b.ME_MobileNum)).ToList();

                //string[] ids = new string[list.Count];
                //for (int i = 0; i < list.Count; i++)
                //{
                //    ids[i] = list[i].ME_ID;
                //}
                //list1 = gdb.GRechargeDetail.Where(b => ids.Contains(b.RD_ME_ID) && b.RD_TYPE == 0).OrderByDescending(b => b.RD_Time).ToList();

                string phone = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        phone = "'" + dt.Rows[i]["phone"].ToString() + "'";
                    }
                    else
                    {
                        phone += ",'" + dt.Rows[i]["phone"].ToString() + "'";
                    }
                }
                list1 = GetGRechargeDetailInfo(phone);
            }
            return list1;
        }
        public List<GRechargeDetail> getRechargeList(string memberId)
        {
            List<GRechargeDetail> list1 = new List<GRechargeDetail>();
            List<GMember> list = new List<GMember>();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(MemberSqls.SELECT_PHONE_LIST_BY_MEMBER_ID, memberId);
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                //string[] phones = new string[dt.Rows.Count];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    phones[i] = dt.Rows[i]["phone"].ToString();
                //}
                //var gdb = new SqlServerDB();
                //list = gdb.GMember.Where(b => phones.Contains(b.ME_MobileNum)).ToList();

                //string[] ids = new string[list.Count];
                //for (int i = 0; i < list.Count; i++)
                //{
                //    ids[i] = list[i].ME_ID;
                //}
                //list1 = gdb.GRechargeDetail.Where(b => ids.Contains(b.RD_ME_ID) && b.RD_TYPE == 0).OrderByDescending(b => b.RD_Time).ToList();

                string phone = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        phone = "'" + dt.Rows[i]["phone"].ToString() + "'";
                    }
                    else
                    {
                        phone += ",'" + dt.Rows[i]["phone"].ToString() + "'";
                    }
                }
                list1 = GetGRechargeDetailInfo(phone);
            }
            return list1;
        }

        public string getResellerImg(string memberId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(MemberSqls.SELECT_RESELLERIMG_BY_MEMBER_ID, memberId);
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public double getProportion()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(MemberSqls.SELECT_PROPORTION);
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToDouble(dt.Rows[0][0]);
            }
            else
            {
                return 0;
            }
        }
        public List<Leek> getLeekListByMemberIdAndPageNum(string memberId, int pageNum)
        {
            List<Leek> list = new List<Leek>();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(MemberSqls.SELECT_RESELLERLEEK_LIST_BY_MEMBER_ID, memberId, pageNum * 15);
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Leek leek = new Leek
                    {
                        leekName = dr["leek_name"].ToString(),
                        createTime = dr["createTime"].ToString()
                    };
                    list.Add(leek);
                }
            }

            return list;
        }
        public List<ResellerAccount> getAccountListByMemberIdAndPageNum(string memberId, int pageNum)
        {
            List<ResellerAccount> list = new List<ResellerAccount>();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(MemberSqls.SELECT_RESELLERACCOUNT_LIST_BY_MEMBER_ID, memberId, pageNum * 15);
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ResellerAccount resellerAccount = new ResellerAccount
                    {
                        acount_date = dr["acount_date"].ToString().Substring(0, 10),
                        member_id = dr["member_id"].ToString(),
                        member_name = dr["member_name"].ToString(),
                        phone = dr["phone"].ToString(),
                        acount_price = Convert.ToDouble(dr["acount_price"]),
                        reseller_price = Convert.ToDouble(dr["reseller_price"]),
                        createTime = dr["createTime"].ToString(),
                    };
                    list.Add(resellerAccount);
                }
            }

            return list;
        }
        public void getAccountSelectList()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(MemberSqls.SELECT_MAX_ACCOUNT_DAY);
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                double proportion = getProportion();
                DateTime maxDate = Convert.ToDateTime(dt.Rows[0][0]).AddDays(1);
                for (DateTime i = maxDate; i < DateTime.Now; i = i.AddDays(1))
                {
                    ArrayList al = new ArrayList();
                    string dateStr = i.ToString("yyyy-MM-dd");
                    StringBuilder builder1 = new StringBuilder();
                    builder1.AppendFormat(MemberSqls.SELECT_LEEK_PHONE_BY_DATE, i.AddDays(1).ToString("yyyy-MM-dd"));
                    string sql1 = builder1.ToString();
                    DataTable dt1 = DatabaseOperationWeb.ExecuteSelectDS(sql1, "T").Tables[0];
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        string phone = "";
                        foreach (DataRow dr in dt1.Rows)
                        {
                            if (phone == "")
                            {
                                phone = "'" + dr["phone"].ToString() + "'";
                            }
                            else
                            {
                                phone += ",'" + dr["phone"].ToString() + "'";
                            }
                        }
                        try
                        {
                            DatabaseOperationWeb.TYPE = new DBManagerZE();
                            StringBuilder builder2 = new StringBuilder();
                            builder2.AppendFormat(MemberSqls.SELECT_RECHARGE_LIST_BY_PHONES, dateStr, phone);
                            string sql2 = builder2.ToString();
                            DataTable dt2 = DatabaseOperationWeb.ExecuteSelectDS(sql2, "T").Tables[0];
                            if (dt2 != null && dt2.Rows.Count > 0)
                            {
                                foreach (DataRow dr2 in dt2.Rows)
                                {
                                    DataRow[] drs = dt1.Select("phone='" + dr2["chat_user_id"].ToString() + "'");
                                    if (drs.Length > 0)
                                    {
                                        double price = Convert.ToDouble(dr2["ALLREC_MONEY"]) * proportion;
                                        StringBuilder builder3 = new StringBuilder();
                                        builder3.AppendFormat(MemberSqls.ADD_ACCOUNT, dateStr, drs[0]["LEEK_MEMBER_ID"].ToString(),
                                            drs[0]["LEEK_NAME"].ToString(), drs[0]["PHONE"].ToString(),
                                            Convert.ToDouble(dr2["ALLREC_MONEY"]), price.ToString());
                                        string sql3 = builder3.ToString();
                                        al.Add(sql3);
                                    }


                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new ApiException(CodeMessage.ACCOUNTZEExists, "ACCOUNTZEExists");
                        }
                        finally
                        {
                            DatabaseOperationWeb.TYPE = new DBManager();
                        }
                    }
                    StringBuilder builder4 = new StringBuilder();
                    builder4.AppendFormat(MemberSqls.ADD_ACCOUNT_LOG, al.Count, dateStr);
                    string sql4 = builder4.ToString();
                    al.Add(sql4);
                    DatabaseOperationWeb.ExecuteDML(al);
                }
            }

        }

        private class MemberSqls
        {
            public const string SELECT_PHONE_LIST_BY_MEMBER_ID = ""
                + "SELECT * "
                + "FROM T_MEMBER_PHONE "
                + "WHERE MEMBER_ID = {0} ";
            public const string SELECT_MEMBER_BY_MEMBER_ID = ""
                + "SELECT * "
                + "FROM T_BASE_MEMBER "
                + "WHERE MEMBER_ID = {0}";

            public const string SELECT_MEMBER_PHONE_BY_PHONE_AND_SHOPTYPE = ""
                + "SELECT * "
                + "FROM T_MEMBER_PHONE "
                + "WHERE PHONE = '{0}' AND SHOP_TYPE='{1}'";
            public const string ADD_MEMBER_PHONE = ""
                + "INSERT INTO T_MEMBER_PHONE(MEMBER_ID,PHONE,SHOP_TYPE,INPUT_TIME) "
                + "VALUES('{0}','{1}','{2}',NOW())";
            public const string ADD_MEMBER_LEEK = ""
                + "INSERT INTO T_MEMBER_LEEK(MEMBER_ID,LEEK_MEMBER_ID,LEEK_NAME,CREATETIME) "
                + "VALUES('{0}','{1}','{2}',NOW())";

            public const string SELECT_RESELLERIMG_BY_MEMBER_ID = ""
                + "SELECT RESELLER_IMG "
                + "FROM T_BASE_MEMBER "
                + "WHERE MEMBER_ID = {0} AND IF_RESELLER='1' ";
            public const string SELECT_RESELLERLEEK_LIST_BY_MEMBER_ID = ""
                + "SELECT * "
                + "FROM T_MEMBER_LEEK "
                + "WHERE MEMBER_ID = {0} "
                + "ORDER BY ID DESC "
                + "LIMIT {1},15  ";
            public const string SELECT_RESELLERACCOUNT_LIST_BY_MEMBER_ID = ""
                + "SELECT A.* "
                + "FROM T_MEMBER_LEEK L ,T_ACCOUNT_LIST A "
                + "WHERE L.LEEK_MEMBER_ID = A.MEMBER_ID "
                + "ORDER BY A.ID DESC "
                + "LIMIT {1},15  ";


            public const string SELECT_APPUSER_LIST_BY_PHONES = ""
                + "SELECT F.TRUE_BALANCE,F.GIFT_BALANCE, U.USER_NICKNAME,U.CHAT_USER_ID  " +
                "FROM F_BALANCE F,U_USER U " +
                "WHERE F.USER_ID = U.USER_ID " +
                  "AND U.CHAT_USER_ID IN ({0})";
            public const string SELECT_APPUSER_POINT_LIST_BY_PHONES = ""
                + "SELECT SUM(F.TRUE_BALANCE) POINT,SUM(F.GIFT_BALANCE) SCORE  " +
                "FROM F_BALANCE F,U_USER U " +
                "WHERE F.USER_ID = U.USER_ID " +
                  "AND U.CHAT_USER_ID IN ({0})";

            public const string SELECT_MEMBERLEEK_BY_LEEKMEMBER_ID = ""
                + "SELECT * "
                + "FROM T_MEMBER_LEEK "
                + "WHERE LEEK_MEMBER_ID = {0} ";
            public const string SELECT_MAX_ACCOUNT_DAY = ""
                + "SELECT ACCOUNT_DATE " +
                "FROM T_ACCOUNT_LOG " +
                "ORDER BY ID DESC " +
                "LIMIT 1  ";
            public const string SELECT_LEEK_PHONE_BY_DATE = ""
                + "SELECT * " +
                "FROM T_MEMBER_LEEK L ,T_MEMBER_PHONE P " +
                "WHERE L.LEEK_MEMBER_ID = P.MEMBER_ID " +
                "AND PHONE <> '' " +
                "AND L.CREATETIME <STR_TO_DATE('{0}', '%Y-%m-%d') ";
            public const string SELECT_RECHARGE_LIST_BY_PHONES = ""
                + "SELECT U.CHAT_USER_ID,C.ALLREC_MONEY/10 as ALLREC_MONEY " +
                "FROM REPORT_RECHARGE_COUNT C ,U_USER U " +
                "WHERE C.USER_ID = U.USER_ID " +
                "AND C.INSERT_DATE = '{0}' " +
                "AND U.CHAT_USER_ID in ({1}) " +
                "AND ALLREC_MONEY >0";
            public const string SELECT_PROPORTION = ""
                + "SELECT CONFIG_VALUE FROM T_SYS_CONFIG WHERE CONFIG_CODE='001'";


            public const string ADD_ACCOUNT = ""
                + "INSERT INTO T_ACCOUNT_LIST(ACOUNT_DATE,MEMBER_ID,MEMBER_NAME,PHONE,ACOUNT_PRICE,RESELLER_PRICE,CREATETIME) "
                + "VALUES('{0}',{1},'{2}','{3}',{4},{5},NOW())";
            public const string ADD_ACCOUNT_LOG = ""
                + "INSERT INTO T_ACCOUNT_LOG(ACCOUNT_COUNT,ACCOUNT_DATE,CREATETIME) "
                + "VALUES('{0}','{1}',NOW())";
        }

        /// <summary>
        /// 获取点数
        /// </summary>
        /// <param name="posCode"></param>
        /// <returns></returns>
        public String GetMemberPoint(string phone)
        {
            string result = "";
            // 创建 HTTP 绑定对象
            var binding = new BasicHttpBinding();
            // 根据 WebService 的 URL 构建终端点对象
            var endpoint = new EndpointAddress(Global.WebServiceUrl);
            // 创建调用接口的工厂，注意这里泛型只能传入接口 添加服务引用时生成的 webservice的接口 一般是 (XXXSoap)
            var factory = new ChannelFactory<WebService.ZEWebService1Soap>(binding, endpoint);
            // 从工厂获取具体的调用实例
            var callClient = factory.CreateChannel();
            //调用的对应webservice 服务类的函数生成对应的请求类Body (一般是webservice 中对应的方法+RequestBody  如GetInfoListRequestBody)
            WebService.GetMemberPointRequestBody body = new WebService.GetMemberPointRequestBody();
            //以下是为该请求body添加对应的参数（就是调用webService中对应的方法的参数）
            body.phone = phone;
            //获取请求对象 （一般是webservice 中对应的方法+tRequest  如GetInfoListRequest）
            var request = new WebService.GetMemberPointRequest(body);
            //发送请求
            var v = callClient.GetMemberPointAsync(request);
            //异步等待
            v.Wait();
            //获取数据
            result = v.Result.Body.GetMemberPointResult;
            return result;
        }

        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <param name="posCode"></param>
        /// <returns></returns>
        public List<GMember> WSGetMemberInfo(string phone)
        {
            List<GMember> list = new List<GMember>();
            // 创建 HTTP 绑定对象
            var binding = new BasicHttpBinding();
            // 根据 WebService 的 URL 构建终端点对象
            var endpoint = new EndpointAddress(Global.WebServiceUrl);
            // 创建调用接口的工厂，注意这里泛型只能传入接口 添加服务引用时生成的 webservice的接口 一般是 (XXXSoap)
            var factory = new ChannelFactory<WebService.ZEWebService1Soap>(binding, endpoint);
            // 从工厂获取具体的调用实例
            var callClient = factory.CreateChannel();
            //调用的对应webservice 服务类的函数生成对应的请求类Body (一般是webservice 中对应的方法+RequestBody  如GetInfoListRequestBody)
            WebService.GetMemberInfoRequestBody body = new WebService.GetMemberInfoRequestBody();
            //以下是为该请求body添加对应的参数（就是调用webService中对应的方法的参数）
            body.phone = phone;
            //获取请求对象 （一般是webservice 中对应的方法+tRequest  如GetInfoListRequest）
            var request = new WebService.GetMemberInfoRequest(body);
            //发送请求
            var v = callClient.GetMemberInfoAsync(request);
            //异步等待
            v.Wait();
            //获取数据
            var result = v.Result.Body.GetMemberInfoResult;
            foreach (var member in result)
            {
                GMember gMember = new GMember
                {
                    ME_ID = member.ME_ID,
                    ME_Type = "线下店",
                    ME_Score = member.ME_Score,
                    ME_Point = member.ME_Point,
                    ME_MobileNum = member.ME_MobileNum,
                };
                list.Add(gMember);
            }
            return list;
        }

        /// <summary>
        /// 获取点数
        /// </summary>
        /// <param name="posCode"></param>
        /// <returns></returns>
        public List<GRechargeDetail> GetGRechargeDetailInfo(string phone)
        {
            List<GRechargeDetail> list = new List<GRechargeDetail>();
            // 创建 HTTP 绑定对象
            var binding = new BasicHttpBinding();
            // 根据 WebService 的 URL 构建终端点对象
            var endpoint = new EndpointAddress(Global.WebServiceUrl);
            // 创建调用接口的工厂，注意这里泛型只能传入接口 添加服务引用时生成的 webservice的接口 一般是 (XXXSoap)
            var factory = new ChannelFactory<WebService.ZEWebService1Soap>(binding, endpoint);
            // 从工厂获取具体的调用实例
            var callClient = factory.CreateChannel();
            //调用的对应webservice 服务类的函数生成对应的请求类Body (一般是webservice 中对应的方法+RequestBody  如GetInfoListRequestBody)
            WebService.GetGRechargeDetailInfoRequestBody body = new WebService.GetGRechargeDetailInfoRequestBody();
            //以下是为该请求body添加对应的参数（就是调用webService中对应的方法的参数）
            body.phone = phone;
            //获取请求对象 （一般是webservice 中对应的方法+tRequest  如GetInfoListRequest）
            var request = new WebService.GetGRechargeDetailInfoRequest(body);
            //发送请求
            var v = callClient.GetGRechargeDetailInfoAsync(request);
            //异步等待
            v.Wait();
            //获取数据
            var result = v.Result.Body.GetGRechargeDetailInfoResult;
            foreach (var GRecharge in result)
            {
                GRechargeDetail gRechargeDetail = new GRechargeDetail
                {
                    RD_GUID = GRecharge.RD_GUID,
                    RD_ME_ID = GRecharge.RD_ME_ID,
                    RD_TYPE = GRecharge.RD_TYPE,
                    RD_Money = GRecharge.RD_Money,
                    RD_Point = GRecharge.RD_Point,
                    RD_GiveScore = GRecharge.RD_GiveScore,
                    RD_Time = GRecharge.RD_Time,
                };
                list.Add(gRechargeDetail);
            }

            return list;
        }
    }
}
