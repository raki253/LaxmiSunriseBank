using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{
    public class CurrentBalanceRequestModel : SourceRequestModel
    {

    }

    public class CurrentBalanceRequestModelXML
    {
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {

            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body? Body { get; set; }
        }

        public class Body
        {
            [XmlElement(ElementName = "GetCurrentBalance", Namespace = "ClientWebService")]
            public GetCurrentBalanceRequest GetCurrentBalance { get; set; }
        }

        public class GetCurrentBalanceRequest
        {
            [XmlElement(ElementName = "AGENT_CODE")]
            public string AgentCode { get; set; }

            [XmlElement(ElementName = "USER_ID")]
            public string UserId { get; set; }

            [XmlElement(ElementName = "AGENT_SESSION_ID")]
            public string AgentSessionId { get; set; }

            [XmlElement(ElementName = "SIGNATURE")]
            public string Signature { get; set; }
        }
    }
}
