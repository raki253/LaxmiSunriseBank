using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{
    public class SourceRequestModel
    {
        private string _agentCode;
        private string _userId;
        private string _agentSessionId;
        private string _signature;

        public SourceRequestModel()
        {
            _agentCode = "FED001";
            _userId = "FEDERAL1";
            _agentSessionId = "1298709";
            _signature = "7d56439f55beff0d0fcd50bc4d887ca24848b676963126a6d317bbb4f377f701";
        }
        public string AgentCode
        {
            get { return _agentCode; }
            set { _agentCode = value; }
        }
        
        public string UserId {
            get { return _userId; }
            set { _userId = value; }
        }

  
        public string AgentSessionId {
            get { return _agentSessionId; }
            set { _agentSessionId = value; }
        }
      
        public string Signature
        {
            get { return _signature; }
            set { _signature = value; }
        }
    }

    /// <summary>
    /// Source Request Model XML
    /// </summary>
    public class SourceRequestModelXML
    {
    //    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    //    public class Envelope
    //    {

    //        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    //        public Body? Body { get; set; }
    //    }       

    //    public class Body
    //    {
    //        [XmlElement(ElementName = "GetEcho", Namespace = "ClientWebService")]
    //        public GetEcho? GetEcho { get; set; }

    //        [XmlElement(ElementName = "GetAgentList", Namespace = "ClientWebService")]
    //        public GetAgentList? GetAgentList { get; set; }

    //        [XmlElement(ElementName = "GetBankList", Namespace = "ClientWebService")]
    //        public GetBankList? GetBankList { get; set; }

    //        [XmlElement(ElementName = "GetCurrentBalance", Namespace = "ClientWebService")]
    //        public GetCurrentBalanceRequest GetCurrentBalance { get; set; }

    //        [XmlElement(ElementName = "GetEXRate", Namespace = "ClientWebService")]
    //        public GetEXRateRequest GetEXRate { get; set; }

    //        [XmlElement(ElementName = "AmendmentRequest", Namespace = "ClientWebService")]
    //        public AmendmentRequest AmendmentRequest { get; set; }

    //    }


    //    public class GetEcho
    //    {
    //        [XmlElement(ElementName = "AGENT_CODE")]
    //        public string? AGENT_CODE { get; set; }

    //        [XmlElement(ElementName = "USER_ID")]
    //        public string? USER_ID { get; set; }

    //        [XmlElement(ElementName = "AGENT_SESSION_ID")]
    //        public string? AGENT_SESSION_ID { get; set; }

    //        [XmlElement(ElementName = "SIGNATURE")]
    //        public string? SIGNATURE { get; set; }
    //    }

    //    public class GetAgentList
    //    {
    //        [XmlElement(ElementName = "AGENT_CODE")]
    //        public string? AGENT_CODE { get; set; }

    //        [XmlElement(ElementName = "USER_ID")]
    //        public string? USER_ID { get; set; }

    //        [XmlElement(ElementName = "AGENT_SESSION_ID")]
    //        public string? AGENT_SESSION_ID { get; set; }

    //        [XmlElement(ElementName = "PAYMENTTYPE")]
    //        public string? PAYMENTTYPE { get; set; }

    //        [XmlElement(ElementName = "PAYOUT_COUNTRY")]
    //        public string? PAYOUT_COUNTRY { get; set; }

    //        [XmlElement(ElementName = "SIGNATURE")]
    //        public string? SIGNATURE { get; set; }
    //    }

    //    public class EchoRequestXMLModel
    //    {

    //        [XmlElement(ElementName = "AGENT_CODE")]
    //        public string AgentCode { get; set; }

    //        [XmlElement(ElementName = "USER_ID")]
    //        public string UserId { get; set; }

    //        [XmlElement(ElementName = "AGENT_SESSION_ID")]
    //        public string AgentSessionId { get; set; }

    //        [XmlElement(ElementName = "SIGNATURE")]
    //        public string SIGNATURE { get; set; }
    //    }

    //    public class GetBankList
    //    {
    //        [XmlElement(ElementName = "AGENT_CODE")]
    //        public string? AGENT_CODE { get; set; }

    //        [XmlElement(ElementName = "USER_ID")]
    //        public string? USER_ID { get; set; }

    //        [XmlElement(ElementName = "AGENT_SESSION_ID")]
    //        public string? AGENT_SESSION_ID { get; set; }

    //        [XmlElement(ElementName = "SIGNATURE")]
    //        public string? SIGNATURE { get; set; }
    //    }

    //    public class GetCurrentBalanceRequest
    //    {
    //        [XmlElement(ElementName = "AGENT_CODE")]
    //        public string AgentCode { get; set; }

    //        [XmlElement(ElementName = "USER_ID")]
    //        public string UserId { get; set; }

    //        [XmlElement(ElementName = "AGENT_SESSION_ID")]
    //        public string AgentSessionId { get; set; }

    //        [XmlElement(ElementName = "SIGNATURE")]
    //        public string Signature { get; set; }
    //    }

    //    public class GetEXRateRequest
    //    {
    //        [XmlElement(ElementName = "AGENT_CODE")]
    //        public string AgentCode { get; set; }

    //        [XmlElement(ElementName = "USER_ID")]
    //        public string UserId { get; set; }

    //        [XmlElement(ElementName = "AGENT_SESSION_ID")]
    //        public string AgentSessionId { get; set; }

    //        [XmlElement(ElementName = "TRANSFER_AMOUNT")]
    //        public string TransferAmount { get; set; }

    //        [XmlElement(ElementName = "PAYMENT_MODE")]
    //        public string PaymentMode { get; set; }

    //        [XmlElement(ElementName = "CALC_BY")]
    //        public string CalcBy { get; set; }

    //        [XmlElement(ElementName = "LOCATION_ID")]
    //        public string LocationId { get; set; }

    //        [XmlElement(ElementName = "PAYOUT_COUNTRY")]
    //        public string PayoutCountry { get; set; }

    //        [XmlElement(ElementName = "SIGNATURE")]
    //        public string Signature { get; set; }
    //    }

    //    public class AmendmentRequest
    //    {
    //        [XmlElement(ElementName = "AGENT_CODE")]
    //        public string AgentCode { get; set; }

    //        [XmlElement(ElementName = "USER_ID")]
    //        public string UserId { get; set; }

    //        [XmlElement(ElementName = "AGENT_SESSION_ID")]
    //        public string AgentSessionId { get; set; }

    //        [XmlElement(ElementName = "PINNO")]
    //        public string PinNo { get; set; }

    //        [XmlElement(ElementName = "AMENDMENT_FIELD")]
    //        public string AmendmentField { get; set; }

    //        [XmlElement(ElementName = "AMENDMENT_VALUE")]
    //        public string AmendmentValue { get; set; }

    //        [XmlElement(ElementName = "SIGNATURE")]
    //        public string Signature { get; set; }
    //    }
    }
}
