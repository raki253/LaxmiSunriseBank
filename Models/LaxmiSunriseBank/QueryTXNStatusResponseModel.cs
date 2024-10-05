using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using static LaxmiSunriseBank.Models.LaxmiSunriseBank.CancelTransactionResponseModelXML;
using static LaxmiSunriseBank.Models.LaxmiSunriseBank.QueryTXNStatusResponseModelXML;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{
    public class QueryTXNStatusResponseModel
    {
        public bool? IsSuccess { get; set; }
        public bool? TimeOut { get; set; }
        public string? URL { get; set; }
        public string? Response { get; set; }
        public QueryTXNStatusResult QueryTXNStatusResult { get; set; }
    }

    public class QueryTXNStatusResponseModelXML
    {
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {
            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body? Body { get; set; }
        }

        public class Body
        {
            [XmlElement(ElementName = "QueryTXNStatusResponse", Namespace = "ClientWebService")]
            public QueryTXNStatusResponse QueryTXNStatusResponse { get; set; }
        }

        public class QueryTXNStatusResponse
        {
            [XmlElement(ElementName = "QueryTXNStatusResult")]
            public QueryTXNStatusResult QueryTXNStatusResult { get; set; }
        }

        public class QueryTXNStatusResult
        {
            [XmlElement(ElementName = "CODE")]
            public string Code { get; set; }

            [XmlElement(ElementName = "AGENT_SESSION_ID")]
            public string AgentSessionId { get; set; }

            [XmlElement(ElementName = "MESSAGE")]
            public string Message { get; set; }

            [XmlElement(ElementName = "PINNO")]
            public string PinNo { get; set; }

            [XmlElement(ElementName = "SENDER_NAME")]
            public string SenderName { get; set; }

            [XmlElement(ElementName = "RECEIVER_NAME")]
            public string ReceiverName { get; set; }

            [XmlElement(ElementName = "COLLECT_AMT")]
            public string CollectAmt { get; set; }

            [XmlElement(ElementName = "COLLECT_CURRENCY")]
            public string CollectCurrency { get; set; }

            [XmlElement(ElementName = "EXCHANGE_RATE")]
            public string ExchangeRate { get; set; }

            [XmlElement(ElementName = "SERVICE_CHARGE")]
            public string ServiceCharge { get; set; }

            [XmlElement(ElementName = "PAYOUTAMT")]
            public string PayoutAmt { get; set; }

            [XmlElement(ElementName = "PAYOUTCURRENCY")]
            public string PayoutCurrency { get; set; }

            [XmlElement(ElementName = "PARTNER_SETTLEMENT")]
            public string PartnerSettlement { get; set; }

            [XmlElement(ElementName = "PARTNER_CCYRATE")]
            public string PartnerCcyRate { get; set; }

            [XmlElement(ElementName = "STATUS")]
            public string Status { get; set; }

            [XmlElement(ElementName = "STATUS_DATE")]
            public string StatusDate { get; set; }

            [XmlElement(ElementName = "AGENT_TXNID")]
            public string AgentTxnId { get; set; }
        }
    }

}
