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
                //string[] phones = new string[dt.Rows.Count];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    phones[i] = dt.Rows[i]["phone"].ToString();
                //}
                //var gdb = new SqlServerDB();
                //list = gdb.GMember.Where(b => phones.Contains(b.ME_MobileNum)).ToList();
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
                list = WSGetMemberInfo(phone);
            }
            return list;
        }

        public MemberInfo GetMemberInfo(string memberId)
        {
            MemberInfo memberInfo = new MemberInfo();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(MemberSqls.SELECT_PHONE_LIST_BY_MEMBER_ID, memberId);
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt != null&& dt.Rows.Count > 0)
            {
                //string[] phones = new string[dt.Rows.Count];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    phones[i] = dt.Rows[i]["phone"].ToString();
                //}
                //var gdb = new SqlServerDB();
                //List<GMember> list1 = new List<GMember>();
                //list1 = gdb.GMember.Where(b => phones.Contains(b.ME_MobileNum)).ToList();
                //memberInfo.point = list1.Sum(b => b.ME_Point);
                //memberInfo.score = list1.Sum(b => b.ME_Score);
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
                string txt = GetMemberPoint(phone);
                string[] sts = txt.Split("#");
                if (sts.Length==2)
                {
                    memberInfo.point =Convert.ToInt32( sts[0]);
                    memberInfo.score = Convert.ToInt32(sts[1]);
                }
                else
                {
                    memberInfo.point =0;
                    memberInfo.score = 0;
                }
                
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

        public string checkMemberPhone(string phone)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(MemberSqls.SELECT_MEMBER_PHONE_BY_PHONE, phone);
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt != null&&dt.Rows.Count>1)
            {
                return dt.Rows[0]["member_id"].ToString();
            }
            return "";
        }

        public bool addMemberPhone(string memberId,string phone)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(MemberSqls.ADD_MEMBER_PHONE, memberId,phone);
            string sql = builder.ToString();
            return DatabaseOperationWeb.ExecuteDML(sql);
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

            public const string SELECT_MEMBER_PHONE_BY_PHONE = ""
                + "SELECT * "
                + "FROM T_MEMBER_PHONE "
                + "WHERE PHONE = '{0}'";
            public const string ADD_MEMBER_PHONE = ""
                + "INSERT INTO T_MEMBER_PHONE(MEMBER_ID,PHONE,INPUT_TIME) "
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
        /// 获取点数
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
