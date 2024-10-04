using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{
    public class CancelTransactionRequestModel : SourceRequestModel
    {
        public string PinNo { get; set; }

        public string CancelReason { get; set; }

        public string TransactionId { get; set; }
    }

    public class CancelTransactionRequestModelXML
    {
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {

            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body? Body { get; set; }
        }

        public class Body
        {
            [XmlElement(ElementName = "CancelTransaction", Namespace = "ClientWebService")]
            public CancelTransaction CancelTransaction { get; set; }
        }

        public class CancelTransaction
        {
            [XmlElement(ElementName = "AGENT_CODE")]
            public string AgentCode { get; set; }

            [XmlElement(ElementName = "USER_ID")]
            public string UserId { get; set; }

            [XmlElement(ElementName = "PINNO")]
            public string PinNo { get; set; }

            [XmlElement(ElementName = "AGENT_SESSION_ID")]
            public string AgentSessionId { get; set; }

            [XmlElement(ElementName = "CANCEL_REASON")]
            public string CancelReason { get; set; }

            [XmlElement(ElementName = "SIGNATURE")]
            public string Signature { get; set; }
        }
    }

}
