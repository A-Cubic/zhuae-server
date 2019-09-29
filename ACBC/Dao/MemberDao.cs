using ACBC.Buss;
using Com.ACBC.Framework.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
                string[] phones = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    phones[i] = dt.Rows[i]["phone"].ToString();
                }
                var gdb = new SqlServerDB();
                list = gdb.GMember.Where(b => phones.Contains(b.ME_MobileNum)).ToList();
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
                string[] phones = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    phones[i] = dt.Rows[i]["phone"].ToString();
                }
                var gdb = new SqlServerDB();
                List<GMember> list1 = new List<GMember>();
                list1 = gdb.GMember.Where(b => phones.Contains(b.ME_MobileNum)).ToList();
                memberInfo.point = list1.Sum(b => b.ME_Point);
                memberInfo.score = list1.Sum(b => b.ME_Score);

            }
            return memberInfo;
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
                string[] phones = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    phones[i] = dt.Rows[i]["phone"].ToString();
                }
                var gdb = new SqlServerDB();
                list = gdb.GMember.Where(b => phones.Contains(b.ME_MobileNum)).ToList();

                string[] ids = new string[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    ids[i] = list[i].ME_ID;
                }
                list1 = gdb.GRechargeDetail.Where(b => ids.Contains(b.RD_ME_ID) && b.RD_TYPE == 0).OrderByDescending(b => b.RD_Time).ToList();
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
                string[] phones = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    phones[i] = dt.Rows[i]["phone"].ToString();
                }
                var gdb = new SqlServerDB();
                list = gdb.GMember.Where(b => phones.Contains(b.ME_MobileNum)).ToList();

                string[] ids = new string[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    ids[i] = list[i].ME_ID;
                }
                list1 = gdb.GRechargeDetail.Where(b => ids.Contains(b.RD_ME_ID) && b.RD_TYPE == 0).OrderByDescending(b => b.RD_Time).ToList();
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
    }
}
