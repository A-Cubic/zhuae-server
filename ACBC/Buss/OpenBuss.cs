﻿using ACBC.Common;
using ACBC.Dao;
using Newtonsoft.Json;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;
using Senparc.Weixin.WxOpen.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACBC.Buss
{
    public class OpenBuss : IBuss
    {
        public ApiType GetApiType()
        {
            return ApiType.OpenApi;
        }

        public object Do_Login(BaseApi baseApi)
        {
            LoginParam loginParam = JsonConvert.DeserializeObject<LoginParam>(baseApi.param.ToString());
            if (loginParam == null)
            {
                throw new ApiException(CodeMessage.InvalidParam, "InvalidParam");
            }

            var jsonResult = SnsApi.JsCode2Json(Global.APPID, Global.APPSECRET, loginParam.code);
            if (jsonResult.errcode == Senparc.Weixin.ReturnCode.请求成功)
            {
                AccessTokenContainer.RegisterAsync(Global.APPID, Global.APPSECRET);
                var sessionBag = SessionContainer.UpdateSession(null, jsonResult.openid, jsonResult.session_key, jsonResult.unionid);

                OpenDao openDao = new OpenDao();
                SessionUser sessionUser = new SessionUser();
                
                Member member = openDao.GetMember(Utils.GetOpenID(sessionBag.Key));
                if(member == null)
                {
                    sessionUser.userType = "GUEST";
                    sessionBag.Name = JsonConvert.SerializeObject(sessionUser);
                    SessionContainer.Update(sessionBag.Key, sessionBag, new TimeSpan(Global.SESSION_EXPIRY_H, Global.SESSION_EXPIRY_M, Global.SESSION_EXPIRY_S));
                    return new { token = sessionBag.Key, isReg = false };
                }
                else
                {
                    sessionUser.userType = "MEMBER";
                    sessionUser.openid = sessionBag.OpenId;
                    sessionUser.memberId = member.memberId;
                    sessionBag.Name = JsonConvert.SerializeObject(sessionUser);
                    SessionContainer.Update(sessionBag.Key, sessionBag, new TimeSpan(Global.SESSION_EXPIRY_H, Global.SESSION_EXPIRY_M, Global.SESSION_EXPIRY_S));
                    return new {
                        token = sessionBag.Key,
                        isReg = true,
                        member.memberId,
                        member.memberName,
                        member.memberImg,
                        member.memberPhone,
                        member.memberSex,
                        member.scanCode,
                        member.reseller
                    };
                }
            }
            else
            {
                throw new ApiException(CodeMessage.SenparcCode, jsonResult.errmsg);
            }
        }

        public object Do_MemberReg(BaseApi baseApi)
        {
            MemberRegParam memberRegParam = JsonConvert.DeserializeObject<MemberRegParam>(baseApi.param.ToString());
            if (memberRegParam == null)
            {
                throw new ApiException(CodeMessage.InvalidParam, "InvalidParam");
            }

            SessionBag sessionBag = SessionContainer.GetSession(baseApi.token);
            if (sessionBag == null)
            {
                throw new ApiException(CodeMessage.InvalidToken, "InvalidToken");
            }

            OpenDao openDao = new OpenDao();
            string openID = Utils.GetOpenID(baseApi.token);
            var member = openDao.GetMember(openID);

            if (member != null)
            {
                throw new ApiException(CodeMessage.MemberExist, "MemberExist");
            }

            if (!openDao.MemberReg(memberRegParam, openID))
            {
                throw new ApiException(CodeMessage.MemberRegError, "MemberRegError");
            }
            member = openDao.GetMember(openID);
            SessionUser sessionUser = JsonConvert.DeserializeObject<SessionUser>(sessionBag.Name);
            sessionUser.openid = sessionBag.OpenId;
            sessionUser.memberId = member.memberId;
            sessionUser.userType = "MEMBER";
            sessionBag.Name = JsonConvert.SerializeObject(sessionUser);
            SessionContainer.Update(sessionBag.Key, sessionBag, new TimeSpan(Global.SESSION_EXPIRY_H, Global.SESSION_EXPIRY_M, Global.SESSION_EXPIRY_S));
            openDao.checkLeekLog(openID, member.memberId);
            return "";
        }

        public object Do_ShopUserLogin(BaseApi baseApi)
        {
            LoginParam loginParam = JsonConvert.DeserializeObject<LoginParam>(baseApi.param.ToString());
            if (loginParam == null)
            {
                throw new ApiException(CodeMessage.InvalidParam, "InvalidParam");
            }

            var jsonResult = SnsApi.JsCode2Json(Global.STOREAPPID, Global.STOREAPPSECRET, loginParam.code);
            if (jsonResult.errcode == Senparc.Weixin.ReturnCode.请求成功)
            {
                AccessTokenContainer.RegisterAsync(Global.STOREAPPID, Global.STOREAPPSECRET);
                var sessionBag = SessionContainer.UpdateSession(null, jsonResult.openid, jsonResult.session_key, jsonResult.unionid);

                OpenDao openDao = new OpenDao();
                SessionUser sessionUser = new SessionUser();

                StoreUser storeUser = openDao.GetStoreUser(Utils.GetOpenID(sessionBag.Key));
                if (storeUser == null)
                {
                    sessionUser.userType = "UNKWON";
                    sessionBag.Name = JsonConvert.SerializeObject(sessionUser);
                    SessionContainer.Update(sessionBag.Key, sessionBag, new TimeSpan(Global.SESSION_EXPIRY_H, Global.SESSION_EXPIRY_M, Global.SESSION_EXPIRY_S));
                    return new { token = sessionBag.Key, isReg = false };
                }
                else
                {
                    sessionUser.userType = "STORE";
                    sessionUser.openid = sessionBag.OpenId;
                    sessionUser.storeUserId = storeUser.storeUserId;
                    sessionBag.Name = JsonConvert.SerializeObject(sessionUser);
                    SessionContainer.Update(sessionBag.Key, sessionBag, new TimeSpan(Global.SESSION_EXPIRY_H, Global.SESSION_EXPIRY_M, Global.SESSION_EXPIRY_S));
                    return new
                    {
                        token = sessionBag.Key,
                        isReg = true,
                        storeUser.storeId,
                        storeUser.storeUserId,
                        storeUser.storeUserName,
                        storeUser.storeUserImg,
                        storeUser.storeUserPhone,
                        storeUser.storeUserSex
                    };
                }
            }
            else
            {
                throw new ApiException(CodeMessage.SenparcCode, jsonResult.errmsg);
            }
        }

        public object Do_ShopUserReg(BaseApi baseApi)
        {
            StoreUserRegParam storeUserRegParam = JsonConvert.DeserializeObject<StoreUserRegParam>(baseApi.param.ToString());
            if (storeUserRegParam == null)
            {
                throw new ApiException(CodeMessage.InvalidParam, "InvalidParam");
            }

            SessionBag sessionBag = SessionContainer.GetSession(baseApi.token);
            if (sessionBag == null)
            {
                throw new ApiException(CodeMessage.InvalidToken, "InvalidToken");
            }

            OpenDao openDao = new OpenDao();
            string openID = Utils.GetOpenID(baseApi.token);
            StoreUser storeUser = openDao.GetStoreUser(openID);

            if (storeUser != null)
            {
                throw new ApiException(CodeMessage.StoreUserExist, "StoreUserExist");
            }

            string storeId = openDao.GetStoreId(storeUserRegParam.storeCode);
            if(storeId == "")
            {
                throw new ApiException(CodeMessage.InvalidStoreCode, "InvalidStoreCode");
            }

            if (!openDao.StoreUserReg(storeUserRegParam, openID, storeId))
            {
                throw new ApiException(CodeMessage.StoreUserRegError, "StoreUserRegError");
            }
            storeUser = openDao.GetStoreUser(openID);
            SessionUser sessionUser = JsonConvert.DeserializeObject<SessionUser>(sessionBag.Name);
            sessionUser.openid = sessionBag.OpenId;
            sessionUser.storeUserId = storeUser.storeUserId;
            sessionUser.userType = "STORE";
            sessionBag.Name = JsonConvert.SerializeObject(sessionUser);
            SessionContainer.Update(sessionBag.Key, sessionBag, new TimeSpan(Global.SESSION_EXPIRY_H, Global.SESSION_EXPIRY_M, Global.SESSION_EXPIRY_S));

            return "";
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

    }
}
