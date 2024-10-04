using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static LaxmiSunriseBank.Models.LaxmiSunriseBank.SourceRequestModelXML;
using System.Xml.Serialization;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{
    public class AgentListRequestModel : SourceRequestModel
    {
        [StringLength(1)]
        public string? PaymentType { get; set; }

        [StringLength(3)]
        public string? PayoutCountry { get; set; }
    }

    public class AgentListRequestModelXML
    {
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {
            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body? Body { get; set; }
        }

        public class Body
        {
            [XmlElement(ElementName = "GetAgentList", Namespace = "ClientWebService")]
            public GetAgentList? GetAgentList { get; set; }
        }

        public class GetAgentList
        {
            [XmlElement(ElementName = "AGENT_CODE")]
            public string? AgentCode { get; set; }

            [XmlElement(ElementName = "USER_ID")]
            public string? UserId { get; set; }

            [XmlElement(ElementName = "AGENT_SESSION_ID")]
            public string? AgentSessionId { get; set; }

            [XmlElement(ElementName = "PAYMENTTYPE")]
            public string? PaymentType { get; set; }

            [XmlElement(ElementName = "PAYOUT_COUNTRY")]
            public string? PayoutCountry { get; set; }

            [XmlElement(ElementName = "SIGNATURE")]
            public string? Signature { get; set; }
        }
    }
}
