using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{
    public class ReconcileReportRequestModel : SourceRequestModel
    {
        public string ReportType { get; set; }

        public string FromDate { get; set; }

        public string FromDateTime { get; set; }

        public string ToDate { get; set; }

        public string ToDateTime { get; set; }

        public string ShowIncremental { get; set; }
    }

    public class ReconcileReportRequestModelXML
    {
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {

            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body? Body { get; set; }
        }

        public class Body
        {
            [XmlElement(ElementName = "ReconcileReport", Namespace = "ClientWebService")]
            public ReconcileReportRequest ReconcileReport { get; set; }
        }

        public class ReconcileReportRequest
        {
            [XmlElement(ElementName = "AGENT_CODE")]
            public string AgentCode { get; set; }

            [XmlElement(ElementName = "USER_ID")]
            public string UserId { get; set; }

            [XmlElement(ElementName = "AGENT_SESSION_ID")]
            public string AgentSessionId { get; set; }

            [XmlElement(ElementName = "REPORT_TYPE")]
            public string ReportType { get; set; }

            [XmlElement(ElementName = "FROM_DATE")]
            public string FromDate { get; set; }

            [XmlElement(ElementName = "FROM_DATE_TIME")]
            public string FromDateTime { get; set; }

            [XmlElement(ElementName = "TO_DATE")]
            public string ToDate { get; set; }

            [XmlElement(ElementName = "TO_DATE_TIME")]
            public string ToDateTime { get; set; }

            [XmlElement(ElementName = "SHOW_INCREMENTAL")]
            public string ShowIncremental { get; set; }

            [XmlElement(ElementName = "SIGNATURE")]
            public string Signature { get; set; }
        }
    }

}
