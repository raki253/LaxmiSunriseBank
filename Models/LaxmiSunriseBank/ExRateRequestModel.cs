using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{
    public class ExRateRequestModel : SourceRequestModel
    {
        public string? TransferAmount { get; set; }

        public string? PaymentMode { get; set; }

        public string? CalcBy { get; set; }

        public string? LocationId { get; set; }

        public string? PayoutCountry { get; set; }
    }

    public class ExRateRequestModelXML
    {
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {

            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body? Body { get; set; }
        }

        public class Body
        {
            [XmlElement(ElementName = "GetEXRate", Namespace = "ClientWebService")]
            public GetEXRateRequest GetEXRate { get; set; }
        }

        public class GetEXRateRequest
        {
            [XmlElement(ElementName = "AGENT_CODE")]
            public string AgentCode { get; set; }

            [XmlElement(ElementName = "USER_ID")]
            public string UserId { get; set; }

            [XmlElement(ElementName = "AGENT_SESSION_ID")]
            public string AgentSessionId { get; set; }

            [XmlElement(ElementName = "TRANSFER_AMOUNT")]
            public string TransferAmount { get; set; }

            [XmlElement(ElementName = "PAYMENT_MODE")]
            public string PaymentMode { get; set; }

            [XmlElement(ElementName = "CALC_BY")]
            public string CalcBy { get; set; }

            [XmlElement(ElementName = "LOCATION_ID")]
            public string LocationId { get; set; }

            [XmlElement(ElementName = "PAYOUT_COUNTRY")]
            public string PayoutCountry { get; set; }

            [XmlElement(ElementName = "SIGNATURE")]
            public string Signature { get; set; }
        }
    }
}
