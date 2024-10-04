using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{
    public  class AuthorizedConfirmedRequestModel : SourceRequestModel
    {
        public string PinNo { get; set; }
    }


    public class AuthorizedConfirmedRequestModelXML
    {
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {

            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body? Body { get; set; }
        }

        public class Body
        {
            [XmlElement(ElementName = "Authorized_Confirmed", Namespace = "ClientWebService")]
            public AuthorizedConfirmModel AuthorizedConfirmModel { get; set; }
        }

        public class AuthorizedConfirmModel
        {

            [XmlElement(ElementName = "AGENT_CODE")]
            public string AgentCode { get; set; }

            [XmlElement(ElementName = "AGENT_SESSION_ID")]
            public string AgentSessionId { get; set; }

            [XmlElement(ElementName = "PINNO")]
            public string PinNo { get; set; }

            [XmlElement(ElementName = "SIGNATURE")]
            public string Signature { get; set; }

            [XmlElement(ElementName = "USER_ID")]
            public string UserId { get; set; }
        }
    }
}
