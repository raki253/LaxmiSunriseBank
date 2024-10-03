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
        public SourceRequestModel()
        {
                
        }
        public string AgentCode
        {
            get { return AgentCode; } 
            set { AgentCode = "FED001"; }
        }
        
        public string UserId {
            get { return UserId; }  
            set { UserId = "FEDERAL1"; }
        }

  
        public string AgentSessionId {
            get { return AgentSessionId; }
            set { AgentSessionId = "1298709"; }
        }
      
        public string Signature
        {
            get { return AgentSessionId; }
            set { AgentSessionId = "7d56439f55beff0d0fcd50bc4d887ca24848b676963126a6d317bbb4f377f701"; }
        }
    }

    /// <summary>
    /// Source Request Model XML
    /// </summary>
    public class SourceRequestModelXML
    {
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {

            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body? Body { get; set; }
        }       

        public class Body
        {
            [XmlElement(ElementName = "GetEcho", Namespace = "ClientWebService")]
            public GetEcho? GetEcho { get; set; }

            [XmlElement(ElementName = "GetAgentList", Namespace = "ClientWebService")]
            public GetAgentList? GetAgentList { get; set; }

            [XmlElement(ElementName = "GetBankList", Namespace = "ClientWebService")]
            public GetBankList? GetBankList { get; set; }
        }


        public class GetEcho
        {
            [XmlElement(ElementName = "AGENT_CODE")]
            public string? AGENT_CODE { get; set; }

            [XmlElement(ElementName = "USER_ID")]
            public string? USER_ID { get; set; }

            [XmlElement(ElementName = "AGENT_SESSION_ID")]
            public string? AGENT_SESSION_ID { get; set; }

            [XmlElement(ElementName = "SIGNATURE")]
            public string? SIGNATURE { get; set; }
        }

        public class GetAgentList
        {
            [XmlElement(ElementName = "AGENT_CODE")]
            public string? AGENT_CODE { get; set; }

            [XmlElement(ElementName = "USER_ID")]
            public string? USER_ID { get; set; }

            [XmlElement(ElementName = "AGENT_SESSION_ID")]
            public string? AGENT_SESSION_ID { get; set; }

            [XmlElement(ElementName = "PAYMENTTYPE")]
            public string? PAYMENTTYPE { get; set; }

            [XmlElement(ElementName = "PAYOUT_COUNTRY")]
            public string? PAYOUT_COUNTRY { get; set; }

            [XmlElement(ElementName = "SIGNATURE")]
            public string? SIGNATURE { get; set; }
        }

        public class EchoRequestXMLModel
        {

            [XmlElement(ElementName = "AGENT_CODE")]
            public string AgentCode { get; set; }

            [XmlElement(ElementName = "USER_ID")]
            public string UserId { get; set; }

            [XmlElement(ElementName = "AGENT_SESSION_ID")]
            public string AgentSessionId { get; set; }

            [XmlElement(ElementName = "SIGNATURE")]
            public string SIGNATURE { get; set; }
        }

        public class GetBankList
        {
            [XmlElement(ElementName = "AGENT_CODE")]
            public string? AGENT_CODE { get; set; }

            [XmlElement(ElementName = "USER_ID")]
            public string? USER_ID { get; set; }

            [XmlElement(ElementName = "AGENT_SESSION_ID")]
            public string? AGENT_SESSION_ID { get; set; }

            [XmlElement(ElementName = "SIGNATURE")]
            public string? SIGNATURE { get; set; }
        }
    }
}
