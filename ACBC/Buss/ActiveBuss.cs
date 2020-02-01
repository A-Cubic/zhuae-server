using ACBC.Common;
using ACBC.Dao;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACBC.Buss
{
    public class ActiveBuss : IBuss
    {
        public ApiType GetApiType()
        {
            return ApiType.ActiveApi;
        }
        public object Do_init(BaseApi baseApi)
        {
            List<Object> res = new List<Object>();
            ActiveDao activeDao = new ActiveDao();
            string memberId = Utils.GetMemberID(baseApi.token);

            List<ActiveList> list = activeDao.GetActiveListAll();
            res.Add(list);

            int count = activeDao.GetMemberCount();
            res.Add(count);

            ActiveMember activeMember = activeDao.GetActiveMemberByMemberId(Convert.ToInt32(memberId));
            if (activeMember.memberId != 0) res.Add("true");
            else res.Add("false");

            return res;
        }


        /// <summary>
        /// 获取所有的活动列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public object Do_GetActiveList(BaseApi baseApi)
        {
            ActiveDao activeDao = new ActiveDao();
            List<ActiveList> list = activeDao.GetActiveListAll();
            Console.WriteLine(list.ToString());
            return list;
        }

        /// <summary>
        /// 获取所有参与活动的人数（一个人参加多个活动算一个还是多个？目前是多个）
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Do_GetActiveMemberCount(BaseApi baseApi)
        {
            ActiveDao activeDao = new ActiveDao();
            int count = activeDao.GetMemberCount();

            return count;
        }

        /// <summary>
        /// 通过阶段数获取活动信息
        /// </summary>
        /// <param name="param">stage：阶段数</param>
        /// <returns></returns>
        public ActiveInfo Do_GetActiveInfoByStage(BaseApi baseApi)
        {
            ActiveInfoParam activeParam = JsonConvert.DeserializeObject<ActiveInfoParam>(baseApi.param.ToString());

            if (activeParam == null || activeParam.stage == 0)
            {
                throw new ApiException(CodeMessage.InvalidParam, "InvalidParam");
            }
            string memberId = Utils.GetMemberID(baseApi.token);

            ActiveDao activeDao = new ActiveDao();
            ActiveInfo activeInfo = activeDao.GetActiveInfoByStage(activeParam.stage);

            return activeInfo;
        }

        /// <summary>
        /// 查看成员是否参加过本次活动
        /// </summary>
        /// <param name="param"></param>
        /// <returns>如参加，则返回true，相反，则返回false</returns>
        public string Do_ActiveMemberisExistByMemberId(BaseApi baseApi)
        {
            string memberId = Utils.GetMemberID(baseApi.token);

            ActiveDao activeDao = new ActiveDao();
            ActiveMember activeMember = activeDao.GetActiveMemberByMemberId(Convert.ToInt32(memberId));
            
            if (activeMember.memberId != 0) return "true";
            else return "false";
        }

        /// <summary>
        /// 添加 成员参加活动
        /// </summary>
        /// <param name="param">activeId:活动id</param>
        /// <returns>如添加成功，则返回true，相反，则返回false</returns>
        public string Do_AddActiveMember(BaseApi baseApi)
        {
            throw new ApiException(CodeMessage.ACCOUNTExists, "ACCOUNTExists");
            //ActiveMemberParam activeMemberParam = JsonConvert.DeserializeObject<ActiveMemberParam>(baseApi.param.ToString());

            //if (activeMemberParam == null)
            //{
            //    throw new ApiException(CodeMessage.InvalidParam, "InvalidParam");
            //}
            //string memberId = Utils.GetMemberID(baseApi.token);

            //MemberDao memberDao = new MemberDao();
            //ActiveMember activeMember = memberDao.getMemberByMemberId(memberId);
            //activeMember.activeId = activeMemberParam.activeId;
            //activeMember.createTime = new DateTime();

            //if (activeMember.memberId == 0) return "false";

            //ActiveDao activeDao = new ActiveDao();
            //bool res = activeDao.AddActiveMember(activeMember);

            //if (res) return "true";
            //else return "false";

        }

        /// <summary>
        /// 添加 活动列表
        /// </summary>
        /// <param name="param">activeId:活动id，activeName:活动名称，flag:备注</param>
        /// <returns>如添加成功，则返回true，相反，则返回false</returns>
        public string Do_AddActiveList(BaseApi baseApi)
        {
            ActiveList activeListParam = JsonConvert.DeserializeObject<ActiveList>(baseApi.param.ToString());

            if (activeListParam == null)
            {
                throw new ApiException(CodeMessage.InvalidParam, "InvalidParam");
            }

            ActiveDao activeDao = new ActiveDao();
            bool res = activeDao.addActiveList(activeListParam);

            if (res) return "true";
            else return "false";
        }

        /// <summary>
        /// 添加 活动信息
        /// </summary>
        /// <param name="param">activeId:活动id, activeStage:阶段, activeValue:活动值, activeText:活动文本,activeImg:活动图片</param>
        /// <returns>如添加成功，则返回true，相反，则返回false</returns>
        public string Do_AddActiveInfo(BaseApi baseApi)
        {
            ActiveInfo activeInfoParam = JsonConvert.DeserializeObject<ActiveInfo>(baseApi.param.ToString());

            if (activeInfoParam == null)
            {
                throw new ApiException(CodeMessage.InvalidParam, "InvalidParam");
            }

            ActiveDao activeDao = new ActiveDao();
            bool res = activeDao.addActiveInfo(activeInfoParam);

            if (res) return "true";
            else return "false";
        }

        /// <summary>
        /// 获取本次活动总金额（假）
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Do_getAllMoney(BaseApi baseApi)
        {
            ActiveDao activeDao = new ActiveDao();
            int mon = activeDao.getMoney();
               
            return mon;
        }

        public List<LeaderBoard> Do_getLeaderBoardList(BaseApi baseApi)
        {
            //ActiveDao activeDao = new ActiveDao();
            MangoDBHelp mangoDBHelp = new MangoDBHelp();
            //List<LeaderBoard> list = mangoDBHelp.GetLeaderBoardListAll();
            List<LeaderBoard> list = new List<LeaderBoard>();
            LeaderBoard l1 = new LeaderBoard();
            l1.nickname = "29Bru...";
            l1.money = "8800";
            list.Add(l1);
            LeaderBoard l2 = new LeaderBoard();
            l2.nickname = "油丶饼";
            l2.money = "4370";
            list.Add(l2);
            LeaderBoard l3 = new LeaderBoard();
            l3.nickname = "哈登MVP";
            l3.money = "4240";
            list.Add(l3);
            LeaderBoard l4 = new LeaderBoard();
            l4.nickname = "AvofM";
            l4.money = "3890";
            list.Add(l4);
            LeaderBoard l5 = new LeaderBoard();
            l5.nickname = "差不多的一生";
            l5.money = "3590";
            list.Add(l5);
            LeaderBoard l6 = new LeaderBoard();
            l6.nickname = "李贺唯";
            l6.money = "2890";
            list.Add(l6);
            return list;
        }

        /****************************
        public object Do_GetQbuyList(BaseApi baseApi)
        {
            ActiveDao activeDao = new ActiveDao();
            string memberId = Utils.GetMemberID(baseApi.token);
            var list = activeDao.GetQbuyList(memberId);
            return list;
        }

        public object Do_GetQbuyGoodsList(BaseApi baseApi)
        {
            QbuyParam qbuyParam = JsonConvert.DeserializeObject<QbuyParam>(baseApi.param.ToString());
            if (qbuyParam == null)
            {
                throw new ApiException(CodeMessage.InvalidParam, "InvalidParam");
            }
            ActiveDao activeDao = new ActiveDao();
            var list = activeDao.GetQbuyGoodsListByQbuyId(qbuyParam.qbuyCode);
            return list;
        }

        public object Do_StartQBuyGoods(BaseApi baseApi)
        {
            StartQBuyGoodsParam startQBuyGoodsParam = JsonConvert.DeserializeObject<StartQBuyGoodsParam>(baseApi.param.ToString());
            if (startQBuyGoodsParam == null)
            {
                throw new ApiException(CodeMessage.InvalidParam, "InvalidParam");
            }

            ActiveDao activeDao = new ActiveDao();
            var qBuyGoods = activeDao.GetQbuyGoodsByQBuyIdAndQBuyGoodsId(startQBuyGoodsParam.qBuyCode, startQBuyGoodsParam.qBuyGoodsId);

            PreOrder preOrder = new PreOrder();
            OrderDao orderDao = new OrderDao();
            MemberDao memberDao = new MemberDao();
            string memberId = Utils.GetMemberID(baseApi.token);
            Store store = orderDao.GetStoreByMemberId(memberId);
            if (store == null)
            {
                throw new ApiException(CodeMessage.BindStoreFirst, "BindStoreFirst");
            }
            preOrder.addr = store.storeAddr;

            string[] goodsIds = new string[1];
            goodsIds[0] = qBuyGoods.goodsId;

            List<Goods> goodsList = orderDao.GetGoodsByGoodsIds(goodsIds);

            int total = 0;
            List<PreOrderGoods> list = new List<PreOrderGoods>();
            foreach (Goods goods in goodsList)
            {
                if (qBuyGoods.goodsId != goods.goodsId)
                {
                    throw new ApiException(CodeMessage.InvalidGoods, "InvalidGoods");
                }
                if (Convert.ToInt32(qBuyGoods.num) < 0)
                {
                    throw new ApiException(CodeMessage.ErrorNum, "ErrorNum");
                }
                if (Convert.ToInt32(qBuyGoods.num) <= goods.goodsStock)
                {
                    total += Convert.ToInt32(qBuyGoods.price) * Convert.ToInt32(qBuyGoods.num);
                    PreOrderGoods preOrderGoods = new PreOrderGoods
                    {
                        goodsNum = Convert.ToInt32(qBuyGoods.num),
                        goodsId = goods.goodsId,
                        goodsImg = goods.goodsImg,
                        goodsName = goods.goodsName,
                        goodsPrice = Convert.ToInt32(qBuyGoods.price),
                    };
                    list.Add(preOrderGoods);
                }
                else
                {
                    throw new ApiException(CodeMessage.NotEnoughGoods, "NotEnoughGoods");
                }
            }
            preOrder.list = list;
            preOrder.total = total;
            preOrder.storeCode = store.storeCode;

            MemberInfo memberInfo = memberDao.GetMemberInfo(memberId);
            if (memberInfo.heart < Convert.ToInt32(total))
            {
                throw new ApiException(CodeMessage.NotEnoughHearts, "NotEnoughHearts");
            }

            string orderCode = preOrder.storeCode + memberId.PadLeft(6, '0') + DateTime.Now.ToString("yyyyMMddHHmmss");
            if (!activeDao.updateQBuy(startQBuyGoodsParam.qBuyCode, orderCode))
            {
                throw new ApiException(CodeMessage.PayForOrderError, "PayForOrderError");
            }
            if (!orderDao.InsertOrder(memberId, orderCode, preOrder, startQBuyGoodsParam.qBuyCode, preOrder.addr, 0))
            {
                throw new ApiException(CodeMessage.CreateOrderError, "CreateOrderError");
            }
            Order order = orderDao.GetOrderInfoByCode(orderCode);
            if (!orderDao.PayForOrder(memberId, order, memberInfo.heart))
            {
                throw new ApiException(CodeMessage.PayForOrderError, "PayForOrderError");
            }
            return "";
        }
    **************************************************/

    }
}
