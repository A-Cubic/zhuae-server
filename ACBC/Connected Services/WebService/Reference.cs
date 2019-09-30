//------------------------------------------------------------------------------
// <自动生成>
//     此代码由工具生成。
//     //
//     对此文件的更改可能导致不正确的行为，并在以下条件下丢失:
//     代码重新生成。
// </自动生成>
//------------------------------------------------------------------------------

namespace WebService
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GMember", Namespace="http://tempuri.org/")]
    public partial class GMember : object
    {
        
        private string ME_IDField;
        
        private int ME_ScoreField;
        
        private int ME_PointField;
        
        private string ME_MobileNumField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string ME_ID
        {
            get
            {
                return this.ME_IDField;
            }
            set
            {
                this.ME_IDField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int ME_Score
        {
            get
            {
                return this.ME_ScoreField;
            }
            set
            {
                this.ME_ScoreField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public int ME_Point
        {
            get
            {
                return this.ME_PointField;
            }
            set
            {
                this.ME_PointField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string ME_MobileNum
        {
            get
            {
                return this.ME_MobileNumField;
            }
            set
            {
                this.ME_MobileNumField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GRechargeDetail", Namespace="http://tempuri.org/")]
    public partial class GRechargeDetail : object
    {
        
        private string RD_GUIDField;
        
        private string RD_ME_IDField;
        
        private int RD_TYPEField;
        
        private int RD_MoneyField;
        
        private int RD_PointField;
        
        private int RD_GiveScoreField;
        
        private System.DateTime RD_TimeField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string RD_GUID
        {
            get
            {
                return this.RD_GUIDField;
            }
            set
            {
                this.RD_GUIDField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string RD_ME_ID
        {
            get
            {
                return this.RD_ME_IDField;
            }
            set
            {
                this.RD_ME_IDField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int RD_TYPE
        {
            get
            {
                return this.RD_TYPEField;
            }
            set
            {
                this.RD_TYPEField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=3)]
        public int RD_Money
        {
            get
            {
                return this.RD_MoneyField;
            }
            set
            {
                this.RD_MoneyField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=4)]
        public int RD_Point
        {
            get
            {
                return this.RD_PointField;
            }
            set
            {
                this.RD_PointField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=5)]
        public int RD_GiveScore
        {
            get
            {
                return this.RD_GiveScoreField;
            }
            set
            {
                this.RD_GiveScoreField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=6)]
        public System.DateTime RD_Time
        {
            get
            {
                return this.RD_TimeField;
            }
            set
            {
                this.RD_TimeField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WebService.ZEWebService1Soap")]
    public interface ZEWebService1Soap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetMemberInfo", ReplyAction="*")]
        System.Threading.Tasks.Task<WebService.GetMemberInfoResponse> GetMemberInfoAsync(WebService.GetMemberInfoRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetMemberPoint", ReplyAction="*")]
        System.Threading.Tasks.Task<WebService.GetMemberPointResponse> GetMemberPointAsync(WebService.GetMemberPointRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetGRechargeDetailInfo", ReplyAction="*")]
        System.Threading.Tasks.Task<WebService.GetGRechargeDetailInfoResponse> GetGRechargeDetailInfoAsync(WebService.GetGRechargeDetailInfoRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetMemberInfoRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetMemberInfo", Namespace="http://tempuri.org/", Order=0)]
        public WebService.GetMemberInfoRequestBody Body;
        
        public GetMemberInfoRequest()
        {
        }
        
        public GetMemberInfoRequest(WebService.GetMemberInfoRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetMemberInfoRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string phone;
        
        public GetMemberInfoRequestBody()
        {
        }
        
        public GetMemberInfoRequestBody(string phone)
        {
            this.phone = phone;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetMemberInfoResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetMemberInfoResponse", Namespace="http://tempuri.org/", Order=0)]
        public WebService.GetMemberInfoResponseBody Body;
        
        public GetMemberInfoResponse()
        {
        }
        
        public GetMemberInfoResponse(WebService.GetMemberInfoResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetMemberInfoResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public WebService.GMember[] GetMemberInfoResult;
        
        public GetMemberInfoResponseBody()
        {
        }
        
        public GetMemberInfoResponseBody(WebService.GMember[] GetMemberInfoResult)
        {
            this.GetMemberInfoResult = GetMemberInfoResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetMemberPointRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetMemberPoint", Namespace="http://tempuri.org/", Order=0)]
        public WebService.GetMemberPointRequestBody Body;
        
        public GetMemberPointRequest()
        {
        }
        
        public GetMemberPointRequest(WebService.GetMemberPointRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetMemberPointRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string phone;
        
        public GetMemberPointRequestBody()
        {
        }
        
        public GetMemberPointRequestBody(string phone)
        {
            this.phone = phone;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetMemberPointResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetMemberPointResponse", Namespace="http://tempuri.org/", Order=0)]
        public WebService.GetMemberPointResponseBody Body;
        
        public GetMemberPointResponse()
        {
        }
        
        public GetMemberPointResponse(WebService.GetMemberPointResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetMemberPointResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetMemberPointResult;
        
        public GetMemberPointResponseBody()
        {
        }
        
        public GetMemberPointResponseBody(string GetMemberPointResult)
        {
            this.GetMemberPointResult = GetMemberPointResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetGRechargeDetailInfoRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetGRechargeDetailInfo", Namespace="http://tempuri.org/", Order=0)]
        public WebService.GetGRechargeDetailInfoRequestBody Body;
        
        public GetGRechargeDetailInfoRequest()
        {
        }
        
        public GetGRechargeDetailInfoRequest(WebService.GetGRechargeDetailInfoRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetGRechargeDetailInfoRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string phone;
        
        public GetGRechargeDetailInfoRequestBody()
        {
        }
        
        public GetGRechargeDetailInfoRequestBody(string phone)
        {
            this.phone = phone;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetGRechargeDetailInfoResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetGRechargeDetailInfoResponse", Namespace="http://tempuri.org/", Order=0)]
        public WebService.GetGRechargeDetailInfoResponseBody Body;
        
        public GetGRechargeDetailInfoResponse()
        {
        }
        
        public GetGRechargeDetailInfoResponse(WebService.GetGRechargeDetailInfoResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetGRechargeDetailInfoResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public WebService.GRechargeDetail[] GetGRechargeDetailInfoResult;
        
        public GetGRechargeDetailInfoResponseBody()
        {
        }
        
        public GetGRechargeDetailInfoResponseBody(WebService.GRechargeDetail[] GetGRechargeDetailInfoResult)
        {
            this.GetGRechargeDetailInfoResult = GetGRechargeDetailInfoResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public interface ZEWebService1SoapChannel : WebService.ZEWebService1Soap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public partial class ZEWebService1SoapClient : System.ServiceModel.ClientBase<WebService.ZEWebService1Soap>, WebService.ZEWebService1Soap
    {
        
    /// <summary>
    /// 实现此分部方法，配置服务终结点。
    /// </summary>
    /// <param name="serviceEndpoint">要配置的终结点</param>
    /// <param name="clientCredentials">客户端凭据</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public ZEWebService1SoapClient(EndpointConfiguration endpointConfiguration) : 
                base(ZEWebService1SoapClient.GetBindingForEndpoint(endpointConfiguration), ZEWebService1SoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ZEWebService1SoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(ZEWebService1SoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ZEWebService1SoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(ZEWebService1SoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ZEWebService1SoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WebService.GetMemberInfoResponse> WebService.ZEWebService1Soap.GetMemberInfoAsync(WebService.GetMemberInfoRequest request)
        {
            return base.Channel.GetMemberInfoAsync(request);
        }
        
        public System.Threading.Tasks.Task<WebService.GetMemberInfoResponse> GetMemberInfoAsync(string phone)
        {
            WebService.GetMemberInfoRequest inValue = new WebService.GetMemberInfoRequest();
            inValue.Body = new WebService.GetMemberInfoRequestBody();
            inValue.Body.phone = phone;
            return ((WebService.ZEWebService1Soap)(this)).GetMemberInfoAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WebService.GetMemberPointResponse> WebService.ZEWebService1Soap.GetMemberPointAsync(WebService.GetMemberPointRequest request)
        {
            return base.Channel.GetMemberPointAsync(request);
        }
        
        public System.Threading.Tasks.Task<WebService.GetMemberPointResponse> GetMemberPointAsync(string phone)
        {
            WebService.GetMemberPointRequest inValue = new WebService.GetMemberPointRequest();
            inValue.Body = new WebService.GetMemberPointRequestBody();
            inValue.Body.phone = phone;
            return ((WebService.ZEWebService1Soap)(this)).GetMemberPointAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WebService.GetGRechargeDetailInfoResponse> WebService.ZEWebService1Soap.GetGRechargeDetailInfoAsync(WebService.GetGRechargeDetailInfoRequest request)
        {
            return base.Channel.GetGRechargeDetailInfoAsync(request);
        }
        
        public System.Threading.Tasks.Task<WebService.GetGRechargeDetailInfoResponse> GetGRechargeDetailInfoAsync(string phone)
        {
            WebService.GetGRechargeDetailInfoRequest inValue = new WebService.GetGRechargeDetailInfoRequest();
            inValue.Body = new WebService.GetGRechargeDetailInfoRequestBody();
            inValue.Body.phone = phone;
            return ((WebService.ZEWebService1Soap)(this)).GetGRechargeDetailInfoAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.ZEWebService1Soap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.ZEWebService1Soap12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpTransportBindingElement httpBindingElement = new System.ServiceModel.Channels.HttpTransportBindingElement();
                httpBindingElement.AllowCookies = true;
                httpBindingElement.MaxBufferSize = int.MaxValue;
                httpBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("找不到名称为“{0}”的终结点。", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.ZEWebService1Soap))
            {
                return new System.ServiceModel.EndpointAddress("http://139.196.101.142:9190/ZEWebService.asmx");
            }
            if ((endpointConfiguration == EndpointConfiguration.ZEWebService1Soap12))
            {
                return new System.ServiceModel.EndpointAddress("http://139.196.101.142:9190/ZEWebService.asmx");
            }
            throw new System.InvalidOperationException(string.Format("找不到名称为“{0}”的终结点。", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            ZEWebService1Soap,
            
            ZEWebService1Soap12,
        }
    }
}
