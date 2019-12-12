using ACBC.Common;
using ACBC.Dao;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACBC.Buss
{
    public class HomePageBuss : IBuss
    {
        public ApiType GetApiType()
        {
            return ApiType.HomePageApi;
        }

        public object Do_GetHomePage(BaseApi baseApi)
        {
            HomePageDao homePageDao = new HomePageDao();
            string memberId = Utils.GetMemberID(baseApi.token);
            var list = homePageDao.getHomePage();
            return list;
        }
        public object Do_GetExplainList(BaseApi baseApi)
        {
            HomePageDao homePageDao = new HomePageDao();
            var list = homePageDao.getExplainList();
            return list;
        }
        public object Do_GetActiveList(BaseApi baseApi)
        {
            HomePageDao homePageDao = new HomePageDao();
            var list = homePageDao.getActiveList();
            return list;
        }
        public object Do_GetGoodsListByPageNum(BaseApi baseApi)
        {
            GetGoodsListParam getGoodsListParam = JsonConvert.DeserializeObject<GetGoodsListParam>(baseApi.param.ToString());
            if (getGoodsListParam == null)
            {
                throw new ApiException(CodeMessage.InvalidParam, "InvalidParam");
            }
            ShowGoodsList list = Utils.GetCache<ShowGoodsList>(getGoodsListParam);
            if (list == null)
            {
                HomePageDao homePageDao = new HomePageDao();
                list = homePageDao.GetShowDayGoodsList(getGoodsListParam.pageNum);
                list.Unique = getGoodsListParam.GetUnique();
                Utils.SetCache(list, 1, 0, 0);
            }
            return list;
        }
        public object Do_GetGoodsImg(BaseApi baseApi)
        {
            CheckAsnGoodsParam checkAsnGoodsParam = JsonConvert.DeserializeObject<CheckAsnGoodsParam>(baseApi.param.ToString());
            if (checkAsnGoodsParam == null)
            {
                throw new ApiException(CodeMessage.InvalidParam, "InvalidParam");
            }
            HomePageDao homePageDao = new HomePageDao();
            var list = homePageDao.GetGoodsImg(checkAsnGoodsParam.goodsId);
            return list;
        }

        public object Do_GetActiveCheck(BaseApi baseApi)
        {
            HomePageDao homePageDao = new HomePageDao();
            var list = homePageDao.getActiveCheckListByActiveType();
            return list;
        }

        public object Do_GetActiveConsume(BaseApi baseApi)
        {
            HomePageDao homePageDao = new HomePageDao();
            var list = homePageDao.getActiveConsumeListByActiveType();
            return list;
        }
        public object Do_GetQBuyList(BaseApi baseApi)
        {
            HomePageDao homePageDao = new HomePageDao();
            var list = homePageDao.getQBuyList();
            return list;
        }
        public object Do_GetQBuyGoodsList(BaseApi baseApi)
        {
            QbuyParam qbuyParam = JsonConvert.DeserializeObject<QbuyParam>(baseApi.param.ToString());
            if (qbuyParam == null)
            {
                throw new ApiException(CodeMessage.InvalidParam, "InvalidParam");
            }
            HomePageDao homePageDao = new HomePageDao();
            var list = homePageDao.GetQbuyGoodsList(qbuyParam.qbuyCode);
            return list;
        }
        public object Do_GetShopList(BaseApi baseApi)
        {
            HomePageDao homePageDao = new HomePageDao();
            var list = homePageDao.getShopList();
            return list;
        }
        public object Do_AddLeek(BaseApi baseApi)
        {
            MemberParam param = JsonConvert.DeserializeObject<MemberParam>(baseApi.param.ToString());
            if (param == null)
            {
                throw new ApiException(CodeMessage.InvalidParam, "InvalidParam");
            }
            if (param.memberId == null || param.memberId == "")
            {
                throw new ApiException(CodeMessage.InterfaceValueError, "InterfaceValueError");
            }

            MemberDao memberDao = new MemberDao();
            string memberId = Utils.GetMemberID(baseApi.token);
            if (memberId == null)
            {
                string openId = Utils.GetOpenID(baseApi.token);
                memberDao.addLog("LEEK", openId, param.memberId);
                memberDao.addLeekLog(param.memberId, openId);
            }
            else
            {
                memberDao.addLog("LEEK", memberId, param.memberId);
                if (memberDao.checkMemberLeek(param.memberId, memberId) == "20005")
                {
                    throw new ApiException(CodeMessage.MemberLeekBindSameExists, "MemberLeekBindSameExists");
                }
                else if (memberDao.checkMemberLeek(param.memberId, memberId) == "20006")
                {
                    throw new ApiException(CodeMessage.MemberLeekBindOtherExists, "MemberLeekBindOtherExists");
                }

                if (!memberDao.addLeek(param.memberId, memberId))
                {
                    throw new ApiException(CodeMessage.MemberBindExists, "MemberBindExists");

                }
            }
            return "";
        }
        public object Do_HandleAccount(BaseApi baseApi)
        {
            MemberDao memberDao = new MemberDao();
            memberDao.getAccountSelectList();
            return "";
        }
    }
}
