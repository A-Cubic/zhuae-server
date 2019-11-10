using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ACBC.Common;
using ACBC.Dao;
using Newtonsoft.Json;
using Senparc.Weixin.WxOpen.Containers;

namespace ACBC.Buss
{
    public class MemberBuss : IBuss
    {
        public ApiType GetApiType()
        {
            return ApiType.MemberApi;
        }

        public object Do_GetMemberInfo(BaseApi baseApi)
        {
            MemberDao memberDao = new MemberDao();
            string memberId = Utils.GetMemberID(baseApi.token);
            return memberDao.GetMemberInfo(memberId);

        }

        public object Do_GetCardList(BaseApi baseApi)
        {
            MemberDao memberDao = new MemberDao();
            string memberId = Utils.GetMemberID(baseApi.token);
            List<GMember> list = memberDao.GetCardList(memberId);
            return list;

        }

        public object Do_CheckCode(BaseApi baseApi)
        {
            CheckCodeParam checkCodeParam = JsonConvert.DeserializeObject<CheckCodeParam>(baseApi.param.ToString());
            if (checkCodeParam == null)
            {
                throw new ApiException(CodeMessage.InvalidParam, "InvalidParam");
            }
            string tSms = Utils.GetCache<string>(baseApi.token);
            if (tSms != null)
            {
                throw new ApiException(CodeMessage.SmsCodeError, "SmsCodeError");
            }

            Utils.SetCache(baseApi.token, "sms", 0, 0, 30);

            string code = new Random().Next(999999).ToString().PadLeft(6, '0');
            SessionBag sessionBag = SessionContainer.GetSession(baseApi.token);
            SessionUser sessionUser = JsonConvert.DeserializeObject<SessionUser>(sessionBag.Name);
            if (sessionUser == null)
            {
                throw new ApiException(CodeMessage.InvalidToken, "InvalidToken");
            }
            //checkCodeParam.phone = "13644237400";
            sessionUser.checkCode = code;
            sessionUser.checkPhone = checkCodeParam.phone;
            sessionBag.Name = JsonConvert.SerializeObject(sessionUser);
            SessionContainer.Update(sessionBag.Key, sessionBag, new TimeSpan(1, 0, 0));
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(Global.SMS_CODE_URL, Global.SMS_CODE, Global.SMS_TPL, code, checkCodeParam.phone);
            string url = builder.ToString();
            string res = Utils.GetHttp(url);

            SmsCodeRes smsCodeRes = JsonConvert.DeserializeObject<SmsCodeRes>(res);
            if (smsCodeRes == null || smsCodeRes.error_code != 0)
            {
                throw new ApiException(CodeMessage.SmsCodeError, (smsCodeRes == null ? "SmsCodeError" : smsCodeRes.reason));
            }

            return "";
        }

        public object Do_AddPhone(BaseApi baseApi)
        {
            AddPhoneParam param = JsonConvert.DeserializeObject<AddPhoneParam>(baseApi.param.ToString());
            if (param == null)
            {
                throw new ApiException(CodeMessage.InvalidParam, "InvalidParam");
            }

            MemberDao memberDao = new MemberDao();
            string memberId = Utils.GetMemberID(baseApi.token);

            SessionBag sessionBag = SessionContainer.GetSession(baseApi.token);
            SessionUser sessionUser = JsonConvert.DeserializeObject<SessionUser>(sessionBag.Name);
            if (sessionUser == null)
            {
                throw new ApiException(CodeMessage.InvalidToken, "InvalidToken");
            }
            //if (sessionUser.checkCode != param.code ||
            //    sessionUser.checkPhone != param.userPhone)
            //{
            //    throw new ApiException(CodeMessage.InvalidCheckCode, "InvalidCheckCode");
            //}
            if (sessionUser.checkCode != param.code)
            {
                throw new ApiException(CodeMessage.InvalidCheckCode, "InvalidCheckCode");
            }
            sessionUser.checkCode = "";
            sessionUser.checkPhone = "";
            sessionBag.Name = JsonConvert.SerializeObject(sessionUser);
            SessionContainer.Update(sessionBag.Key, sessionBag, new TimeSpan(Global.SESSION_EXPIRY_H, Global.SESSION_EXPIRY_M, Global.SESSION_EXPIRY_S));
            string checkMemberId = memberDao.checkMemberPhone(param.userPhone, param.shopType);
            if (checkMemberId != "")
            {
                if (checkMemberId == memberId)
                {
                    throw new ApiException(CodeMessage.MemberPhoneExistsByOneself, "MemberPhoneExistsByOneself");
                }
                else
                {
                    throw new ApiException(CodeMessage.MemberPhoneExistsByOther, "MemberPhoneExistsByOther");
                }
            }

            if (!memberDao.checkSQLMemberPhone(param.userPhone))
            {
                throw new ApiException(CodeMessage.MemberPhoneError, "MemberPhoneError");
            }
            if (memberDao.addMemberPhone(memberId, param.userPhone, param.shopType))
            {
                throw new ApiException(CodeMessage.MemberBindExists, "MemberBindExists");
            }
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

            if (memberDao.checkMemberLeek(param.memberId, memberId) == "20005")
            {
                throw new ApiException(CodeMessage.MemberLeekBindSameExists, "MemberLeekBindSameExists");
            }
            else if (memberDao.checkMemberLeek(param.memberId, memberId) == "20006")
            {
                throw new ApiException(CodeMessage.MemberLeekBindOtherExists, "MemberLeekBindOtherExists");
            }

            if (memberDao.addLeek(param.memberId, memberId))
            {
                throw new ApiException(CodeMessage.MemberBindExists, "MemberBindExists");

            }
            return "";
        }
        public object Do_GetExchangeList(BaseApi baseApi)
        {
            MemberDao memberDao = new MemberDao();
            string memberId = Utils.GetMemberID(baseApi.token);
            List<GRechargeDetail> list = memberDao.getExchangeList(memberId);
            return list;

        }

        public object Do_GetRechargeList(BaseApi baseApi)
        {
            MemberDao memberDao = new MemberDao();
            string memberId = Utils.GetMemberID(baseApi.token);
            List<GRechargeDetail> list = memberDao.getRechargeList(memberId);
            return list;

        }

        public object Do_GetResellerImg(BaseApi baseApi)
        {
            MemberDao memberDao = new MemberDao();
            string memberId = Utils.GetMemberID(baseApi.token);
            return memberDao.getResellerImg(memberId);
        }
        public object Do_GetResellerLeek(BaseApi baseApi)
        {
            GetGoodsListParam getGoodsListParam = JsonConvert.DeserializeObject<GetGoodsListParam>(baseApi.param.ToString());
            if (getGoodsListParam == null)
            {
                throw new ApiException(CodeMessage.InvalidParam, "InvalidParam");
            }

            string memberId = Utils.GetMemberID(baseApi.token);
            MemberDao memberDao = new MemberDao();
            return memberDao.getLeekListByMemberIdAndPageNum(memberId, getGoodsListParam.pageNum);

        }
        public object Do_GetResellerAccount(BaseApi baseApi)
        {
            GetGoodsListParam getGoodsListParam = JsonConvert.DeserializeObject<GetGoodsListParam>(baseApi.param.ToString());
            if (getGoodsListParam == null)
            {
                throw new ApiException(CodeMessage.InvalidParam, "InvalidParam");
            }

            string memberId = Utils.GetMemberID(baseApi.token);
            MemberDao memberDao = new MemberDao();
            return memberDao.getAccountListByMemberIdAndPageNum(memberId, getGoodsListParam.pageNum);

        }

        public object Do_HandleAccount(BaseApi baseApi)
        {
            MemberDao memberDao = new MemberDao();


            memberDao.getAccountSelectList();
            return "";
        }
    }
}
