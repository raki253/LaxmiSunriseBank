using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{
    public class SourceRequestModel
    {
        [StringLength(50)]
        public string? AgentCode { get; set; }

        [StringLength(50)]
        public string? UserId { get; set; }

        [StringLength(50)]
        public string? AgentSessionId { get; set; }

        [StringLength(150)]
        public string? Signature { get; set; }
    }

    /// <summary>
    /// Source Request Model XML
    /// </summary>
    public class SourceRequestModelXML
    {
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {

            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body? Body { get; set; }
        }

        public class Body
        {
            [XmlElement(ElementName = "Sources", Namespace = "http://tempuri.org/")]
            public Sources? Sources { get; set; }
        }

        public class Sources
        {
            [XmlElement(ElementName = "login", Namespace = "http://tempuri.org/")]
            public Login? Login { get; set; }
        }

        public class Login
        {
            [XmlElement(ElementName = "USERNAME", Namespace = "http://tempuri.org/")]
            public string? USERNAME { get; set; }

            [XmlElement(ElementName = "PASSWORD", Namespace = "http://tempuri.org/")]
            public string? PASSWORD { get; set; }

            [XmlElement(ElementName = "AGENTCODE", Namespace = "http://tempuri.org/")]
            public string? AGENTCODE { get; set; }
        }
    }
}
