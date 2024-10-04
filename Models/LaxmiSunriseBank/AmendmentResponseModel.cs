using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using static LaxmiSunriseBank.Models.LaxmiSunriseBank.AmendmentResponseModelXML;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{

    public class AmendmentResponse
    {
        public bool? IsSuccess { get; set; }
        public bool? TimeOut { get; set; }
        public string? URL { get; set; }
        public string? Response { get; set; }
        public AmendmentRequestResult AmendmentRequestResult { get; set; }
    }

    public class AmendmentResponseModelXML
    {
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {
            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body Body { get; set; }
        }

        public class Body
        {
            [XmlElement(ElementName = "AmendmentRequestResponse", Namespace = "ClientWebService")]
            public AmendmentRequestResponse AmendmentRequestResponse { get; set; }
        }

        public class AmendmentRequestResponse
        {
            [XmlElement(ElementName = "AmendmentRequestResult")]
            public AmendmentRequestResult AmendmentRequestResult { get; set; }
        }

        public class AmendmentRequestResult
        {
            [XmlElement(ElementName = "CODE")]
            public string Code { get; set; }

            [XmlElement(ElementName = "AGENT_SESSION_ID")]
            public string AgentSessionId { get; set; }

            [XmlElement(ElementName = "MESSAGE")]
            public string Message { get; set; }

            [XmlElement(ElementName = "PINNO")]
            public string PinNo { get; set; }
        }

    }
}
