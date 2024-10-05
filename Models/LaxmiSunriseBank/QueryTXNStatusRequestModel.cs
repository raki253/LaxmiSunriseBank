using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{
    public class QueryTXNStatusRequestModel
    {
        public string AgentCode { get; set; }

        public string UserId { get; set; }

        public string PinNo { get; set; }

        public string AgentSessionId { get; set; }

        public string AgentTxnId { get; set; }

        public string Signature { get; set; }
    }


    public class QueryTXNStatusRequestModelXML
    {
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {
            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body? Body { get; set; }
        }

        public class Body
        {
            [XmlElement(ElementName = "QueryTXNStatus", Namespace = "ClientWebService")]
            public QueryTXNStatusRequest QueryTXNStatus { get; set; }
        }

        public class QueryTXNStatusRequest
        {
            [XmlElement(ElementName = "AGENT_CODE")]
            public string AgentCode { get; set; }

            [XmlElement(ElementName = "USER_ID")]
            public string UserId { get; set; }

            [XmlElement(ElementName = "PINNO")]
            public string PinNo { get; set; }

            [XmlElement(ElementName = "AGENT_SESSION_ID")]
            public string AgentSessionId { get; set; }

            [XmlElement(ElementName = "AGENT_TXNID")]
            public string AgentTxnId { get; set; }

            [XmlElement(ElementName = "SIGNATURE")]
            public string Signature { get; set; }
        }
    }

}
