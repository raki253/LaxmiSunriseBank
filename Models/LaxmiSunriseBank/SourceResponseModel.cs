using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{
    /// <summary>
    /// Source Response
    /// </summary>
    public class SourceResponse
    {
        public bool? IsSuccess { get; set; }
        public bool? TimeOut { get; set; }
        public string? URL { get; set; }
        public List<SourceResponseModel> Source { get; set; } = new List<SourceResponseModel>();
    }

    /// <summary>
    /// Source Response Model
    /// </summary>
    public class SourceResponseModel
    {
        public string? SRCCODE { get; set; }
        public string? SRCDESC { get; set; }
        public string? StatusCode { get; set; }
        public string? StatusMessage { get; set; }

    }

    /// <summary>
    /// Source Response Model XML For Getting The Sources
    /// </summary>
    public class SourceResponseModelXML
    {
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {
            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body? Body { get; set; }
        }

        public class Body
        {
            [XmlElement(ElementName = "SourcesResponse", Namespace = "http://tempuri.org/")]
            public SourceResponse? SourcesResponse { get; set; }
        }

        public class SourceResponse
        {
            [XmlElement(ElementName = "SourcesResult", Namespace = "http://tempuri.org/")]
            public SourceResult? SourcesResult { get; set; }
        }

        public class SourceResult
        {
            [XmlElement(ElementName = "Source", Namespace = "http://tempuri.org/")]
            public List<Source>? SourceList { get; set; }
        }

        public class Source
        {
            [XmlElement(ElementName = "SRCCODE", Namespace = "http://tempuri.org/")]
            public string? SRCCODE { get; set; }

            [XmlElement(ElementName = "SRCDESC", Namespace = "http://tempuri.org/")]
            public string? SRCDESC { get; set; }

            [XmlElement(ElementName = "StatusCode", Namespace = "http://tempuri.org/")]
            public string? StatusCode { get; set; }

            [XmlElement(ElementName = "StatusMessage", Namespace = "http://tempuri.org/")]
            public string? StatusMessage { get; set; }
        }
    }
}
