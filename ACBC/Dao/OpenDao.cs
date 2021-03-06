﻿using ACBC.Buss;
using Com.ACBC.Framework.Database;
using System;
using System.Collections;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace ACBC.Dao
{
    public class OpenDao
    {

        public Member GetMember(string openID)
        {
            Member member = null;

            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(OpenSqls.SELECT_MEMBER_BY_OPENID, openID);
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt != null && dt.Rows.Count == 1)
            {
                member = new Member
                {
                    memberId = dt.Rows[0]["MEMBER_ID"].ToString(),
                    memberImg = dt.Rows[0]["MEMBER_IMG"].ToString(),
                    memberName = dt.Rows[0]["MEMBER_NAME"].ToString(),
                    memberPhone = dt.Rows[0]["MEMBER_PHONE"].ToString(),
                    memberSex = dt.Rows[0]["MEMBER_SEX"].ToString(),
                    openid = dt.Rows[0]["OPENID"].ToString(),
                    scanCode = "CHECK_" + dt.Rows[0]["SCAN_CODE"].ToString(),
                    reseller = (dt.Rows[0]["IF_RESELLER"].ToString() == "1"),
                };
            }

            return member;
        }

        public bool MemberReg(MemberRegParam memberRegParam, string openID)
        {
            string scanCode = "";
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(openID));
                var strResult = BitConverter.ToString(result);
                scanCode = strResult.Replace("-", "");
            }

            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(OpenSqls.INSERT_MEMBER,
                memberRegParam.nickName,
                memberRegParam.avatarUrl,
                memberRegParam.gender,
                openID,
                scanCode);
            string sqlInsert = builder.ToString();
            
            return DatabaseOperationWeb.ExecuteDML(sqlInsert);
        }

        public StoreUser GetStoreUser(string openID)
        {
            StoreUser storeUser = null;

            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(OpenSqls.SELECT_STORE_USER_BY_OPENID, openID);
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt != null && dt.Rows.Count == 1)
            {
                storeUser = new StoreUser
                {
                    storeUserId = dt.Rows[0]["STORE_USER_ID"].ToString(),
                    storeUserImg = dt.Rows[0]["STORE_USER_IMG"].ToString(),
                    storeUserName = dt.Rows[0]["STORE_USER_NAME"].ToString(),
                    storeUserPhone = dt.Rows[0]["STORE_USER_PHONE"].ToString(),
                    storeUserSex = dt.Rows[0]["STORE_USER_SEX"].ToString(),
                    openid = dt.Rows[0]["OPENID"].ToString(),
                    storeId = dt.Rows[0]["STORE_ID"].ToString(),
                };
            }

            return storeUser;
        }

        public string GetStoreId(string storeCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(OpenSqls.SELECT_STORE_CODE, storeCode);
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt != null && dt.Rows.Count == 1)
            {
                return dt.Rows[0]["STORE_ID"].ToString();
            }

            return "";
        }

        public bool StoreUserReg(StoreUserRegParam storeUserRegParam, string openID, string storeId)
        {
            ArrayList list = new ArrayList();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(OpenSqls.INSERT_STORE_USER,
                storeUserRegParam.nickName,
                storeUserRegParam.avatarUrl,
                storeUserRegParam.gender,
                openID,
                storeId);
            string sqlInsert = builder.ToString();
            list.Add(sqlInsert);
            builder.Clear();
            builder.AppendFormat(OpenSqls.UPDATE_STORE_CODE,
                storeUserRegParam.storeCode);
            sqlInsert = builder.ToString();
            list.Add(sqlInsert);
            return DatabaseOperationWeb.ExecuteDML(list);
        }

        public void add_log(string logType, string value, string memberId)
        {
            StringBuilder builder1 = new StringBuilder();
            builder1.AppendFormat(OpenSqls.ADD_LOG, logType, memberId, value);
            string sql1 = builder1.ToString();
            DatabaseOperationWeb.ExecuteDML(sql1);
        }

        public void checkLeekLog(string openId,string leekMemberId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(OpenSqls.SELECT_LEEK_LOG, openId);
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt != null && dt.Rows.Count >0)
            {
                string memberId = dt.Rows[0]["MEMBER_ID"].ToString();
                MemberDao memberDao = new MemberDao();
                if (memberDao.checkMemberLeek(memberId, leekMemberId) == "20005")
                {
                    
                }
                else if (memberDao.checkMemberLeek(memberId, leekMemberId) == "20006")
                {

                }
                else
                {
                    memberDao.addLeek(memberId, leekMemberId);
                }
            }
        }
        private class OpenSqls
        {
            public const string SELECT_MEMBER_BY_OPENID = ""
                + "SELECT * "
                + "FROM T_BASE_MEMBER "
                + "WHERE OPENID = '{0}'";
            public const string INSERT_MEMBER = ""
                + "INSERT INTO T_BASE_MEMBER "
                + "(MEMBER_NAME,MEMBER_IMG,MEMBER_SEX,OPENID,SCAN_CODE)"
                + "VALUES( "
                + "'{0}','{1}','{2}','{3}','{4}')";
            public const string SELECT_STORE_USER_BY_OPENID = ""
                + "SELECT * "
                + "FROM T_BASE_STORE_USER "
                + "WHERE OPENID = '{0}'";
            public const string INSERT_STORE_USER = ""
                + "INSERT INTO T_BASE_STORE_USER "
                + "(STORE_USER_NAME,STORE_USER_IMG,STORE_USER_SEX,OPENID,STORE_ID)"
                + "VALUES( "
                + "'{0}','{1}','{2}','{3}',{4})";
            public const string UPDATE_STORE_CODE = ""
                + "UPDATE T_BUSS_STORE_CODE "
                + "SET STATE = STATE - 1 "
                + "WHERE STORE_CODE = '{0}' ";
            public const string SELECT_STORE_CODE = ""
                + "SELECT * "
                + "FROM T_BUSS_STORE_CODE "
                + "WHERE STORE_CODE = '{0}' "
                + "AND STATE > 0";
            public const string ADD_LOG = ""
                + "INSERT INTO T_BASE_LOG(LOG_TYPE,MEMBER_ID,LOG_TIME,LOG_VALUE) "
                + "VALUES('{0}','{1}',NOW(),'{2}')";
            public const string SELECT_LEEK_LOG = ""
                + "SELECT * "
                + "FROM T_MEMBER_LEEK_LOG "
                + "WHERE LEEK_OPEN_ID = '{0}' "
                + "AND FLAG ='0' " 
                + "ORDER BY ID ASC LIMIT 1";
        }
    }
}
