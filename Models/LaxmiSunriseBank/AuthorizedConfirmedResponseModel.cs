using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using static LaxmiSunriseBank.Models.LaxmiSunriseBank.AuthorizedConfirmedResponseModelXML;
using static LaxmiSunriseBank.Models.LaxmiSunriseBank.CurrentBalanceResponseModelXML;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{
    public class AuthorizedConfirmedResponseModel
    {
        public bool? IsSuccess { get; set; }
        public bool? TimeOut { get; set; }
        public string? URL { get; set; }
        public string? Response { get; set; }
        public AuthorisedConfirmDetails GetCurrentBalanceResult { get; set; }
    }


    public class AuthorizedConfirmedResponseModelXML
    {
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {
            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body Body { get; set; }
        }

        public class Body
        {
            [XmlElement("Authorized_ConfirmedResponse", Namespace = "ClientWebService")]
            public AuthorizedConfirmResponse AuthorizedConfirmResponse { get; set; }
        }

        public class AuthorizedConfirmResponse
        {
            [XmlElement(ElementName = "Authorized_ConfirmedResult")]
            public AuthorisedConfirmDetails AuthorisedConfirmDetails { get; set; }
        }

        public class AuthorisedConfirmDetails
        {
            [XmlElement(ElementName = "CODE")]
            public string Code { get; set; }

            [XmlElement(ElementName = "PINNO")]
            public string PinNo { get; set; }

            [XmlElement(ElementName = "MESSAGE")]
            public string Message { get; set; }

            [XmlElement(ElementName = "SENDER_NAME")]
            public string SenderName { get; set; }

            [XmlElement(ElementName = "PAYOUT_AMT")]
            public string PayoutAmt { get; set; }

            [XmlElement(ElementName = "PAYOUT_CURRENCY")]
            public string PayoutCurrency { get; set; }

            [XmlElement(ElementName = "AGENT_SESSION_ID")]
            public string AgentSessionId { get; set; }
        }
    }
}
