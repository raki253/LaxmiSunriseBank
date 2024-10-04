using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using static LaxmiSunriseBank.Models.LaxmiSunriseBank.BankListResponseModelXML;
using static LaxmiSunriseBank.Models.LaxmiSunriseBank.ExRateResponseModelXML;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{

    public class ExRateResponse
    {
        public bool? IsSuccess { get; set; }
        public bool? TimeOut { get; set; }
        public string? URL { get; set; }
        public string? Response { get; set; }
        public GetEXRateResult GetEXRateResult { get; set; }
    }

    public class ExRateResponseModel
    {
        public string? Code { get; set; }

        public string? AgentSessionId { get; set; }

        public string? Message { get; set; }

        public string? CollectAmount { get; set; }

        public string? CollectCurrency { get; set; }

        public string? ServiceCharge { get; set; }

        public string? ExchangeRate { get; set; }

        public string? PayoutAmt { get; set; }

        public string? PayoutCurrency { get; set; }

        public string? ExRateSessionId { get; set; }

        public string? SettlementRate { get; set; }
    }

    public class ExRateResponseModelXML
    {
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {
            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body Body { get; set; }
        }

        public class Body
        {
            [XmlElement("GetEXRateResponse", Namespace = "ClientWebService")]
            public GetEXRateResponse GetEXRateResponse { get; set; }
        }

        public class GetEXRateResponse
        {
            [XmlElement(ElementName = "GetEXRateResult")]
            public GetEXRateResult GetEXRateResult { get; set; }
        }

        public class GetEXRateResult
        {
            [XmlElement(ElementName = "CODE")]
            public string Code { get; set; }

            [XmlElement(ElementName = "AGENT_SESSION_ID")]
            public string AgentSessionId { get; set; }

            [XmlElement(ElementName = "MESSAGE")]
            public string Message { get; set; }

            [XmlElement(ElementName = "COLLECT_AMT")]
            public string CollectAmount { get; set; }

            [XmlElement(ElementName = "COLLECT_CURRENCY")]
            public string CollectCurrency { get; set; }

            [XmlElement(ElementName = "SERVICE_CHARGE")]
            public string ServiceCharge { get; set; }

            [XmlElement(ElementName = "EXCHANGE_RATE")]
            public string ExchangeRate { get; set; }

            [XmlElement(ElementName = "PAYOUTAMT")]
            public string PayoutAmount { get; set; }

            [XmlElement(ElementName = "PAYOUTCURRENCY")]
            public string PayoutCurrency { get; set; }

            [XmlElement(ElementName = "EXRATE_SESSION_ID")]
            public string ExRateSessionId { get; set; }

            [XmlElement(ElementName = "SETTLEMENT_RATE")]
            public string SettlementRate { get; set; }
        }
    }
}
