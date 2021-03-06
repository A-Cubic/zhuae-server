﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACBC.Common
{
    /// <summary>
    /// 返回信息对照
    /// </summary>
    public enum CodeMessage
    {
        OK = 0,
        PostNull = -1,

        AppIDError = 201,
        SignError = 202,

        NotFound = 404,
        InnerError = 500,

        SenparcCode = 1000,

        PaymentError = 3000,
        PaymentTotalPriceZero = 3001,
        PaymentMsgError = 3002,

        InvalidToken = 4000,
        InvalidMethod = 4001,
        InvalidParam = 4002,
        InterfaceRole = 4003,
        InterfaceValueError = 4004,
        InterfaceDBError = 4005,
        NeedLogin = 4006,
        InvalidCode = 4007,

        MemberExist = 10001,
        MemberRegError = 10002,
        StoreMemberExist = 10003,
        RemoteStoreMemberNotExist = 10004,
        BindStoreMemberError = 10005,
        InvalidCheckCode = 10006,
        HandleCommitPointError = 10007,
        ExchangeHeartError = 10008,
        SetDefaultMemberStoreError = 10009,
        SmsCodeError = 10010,
        OverTheStoreLimit = 10011,
        StorePhoneExist = 10012,
        UpdateScanCodeError = 10013,

        HomeInitError = 10101,
        InvalidGoods = 10102,
        NotEnoughGoods = 10103,
        UpdateCartError = 10104,
        DeleteCartError = 10105,
        BindStoreFirst = 10106,
        InvalidPreOrderId = 10107,
        CreateOrderError = 10108,
        InvalidOrderState = 10109,
        NotEnoughHearts = 10110,
        PayForOrderError = 10111,
        InvalidStore = 10112,
        ErrorNum = 10113,

        RemoteStoreMemberExist = 10201,
        AddRemoteStoreMemberError = 10202,
        AddPointCommitError = 10203,
        UpdatePointCommitError = 10204,

        StoreUserExist = 10301,
        StoreUserRegError = 10302,
        InvalidStoreCode = 10303,
        InvalidOrderCode = 10304,
        PickupGoodsError = 10305,
        NotStoreUserOrder = 10306,
        InvalidExchangeCode = 10307,
        NeedStoreMember = 10308,
        ExchangeError = 10309,
        CheckAsnGoodsError = 10310,
        InvalidMemberCkeckStoreCode = 10311,
        MemberCkeckStoreError = 10312,
        AlreadyCheckThisStoreToday = 10313,

        MemberPhoneExistsByOneself = 20001,
        MemberPhoneExistsByOther = 20002,
        MemberPhoneError = 20003,
        ZEMemberPhoneExists = 20004,
        MemberLeekBindSameExists = 20005,
        MemberLeekBindOtherExists = 20006,
        MemberBindExists = 20007,
        ACCOUNTExists = 20008,
        ACCOUNTZEExists = 20009,
        UpdateResellerTypeError = 20010,

        NoActiveMemberError = 30001,
        ActiveJoinedError=30002,
    }
}
