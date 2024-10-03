using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using static LaxmiSunriseBank.Models.LaxmiSunriseBank.AgentListResponseModelXML;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{
    public class AgentListResponse
    {
        public bool? IsSuccess { get; set; }
        public bool? TimeOut { get; set; }
        public string? URL { get; set; }
        public List<ReturnAgentList> AgentList { get; set; } = new List<ReturnAgentList>();
    }
    public class AgentListResponseModel : SourceResponse
    {
        public string? Code { get; set; }

        public string? AgentSessionId { get; set; }

        public string? Message { get; set; }

        public string? LocationId { get; set; }

        public string? Agent { get; set; }

        public string? Branch { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Currency { get; set; }
    }

    public class AgentListResponseModelXML
    {
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {
            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body? Body { get; set; }
        }

        public class Body
        {
            [XmlElement(ElementName = "GetAgentListResponse", Namespace = "Web Services")]
            public GetAgentListResponse? GetAgentListResponse { get; set; }
        }

        public class GetAgentListResponse
        {
            [XmlElement(ElementName = "GetAgentListResult")]
            public GetAgentListResult? GetAgentListResult { get; set; }
        }

        public class GetAgentListResult
        {
            [XmlElement(ElementName = "Return_AGENTLIST")]
            public List<ReturnAgentList>? Return_AGENTLIST { get; set; }
        }

        public class ReturnAgentList
        {
            [XmlElement(ElementName = "CODE")]
            public string? CODE { get; set; }

            [XmlElement(ElementName = "AGENT_SESSION_ID")]
            public string? AGENT_SESSION_ID { get; set; }

            [XmlElement(ElementName = "MESSAGE")]
            public string? MESSAGE { get; set; }

            [XmlElement(ElementName = "LOCATIONID")]
            public string? LOCATIONID { get; set; }

            [XmlElement(ElementName = "AGENT")]
            public string? AGENT { get; set; }

            [XmlElement(ElementName = "BRANCH")]
            public string? BRANCH { get; set; }

            [XmlElement(ElementName = "ADDRESS")]
            public string? ADDRESS { get; set; }

            [XmlElement(ElementName = "CITY")]
            public string? CITY { get; set; }

            [XmlElement(ElementName = "CURRENCY")]
            public string? CURRENCY { get; set; }
        }

    }
}
