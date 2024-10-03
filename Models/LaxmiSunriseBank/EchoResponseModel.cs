using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using static LaxmiSunriseBank.Models.LaxmiSunriseBank.EchoResponseModelXML;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{
    public class EchoResponseModel : SourceResponse
    {
        public GetEchoResult GetEchoResult { get; set; }
    }

    public class EchoResponseModelXML
    {
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {
            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body Body { get; set; }
        }

        public class Body
        {
            [XmlElement(ElementName = "GetEchoResponse", Namespace = "ClientWebService")]
            public GetEchoResponse GetEchoResponse { get; set; }
        }

        public class GetEchoResponse
        {
            [XmlElement(ElementName = "GetEchoResult")]
            public GetEchoResult GetEchoResult { get; set; }
        }

        public class GetEchoResult
        {
            [XmlElement(ElementName = "CODE")]
            public string Code { get; set; }

            [XmlElement(ElementName = "MESSAGE")]
            public string Message { get; set; }
        }
    }

   
}
