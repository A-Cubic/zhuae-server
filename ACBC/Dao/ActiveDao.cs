using ACBC.Buss;
using ACBC.Common;
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
    public class ActiveDao
    {


        public List<ActiveList> GetActiveListAll()
        {
            if (DatabaseOperationWeb.TYPE.GetType().Name != "DBManager")
            {
                DatabaseOperationWeb.TYPE = new DBManager();
            }

            List<ActiveList> list = new List<ActiveList>();
            string select = OrderSqls.SELECT_T_ACTIVE_LIST_ALL;
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(select, "T").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ActiveList al = new ActiveList
                    {
                        activeId = Convert.ToInt32(dr["ACTIVE_ID"]),
                        activeName = dr["ACTIVE_NAME"].ToString(),
                        flag = dr["FLAG"].ToString(),
                    };
                    list.Add(al);
                }
            }
            return list;
        }

        public int GetMemberCount()
        {
            if(DatabaseOperationWeb.TYPE.GetType().Name != "DBManager")
            {
                DatabaseOperationWeb.TYPE = new DBManager();
            }

            string select = OrderSqls.SELECT_T_ACTIVE_MEMBER_ALL_COUNT;
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(select, "T").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                int count = Convert.ToInt32(dr["CNT"]);
                return count;
            }
            return 0;

        }

        public bool AddActiveMember(ActiveMember activeMember)
        {
            if (DatabaseOperationWeb.TYPE.GetType().Name != "DBManager")
            {
                DatabaseOperationWeb.TYPE = new DBManager();
            }

            StringBuilder builder = new StringBuilder();

            builder.AppendFormat(OrderSqls.INSERT_INTO_T_ACTIVE_MEMBER,
                0,
                activeMember.activeId,
                activeMember.memberId,
                activeMember.memberName,
                activeMember.memberImg,
                activeMember.createTime.ToString("G"));
            string insert = builder.ToString();
            if (DatabaseOperationWeb.ExecuteDML(insert))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ActiveMember GetActiveMemberByMemberId(int memberId)
        {
            if (DatabaseOperationWeb.TYPE.GetType().Name != "DBManager")
            {
                DatabaseOperationWeb.TYPE = new DBManager();
            }

            ActiveMember activeMember = new ActiveMember();
            activeMember.memberId = 0;
            activeMember.activeId = 0;
            activeMember.memberName = "";
            activeMember.memberImg = "";
            activeMember.createTime = new DateTime(0);
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(OrderSqls.SELECT_T_ACTIVE_MEMBER_BY_MEMBER_ID, memberId);
            string select = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(select, "T").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                activeMember.memberId = Convert.ToInt32(dr["MEMBER_ID"]);
                activeMember.activeId = Convert.ToInt32(dr["ACTIVE_ID"]);
                activeMember.memberName = dr["MEMBER_NAME"].ToString();
                activeMember.memberImg = dr["MEMBER_IMG"].ToString();
                activeMember.createTime = DateTime.Parse(dr["CREATETIME"].ToString());
            }
            return activeMember;
        }

        public ActiveInfo GetActiveInfoByStage(int activeStage)
        {
            if (DatabaseOperationWeb.TYPE.GetType().Name != "DBManager")
            {
                DatabaseOperationWeb.TYPE = new DBManager();
            }

            ActiveInfo activeInfo = new ActiveInfo();
            activeInfo.activeId = 0;
            activeInfo.activeStage = "";
            activeInfo.activeValue = "";
            activeInfo.activeText = "";
            activeInfo.activeImg = "";
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(OrderSqls.SELECT_T_ACTIVE_INFO_BY_ACTIVE_STAGE, activeStage);
            string select = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(select, "T").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                activeInfo.activeId = Convert.ToInt32(dr["ACTIVE_ID"]);
                activeInfo.activeStage = dr["ACTIVE_STAGE"].ToString();
                activeInfo.activeValue = dr["ACTIVE_VALUE"].ToString();
                activeInfo.activeText = dr["ACTIVE_TEXT"].ToString();
                activeInfo.activeImg = dr["ACTIVE_IMG"].ToString();
            }
            return activeInfo;

        }

        public bool addActiveList(ActiveList activeList)
        {
            if (DatabaseOperationWeb.TYPE.GetType().Name != "DBManager")
            {
                DatabaseOperationWeb.TYPE = new DBManager();
            }

            StringBuilder builder = new StringBuilder();

            builder.AppendFormat(OrderSqls.INSERT_INTO_T_ACTIVE_LIST,
                0,
                activeList.activeName,
                activeList.flag
            );
            string insert = builder.ToString();
            if (DatabaseOperationWeb.ExecuteDML(insert))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /**
         * 假的，用于获取总金额
         * */
        public int getMoney()
        {
            int money = 0;
            int add_money = 0;
            //try
            //{
            //    DatabaseOperationWeb.TYPE = new DBManagerZE();
            //    string select = OrderSqls.SELECT_TOTAL_ALLREC_MONEY;
            //    DataTable dt1 = DatabaseOperationWeb.ExecuteSelectDS(select, "T").Tables[0];
            //    if (dt1 != null && dt1.Rows.Count > 0)
            //    {
            //        int.TryParse(dt1.Rows[0][0].ToString(), out money);
            //    }
            //}
            //catch (Exception e)
            //{ 
            //}
            //finally
            //{
            //    DatabaseOperationWeb.TYPE = new DBManager();
            //}
            MangoDBHelp mangoDBHelp = new MangoDBHelp();
            money = mangoDBHelp.getTotalPrice();

            StringBuilder builder = new StringBuilder();

            builder.AppendFormat(OrderSqls.SELECT_ADD_TEMP_ALLREC_MONEY, "NewYear");
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                int.TryParse(dt.Rows[0]["ACTIVE_NUMBER"].ToString(), out add_money);
                
            }
            return money + add_money;
        }

        public bool addActiveInfo(ActiveInfo activeInfo)
        {
            if (DatabaseOperationWeb.TYPE.GetType().Name != "DBManager")
            {
                DatabaseOperationWeb.TYPE = new DBManager();
            }

            StringBuilder builder = new StringBuilder();

            builder.AppendFormat(OrderSqls.INSERT_INTO_T_ACTIVE_INFO,
                0,
                activeInfo.activeId,
                activeInfo.activeStage,
                activeInfo.activeValue,
                activeInfo.activeText,
                activeInfo.activeImg
            );
            string insert = builder.ToString();
            if (DatabaseOperationWeb.ExecuteDML(insert))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<LeaderBoard> GetLeaderBoardListAll()
        {
            List<LeaderBoard> list = new List<LeaderBoard>();
            try
            {
                DatabaseOperationWeb.TYPE = new DBManagerZE();
                string select = OrderSqls.SELECT_LEADERBOARD;
                DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(select, "T").Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        LeaderBoard al = new LeaderBoard();

                        al.id = Convert.ToInt32(dr[0]);
                        if (dr[1].ToString().Length >= 7)
                            al.nickname = dr[1].ToString().Substring(0, 6) + "...";
                        else
                            al.nickname = dr[1].ToString();
                        al.money = Convert.ToInt32(dr[2]).ToString("0.##");
                        list.Add(al);
                    }
                }
            }
            catch(Exception e)
            {
                throw new ApiException(CodeMessage.ACCOUNTZEExists, "ACCOUNTZEExists");
            }
            finally
            {
                DatabaseOperationWeb.TYPE = new DBManager();

            }

            return list;
        }

        /***********************************
        public List<Qbuy> GetQbuyList(string memberId)
        {
            List<Qbuy> list = new List<Qbuy>();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(OrderSqls.SELECT_QBUY_LIST_BY_MEMBER_ID, memberId);
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string startTime = dr["START_TIME"].ToString();
                    string endTime = dr["END_TIME"].ToString();
                    Qbuy qbuy = new Qbuy
                    {
                        qbuyCode = dr["QBUY_CODE"].ToString(),
                        storeId = dr["STORE_ID"].ToString(),
                        storeName = dr["STORE_NAME"].ToString(),
                        startTime = startTime,
                        endTime = endTime,
                        activeName = dr["REMARK"].ToString(),
                    };
                    list.Add(qbuy);
                }
            }

            return list;
        }

        public List<QBuyGoods> GetQbuyGoodsListByQbuyId(string qbuyCode)
        {
            List<QBuyGoods> list = new List<QBuyGoods>();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(OrderSqls.SELECT_QBUYGOODS_BY_QBUY_CODE, qbuyCode);
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    bool ifUse = false;
                    if (dr["IF_USE"].ToString() == "1")
                    {
                        int stock = 0;
                        int.TryParse(dr["GOODS_STOCK"].ToString(), out stock);
                        if (stock > 0)
                        {
                            ifUse = true;
                        }
                    }

                    QBuyGoods qBuyGoods = new QBuyGoods
                    {
                        qBuyGoodsId = dr["QBUY_GOODS_ID"].ToString(),
                        qBuyCode = dr["QBUY_CODE"].ToString(),
                        goodsId = dr["GOODS_ID"].ToString(),
                        goodsName = dr["GOODS_NAME"].ToString(),
                        price = dr["Q_PRICE"].ToString(),
                        num = dr["Q_NUM"].ToString(),
                        slt = dr["GOODS_IMG"].ToString(),
                        ifUse = ifUse,
                    };
                    list.Add(qBuyGoods);
                }
            }

            return list;
        }

        public QBuyGoods GetQbuyGoodsByQBuyIdAndQBuyGoodsId(string qBuyCode, string qBuyGoodsId)
        {
            QBuyGoods qBuyGoods = new QBuyGoods();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(OrderSqls.SELECT_QBUYGOODS_BY_QBUY_CODE_AND_QBUY_GOODS_ID, qBuyCode, qBuyGoodsId);
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt != null && dt.Rows.Count == 1)
            {
                qBuyGoods = new QBuyGoods
                {
                    qBuyGoodsId = dt.Rows[0]["QBUY_GOODS_ID"].ToString(),
                    qBuyCode = dt.Rows[0]["QBUY_CODE"].ToString(),
                    goodsId = dt.Rows[0]["GOODS_ID"].ToString(),
                    goodsName = dt.Rows[0]["GOODS_NAME"].ToString(),
                    price = dt.Rows[0]["Q_PRICE"].ToString(),
                    num = dt.Rows[0]["Q_NUM"].ToString(),
                    slt = dt.Rows[0]["GOODS_IMG"].ToString(),
                };
            }

            return qBuyGoods;
        }

        public bool updateQBuy(string qBuyCode, string orderCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(OrderSqls.UPDATE_QBUYGOODS_STATE, qBuyCode, orderCode);
            string sql = builder.ToString();
            return DatabaseOperationWeb.ExecuteDML(sql);
        }

    **************************************************************************/

        private class OrderSqls
        {

            public const string SELECT_T_ACTIVE_MEMBER_ALL_COUNT = "" +
                "SELECT COUNT(*) AS CNT " +
                "FROM T_ACTIVE_MEMBER";

            public const string SELECT_T_ACTIVE_MEMBER_BY_MEMBER_ID = "" +
                "SELECT *" +
                "FROM T_ACTIVE_MEMBER "+
                "WHERE MEMBER_ID = {0}";

            public const string INSERT_INTO_T_ACTIVE_MEMBER = "" +
                "INSERT " +
                "INTO T_ACTIVE_MEMBER " +
                "VALUES ({0}, {1}, {2}, '{3}', '{4}', '{5}')";

            public const string SELECT_T_ACTIVE_LIST_ALL = "" +
                "SELECT * " +
                "FROM T_ACTIVE_LIST";

            public const string SELECT_T_ACTIVE_INFO_ALL = "" +
                "SELECT * " +
                "FROM T_ACTIVE_INFO";

            public const string SELECT_T_ACTIVE_INFO_BY_ACTIVE_STAGE = "" +
                "SELECT * " +
                "FROM T_ACTIVE_INFO "+
                "WHERE ACTIVE_STAGE = {0}";

            public const string INSERT_INTO_T_ACTIVE_LIST = "" +
                "INSERT " +
                "INTO T_ACTIVE_LIST " +
                "VALUES ({0}, '{1}', '{2}')";

            public const string INSERT_INTO_T_ACTIVE_INFO = "" +
                "INSERT " +
                "INTO T_ACTIVE_INFO " +
                "VALUES ({0}, {1}, {2}, '{3}', '{4}', '{5}')";

            public const string SELECT_LEADERBOARD = "" +
                "SELECT * from v_max_allrec_money";

            public const string SELECT_TOTAL_ALLREC_MONEY = "" +
                "SELECT * from v_total_allrec_money";

            public const string SELECT_ADD_TEMP_ALLREC_MONEY = "" +
                "SELECT * FROM T_ACTIVE_TEMP_NUM WHERE ACTIVE_CODE =   '{0}'";


            /**********************************
            public const string SELECT_QBUY_LIST_BY_MEMBER_ID = ""
                + "SELECT BQ.* ,BA.REMARK ,S.STORE_NAME " +
                "FROM T_BUSS_QBUY BQ ,T_BUSS_ACTIVE BA,T_BASE_STORE S " +
                "WHERE BQ.ACTIVE_ID = BA.ACTIVE_ID " +
                "AND BQ.STORE_ID = S.STORE_ID " +
                "AND BQ.STATE = 0 " +
                "AND BQ.MEMBER_ID = '{0}' ";

            public const string SELECT_QBUYGOODS_BY_QBUY_CODE = ""
                + "SELECT G.GOODS_IMG, G.GOODS_NAME,QG.*,G.IF_USE,G.GOODS_STOCK " +
                "FROM T_BUSS_GOODS G ,T_BUSS_QBUY_GOODS QG " +
                "WHERE G.GOODS_ID = QG.GOODS_ID " +
                "AND QG.QBUY_CODE = '{0}'";

            public const string SELECT_QBUYGOODS_BY_QBUY_CODE_AND_QBUY_GOODS_ID = ""
                + "SELECT G.GOODS_IMG, G.GOODS_NAME,QG.* "
                + "FROM T_BUSS_GOODS G ,T_BUSS_QBUY_GOODS QG,T_BUSS_QBUY BQ "
                + "WHERE G.GOODS_ID = QG.GOODS_ID "
                + "AND QG.QBUY_CODE = BQ.QBUY_CODE "
                + "AND BQ.STATE = 0 "
                + "AND QG.QBUY_CODE = '{0}' AND QG.QBUY_GOODS_ID = {1}";

            public const string UPDATE_QBUYGOODS_STATE = ""
                + "UPDATE T_BUSS_QBUY SET STATE = 1,ORDER_CODE = '{1}' WHERE QBUY_CODE = '{0}' ";
                ************************************/
        }
    }
}
