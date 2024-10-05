using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{
    public class EchoRequestModel : SourceRequestModel
    {
        public EchoRequestModel() :base() { }
        
    }

    public class EchoRequestModelXML
    {
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {

            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body? Body { get; set; }
        }

        public class Body
        {
            [XmlElement(ElementName = "GetEcho", Namespace = "ClientWebService")]
            public GetEcho? GetEcho { get; set; }
        }


        public class GetEcho
        {
            [XmlElement(ElementName = "AGENT_CODE")]
            public string? AgentCode { get; set; }

            [XmlElement(ElementName = "USER_ID")]
            public string? USerId { get; set; }

            [XmlElement(ElementName = "AGENT_SESSION_ID")]
            public string? AgentSessionId { get; set; }

            [XmlElement(ElementName = "SIGNATURE")]
            public string? Signature { get; set; }
        }
    }
}
