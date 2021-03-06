﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACBC.Common
{
    /// <summary>
    /// API类型分组
    /// </summary>
    public enum ApiType
    {
        OpenApi,
        UploadApi,
        MallApi,
        MemberApi,
        OrderApi,
        RemoteApi,
        StoreApi,
        ActiveApi,
        HomePageApi,
        PaymentApi,
    }

    public enum CheckType
    {
        Open,
        Token,
        OpenToken,
        Sign,
    }

    public enum InputType
    {
        Header,
        Body,
    }

    public abstract class BaseApi
    {
        public string appId;
        public string lang;
        public string code;
        public string method;
        public string token;
        public string sign;
        public string nonceStr;
        public object param;

        public abstract CheckType GetCheckType();
        public abstract ApiType GetApiType();
        public abstract InputType GetInputType();

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{2}; method:{0}; param:{1}", method, param, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            string rets = builder.ToString();
            return rets;
        }
    }

    /// <summary>
    /// Upload类API
    /// </summary>
    public class UploadApi : BaseApi
    {
        public override CheckType GetCheckType()
        {
            return CheckType.Open;
        }

        public override InputType GetInputType()
        {
            return InputType.Header;
        }

        public override ApiType GetApiType()
        {
            return ApiType.UploadApi;
        }

    }

    /// <summary>
    /// 完全开放
    /// </summary>
    public class OpenApi : BaseApi
    {
        public override CheckType GetCheckType()
        {
            return CheckType.Open;
        }

        public override InputType GetInputType()
        {
            return InputType.Body;
        }

        public override ApiType GetApiType()
        {
            return ApiType.OpenApi;
        }

    }

    public class MallApi : BaseApi
    {
        public override CheckType GetCheckType()
        {
            return CheckType.OpenToken;
        }

        public override InputType GetInputType()
        {
            return InputType.Body;
        }

        public override ApiType GetApiType()
        {
            return ApiType.MallApi;
        }

    }

    public class MemberApi : BaseApi
    {
        public override CheckType GetCheckType()
        {
            return CheckType.Token;
        }

        public override InputType GetInputType()
        {
            return InputType.Body;
        }

        public override ApiType GetApiType()
        {
            return ApiType.MemberApi;
        }

    }

    public class OrderApi : BaseApi
    {
        public override CheckType GetCheckType()
        {
            return CheckType.Token;
        }

        public override InputType GetInputType()
        {
            return InputType.Body;
        }

        public override ApiType GetApiType()
        {
            return ApiType.OrderApi;
        }

    }

    public class RemoteApi : BaseApi
    {
        public override CheckType GetCheckType()
        {
            return CheckType.Sign;
        }

        public override InputType GetInputType()
        {
            return InputType.Body;
        }

        public override ApiType GetApiType()
        {
            return ApiType.RemoteApi;
        }

    }

    public class StoreApi : BaseApi
    {
        public override CheckType GetCheckType()
        {
            return CheckType.Token;
        }

        public override InputType GetInputType()
        {
            return InputType.Body;
        }

        public override ApiType GetApiType()
        {
            return ApiType.StoreApi;
        }

    }


    public class ActiveApi : BaseApi
    {
        public override CheckType GetCheckType()
        {
            return CheckType.Token;
        }

        public override InputType GetInputType()
        {
            return InputType.Body;
        }

        public override ApiType GetApiType()
        {
            return ApiType.ActiveApi;
        }

    }

    public class HomePageApi : BaseApi
    {
        public override CheckType GetCheckType()
        {
            return CheckType.OpenToken;
        }

        public override InputType GetInputType()
        {
            return InputType.Body;
        }

        public override ApiType GetApiType()
        {
            return ApiType.HomePageApi;
        }

    }
    /// <summary>
    /// 完全开放
    /// </summary>
    public class PaymentApi : BaseApi
    {
        public override CheckType GetCheckType()
        {
            return CheckType.Open;
        }

        public override InputType GetInputType()
        {
            return InputType.Body;
        }

        public override ApiType GetApiType()
        {
            return ApiType.PaymentApi;
        }
    }
}
