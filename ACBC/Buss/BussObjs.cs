﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ACBC.Buss
{
    #region Sys

    public enum WsType
    {
        CHECK,
        ORDER,
        EXCHANGE,
    }

    public class BussCache
    {
        private string unique = "";
        public string Unique
        {
            get
            {
                return unique;
            }
            set
            {
                unique = value;
            }
        }
    }

    public class BussParam
    {
        public string GetUnique()
        {
            string needMd5 = "";
            string md5S = "";
            foreach (FieldInfo f in this.GetType().GetFields())
            {
                needMd5 += f.Name;
                needMd5 += f.GetValue(this).ToString();
            }
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(needMd5));
                var strResult = BitConverter.ToString(result);
                md5S = strResult.Replace("-", "");
            }
            return md5S;
        }
    }

    public class SessionUser
    {
        public string openid;
        public string checkPhone;
        public string checkCode;
        public string userType;
        public string memberId;
        public string storeUserId;
    }

    public class SmsCodeRes
    {
        public int error_code;
        public string reason;
    }

    public class WsPayState : BussCache
    {
        public WsType wsType;
    }

    public class WsPayStateParam : BussParam
    {
        public string scanCode;
    }

    public class ExchangeRes
    {
        public string reason;
        public ExchangeResult result;
        public int error_code;
    }

    public class ExchangeResult
    {
        public string update;
        public List<string[]> list;
    }

    public class ConfigItem
    {
        public string key;
        public string value;
    }

    public class ConfigParam
    {
        public string env;
        public string group;
    }

    public class RequestParam
    {
        public string method;
        public object param;
    }

    public class ResponseObj
    {
        public bool success;
        public ResponseMsg msg;
        public List<ConfigItem> data;
    }

    public class ResponseMsg
    {
        public string code;
        public string msg;
    }

    #endregion

    #region Params

    public class LoginParam
    {
        public string code;
    }

    public class MemberRegParam
    {
        public string avatarUrl;
        public string city;
        public string country;
        public string gender;
        public string language;
        public string nickName;
        public string province;
    }

    public class StoreUserRegParam
    {
        public string avatarUrl;
        public string city;
        public string country;
        public string gender;
        public string language;
        public string nickName;
        public string province;
        public string storeCode;
    }

    public class GetGoodsListParam : BussParam
    {
        public int pageNum;

    }

    public class GetGoodsParam : BussParam
    {
        public string goodsId;

    }

    public class InputCartParam
    {
        public string goodsId;
        public int goodsNum;
    }

    public class UpdateCartParam
    {
        public string cartId;
        public int goodsNum;
    }

    public class DeleteCartParam
    {
        public string cartId;
    }

    public class PreOrderParam
    {
        public string goodsId;
        public string cartId;
        public int goodsNum;
    }

    public class PayOrderParam
    {
        public string preOrderId;
        public string remark;
    }

    public class PayOrderParamV2
    {
        public string preOrderId;
        public string remark;
        public string storeBranchId;
        public string expAddr;
    }

    public class GetOrderInfoParam
    {
        public string orderId;
    }

    public class PayForOrderParam
    {
        public string orderId;
    }

    public class GetStoreInfoParam : BussParam
    {
        public string storeId;
    }

    public class SetDefaultMemberStoreParam
    {
        public string storeId;
    }

    public class CheckCodeParam
    {
        public string phone;
    }
    public class AddPhoneParam
    {
        public string userPhone;
        public string shopType;
        public string code;
    }

    public class BindStoreParam
    {
        public string phone;
        public string checkCode;
        public string storeId;
        public string cardCode;
    }

    public class GetRemoteStoreInfoParam
    {
        public string storeMemberId;
    }

    public class ExchangeHeartParam
    {
        public string storeMemberId;
        public int point;
    }

    public class AddMemberInfoParam
    {
        public string phone;
        public string point;
        public string cardCode;
    }

    public class AddPointRecordParam
    {
        public string phone;
        public string point;
    }

    public class UpdatePointCommitParam
    {
        public string pointCommitId;
    }

    public class GetStoreGoodsCodeParam
    {
        public string orderId;
    }

    public class ScanOrderCodeParam : BussParam
    {
        public string code;
    }

    public class PickupOrderGoodsParam
    {
        public string orderId;
        public string code;
    }

    public class ExchangeParam
    {
        public string code;
        public int score;
    }

    public class ScanExchangeCodeParam : BussParam
    {
        public string code;
    }

    public class CheckAsnGoodsParam
    {
        public string goodsId;
    }

    public class MemberCheckStoreParam
    {
        public string code;
        public int consume;
    }

    public class MemberCheckStoreCodeParam : BussParam
    {
        public string code;
    }

    public class QbuyParam
    {
        public string qbuyCode;
    }

    public class StartQBuyGoodsParam
    {
        public string qBuyCode;
        public string qBuyGoodsId;

    }
    public class ActiveTypeParam
    {
        public string activeType;
    }
    public class MemberParam
    {
        public string memberId;
    }
    public class AddResellerParam
    {
        public string name;
        public string userPhone;
    }
    public class ResellerTypeParam
    {
        public string resellerType;
        public string oldResellerType;
    }

    public class PaymentParam
    {
        public string id; //
        public string buyerName; //
        public string buyerAddr; //
        public string buyerPhone; //
    }
    #endregion

    #region DaoObjs

    public class Member 
    {
        public string memberName;
        public string memberId;
        public string openid;
        public string memberImg;
        public string memberPhone;
        public string memberSex;
        public string scanCode;
        public bool reseller;
    }

    public class StoreUser
    {
        public string storeId;
        public string storeUserName;
        public string storeUserId;
        public string openid;
        public string storeUserImg;
        public string storeUserPhone;
        public string storeUserSex;
    }

    public class Home : BussCache
    {
        public HomeInfo homeInfo;
        public List<HomeList> list;
    }

    public class HomeInfo
    {
        public string homeId;
        public string backgroundImg;
        public string titleMaster;
        public string titleSlaver;
        public string titleTagImg;
    }

    public class HomeList
    {
        public string img;
        public string title;
        public string price;
        public string praise;
        public string urlType;
        public string urlValue;
    }

    public class ShowDayList : BussCache
    {
        public List<int> monthList;
        public Dictionary<int, List<ShowDay>> dayList;
    }

    public class ShowDay
    {
        public string showId;
        public string showImg;
        public string showTitle;
        public string showDay;
        public string showMonth;
    }

    public class ShowGoods
    {
        public string goodsId;
        public string goodsImg;
        public string goodsTitle;
    }

    public class ShowGoodsList : BussCache
    {
        public List<ShowGoods> list;
    }

    public class OrderList
    {
        public List<Order> unPayList = new List<Order>();
        public List<Order> payList = new List<Order>();
        public List<Order> inStoreList = new List<Order>();
        public List<Order> doneList = new List<Order>();
    }

    public class Order
    {
        public string addr;
        public string orderId;
        public string orderCode;
        public string storeCode;
        public string total;
        public string state;
        public string orderTime;
        public string expFee;
        public List<OrderGoods> list = new List<OrderGoods>();
    }

    public class OrderGoods
    {
        public string goodsId;
        public string goodsImg;
        public string goodsName;
        public string price;
        public string goodsNum;
    }

    public class Goods : BussCache
    {
        public string goodsId;
        public string goodsName;
        public string goodsImg;
        public string goodsDesc;
        public int goodsPrice;
        public int goodsStock;
        public int sales;
        public List<GoodsImg> mainImgs = new List<GoodsImg>();
        public List<GoodsImg> infoImgs = new List<GoodsImg>();
    }

    public class GoodsImg
    {
        public string img;
        public int imgSort;
    }

    public class CartGoods
    {
        public string cartId;
        public string goodsId;
        public string goodsName;
        public string goodsImg;
        public int goodsPrice;
        public int goodsNum;
        public int goodsStock;
        public bool cartChecked;
        public bool edit;
    }

    public class PreOrder
    {
        public string preOrderId;
        public string storeCode;
        public string addr;
        public int total;
        public List<PreOrderGoods> list = new List<PreOrderGoods>();
    }

    public class PreOrderGoods
    {
        public string goodsId;
        public string goodsImg;
        public string goodsName;
        public int goodsPrice;
        public string cartId;
        public int goodsNum;

    }

    public class Store
    {
        public string storeId;
        public string storeImg;
        public string storeName;
        public string storeCode;
        public string storeDesc;
        public string storeAddr;
        public string storeTel;
        public string storeCardImg;
        public int storeRate;
        public int openReg = 0;
        public int canExp = 0;
        public int expFee = 0;
    }

    public class StoreList : BussCache
    {
        public List<Store> storeList = new List<Store>();
    }

    public class StoreListV2 : BussCache
    {
        public List<Store> storeList = new List<Store>();
    }

    public class StoreInfo : BussCache
    {
        public string storeId;
        public string storeImg;
        public string storeName;
        public string storeCode;
        public string storeDesc;
        public string storeAddr;
        public string storeTel;
        public string storeCardImg;
        public int storeRate;
        public List<StoreImg> storeImgs = new List<StoreImg>();
        public List<StoreGoods> storeGoods = new List<StoreGoods>();
    }

    public class StoreImg
    {
        public string storeId;
        public string storeImgId;
        public string storeImg;
    }

    public class StoreGoods
    {
        public string storeGoodsId;
        public string storeId;
        public string storeGoodsImg;
        public string storeGoodsName;
        public string storeGoodsDesc;
        public string storeGoodsImgOpen;
    }

    public class MemberStore
    {
        public string memberStoreId;
        public string storeId;
        public string memberId;
        public string regPhone;
        public string cardCode;
        public bool isDefault;
        public string bindDate;
        public string storeCardImg;
        public string storeAddr;
        public string storeName;
    }

    public class MemberInfo
    {
        public int phone = 0;
        public int point = 0;
        public int score = 0;
        public int price = 0;
    }

    public class RemoteStoreMember
    {
        public string storeMemberId;
        public string storeId;
        public string phone;
        public string regTime;
        public string cardCode;
        public int point;
        public int storeRate;
        public string storeName;
        public string storeImg;
        public int exchangeLimit;
        public int todayExchange;
    }

    public class RemotePointCommit
    {
        public string pointCommitId;
        public string storeId;
        public string phone;
        public string memberId;
        public string state;
        public string type;
        public int point;
    }

    public class AsnGoods
    {
        public string goodsId;
        public string goodsName;
        public string goodsImg;
        public string num;
    }

    public class StockGoods
    {
        public string goodsId;
        public string goodsName;
        public string goodsImg;
        public string num;
        public string lastUpdateTime;
    }

    public class StoreAccount
    {
        public string paymentDays;
        public string heartNum;
        public string state;
    }


    public class StoreGoodsCode : BussCache
    {
        public string code;
        public Order order;
    }

    public class ExchangeCode : BussCache
    {
        public string code;
        public string memberId;
    }

    public class MemberCheckStoreCode : BussCache
    {
        public string code;
        public string memberId;
    }
    public class Qbuy
    {
        public string qbuyId;
        public string qbuyCode;
        public string storeId;
        public string storeName;
        public string startTime;
        public string endTime;
        public string activeName;
    }
    public class QBuyGoods
    {
        public string qBuyGoodsId;
        public string qBuyCode;
        public string goodsId;
        public string goodsName;
        public string price;
        public string num;
        public string slt;
        public bool ifUse;
    }

    public class HomePage
    {
        public List<HomePageBanner> bannerList=new List<HomePageBanner>();
        public List<HomePageMenu> menuList= new List<HomePageMenu>();
        public HomePageBanner banner;
        public List<HomePageGoods> goodsList = new List<HomePageGoods>();
    }
    public class HomePageMenu
    {
        public string menuName;
        public string menuImg;
        public string menuUrl;
    }
    public class HomePageBanner
    {
        public string bannerImg;
        public string bannerUrl;
        public string urlType;
    }
    public class HomePageGoods
    {
        public string goodsUrlType;
        public string goodsUrl;
        public string goodsName;
        public string goodsText;
        public string goodsImg;
    }
    public class HomePageData
    {
        public string pageId;
        public string pageType;
        public string pageName;
        public string pageImg;
        public string pageUrl;
        public string urlType;
        public string sort;
        public string remark;
        public string flag;
    }
    public class Explain
    {
        public string explainUrl;
        public string explainTitle;
        public string explainImg;
    }
    public class Category
    {
        public List<string> parentList;
        public Dictionary<string, List<CategoryData>> categoryList;
    }
    public class CategoryData
    {
        public string categoryId;
        public string categoryName;
        public string categoryImg;
    }
    public class ActiveCheckInfo
    {
        public string activeId;
        public string activeStore;
        public string remark;
        public string activeState;
        public string activeType;
        public string activeTimeFrom;
        public string activeTimeTo;
        public string activeCheckId;
        public string itemNums;
        public string itemValue;
        public string valueType;
        public string storeName;
        public string activeImg;
    }
    public class ActiveConsumeInfo
    {
        public string activeId;
        public string activeStore;
        public string remark;
        public string activeState;
        public string activeType;
        public string activeTimeFrom;
        public string activeTimeTo;
        public string activeConsumeId;
        public string consume;
        public string itemNums;
        public string itemValue;
        public string valueType;
        public string storeName;
        public string activeImg;
    }
    public class QBuyInfo
    {
        public string activeId;
        public string activeStore;
        public string storeName;
        public string remark;
        public string activeState;
        public string activeType;
        public string activeTimeFrom;
        public string activeTimeTo;
        public string activeImg;
        public string activeQBuyId;
        public string beforeStart;
        public string lastDays;
        public string checkNum;
        public string consumeNum;
        public string minConsume;
    }
    public class ShopInfo
    {
        public string shopCode;
        public string shopName;
        public string shopImg;
        public string shopAddr;
        public string remark;
    }
    public class ResellerAccount
    {
        public string acount_date;
        public string member_id;
        public string member_name;
        public string phone;
        public double acount_price;
        public double reseller_price;
        public string reseller_type;
        public string state;
        public string createTime;
    }
    public class Leek
    {
        //public string memberId;
        //public string leekMemberId;
        public string leekName;
        public string createTime;
    }
    public class AccountInfo
    {
        public string accountDate;
        public string accountPhones;
    }
    public class ResellerType
    {
        public string resellerType;
        public bool senior;
        public bool buyGoods;
        public bool todayUpdate;
    }
    public class ResellerGoods
    {
        public string id;
        public string goodsId;
        public string barcode;
        public string goodsName;
        public string goodsImg;
        public string goodsPrice;
        public string goodsNum;
        public string goodsType;
    }

    public class PaymentResults
    {
        public string product;
        public string appId;
        public string timeStamp;
        public string nonceStr;
        public string package;
        public string paySign;
        public string billId;
    }

    public class PaymentDataResults
    {
        public string openId;
        public string billid;
        public string billPrice;
        public string billValue;
        public string bookingTime;
        public string bookingState;
        public string prePayId;
        public string payNo;
        public string refundFee;
        public string refundTime;
        public string formId;
    }
    public class Bill
    {
        public string id;
        public string billId;
        public string memberId;
        public string billTime;
        public string billState;
        public double billPrice;
        public string goodsId;
        public string barcode;
        public string goodsName;
        public string goodsImg;
        public int goods_num;
        public string buyerName;
        public string buyerPhone;
        public string buyerAddr;
        public string prePayId;
        public string prePayTime;
        public string payNo;
        public string payType;
        public string payTime;
        public string formId;
    }
    public class LeekNum
    {
        public string leekTotal;
        public string leekToday;
        public string leekActive;
    }
    public class ResellerTotal
    {
        public string total;
        public string monthTotal;
        public string incomeTotal;
    }

    public class ActiveList
    {
        public int activeId;
        public string activeName;
        public string flag;
    }
    public class ActiveMember
    {
        public int activeId;
        public int memberId;
        public string memberName;
        public string memberImg;
        public DateTime createTime;
    }
    public class LeaderBoard
    {
        public int id;
        public string nickname;
        public string money;
    }
    public class ActiveInfo
    {
        public int activeId;
        public string activeStage;
        public string activeValue;
        public string activeText;
        public string activeImg;
    }

    public class ActiveInfoParam
    {
        public int stage;
    }
    public class ActiveMemberParam
    {
        public int activeId;
    }
    public class SimpleAwardInfo
    {
        //public int id;
        public string active_day; //几月几号
        public int state;        //当天是否参加 0未参加，1已参加，2 已过期
        public List<string> goods;//
    }
    public class ExplicitAwardInfo
    { 
        public string active_day; //几月几号  
        public String heartState;        //心值的状态
        public int heart;            //心值数据
        public String coinState;        //硬币的状态
        public int coin;             //币数据
        public String goodstate;        //商品的状态
        public String good;          // 商品数据
    }
    public class RankingAwardInfo
    {
        //public int id;
        public string name; //用户名称
        public string award; //奖品信息
    }
    public class RandomAwardInfo
    {
        //public int id;
        public int heart;  //心值数据
        public int coin; //币数据
        public String good;// 商品数据（无商品为 ””）
    }


    #endregion
}
