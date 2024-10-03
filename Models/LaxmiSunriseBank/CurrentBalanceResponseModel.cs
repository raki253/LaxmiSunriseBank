using System;
using System.Collections.Generic;
using System.Text;
using static LaxmiSunriseBank.Models.LaxmiSunriseBank.BankListResponseModelXML;
using System.Xml.Serialization;
using static LaxmiSunriseBank.Models.LaxmiSunriseBank.CurrentBalanceResponseModelXML;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{

    public class CurrentBalanceResponseModel
    {
        public string? Code { get; set; }

        public string? AgentSessionId { get; set; }

        public string? AgentName { get; set; }

        public string? Country { get; set; }

        public string? CurrentBalance { get; set; }

        public string? Currency { get; set; }

        public string? ServiceCharge { get; set; }
    }

    public class CurrentBalanceResponse
    {
        public bool? IsSuccess { get; set; }
        public bool? TimeOut { get; set; }
        public string? URL { get; set; }
        public string? Response { get; set; }
        public GetCurrentBalanceResult GetCurrentBalanceResult { get; set; }
    }

    public class CurrentBalanceResponseModelXML
    {
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {
            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body Body { get; set; }
        }

        public class Body
        {
            [XmlElement("GetBankListResponse", Namespace = "ClientWebService")]
            public GetCurrentBalanceResponse GetCurrentBalanceResponse { get; set; }
        }

        public class GetCurrentBalanceResponse
        {
            [XmlElement(ElementName = "GetCurrentBalanceResult")]
            public GetCurrentBalanceResult GetCurrentBalanceResult { get; set; }
        }

        public class GetCurrentBalanceResult
        {
            [XmlElement(ElementName = "CODE")]
            public string Code { get; set; }

            [XmlElement(ElementName = "AGENT_SESSION_ID")]
            public string AgentSessionId { get; set; }

            [XmlElement(ElementName = "AGENT_NAME")]
            public string AgentName { get; set; }

            [XmlElement(ElementName = "COUNTRY")]
            public string Country { get; set; }

            [XmlElement(ElementName = "CURRENT_BALANCE")]
            public string CurrentBalance { get; set; }

            [XmlElement(ElementName = "CURRENCY")]
            public string Currency { get; set; }

            [XmlElement(ElementName = "SERVICE_CHARGE")]
            public string ServiceCharge { get; set; }
        }
    }
}
