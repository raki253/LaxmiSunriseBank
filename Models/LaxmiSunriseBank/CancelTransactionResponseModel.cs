using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using static LaxmiSunriseBank.Models.LaxmiSunriseBank.CancelTransactionResponseModelXML;
using static LaxmiSunriseBank.Models.LaxmiSunriseBank.ReconcileReportResponseModelXML;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{
    public class CancelTransactionResponseModel
    {
        public bool? IsSuccess { get; set; }
        public bool? TimeOut { get; set; }
        public string? URL { get; set; }
        public string? Response { get; set; }
        public CancelTransactionResult CancelTransactionResult { get; set; }
    }

    public class CancelTransactionResponseModelXML
    {
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {
            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body? Body { get; set; }
        }

        public class Body
        {
            [XmlElement(ElementName = "CancelTransactionResponse", Namespace = "ClientWebService")]
            public CancelTransactionResponse CancelTransactionResponse { get; set; }
        }

        public class CancelTransactionResponse
        {
            [XmlElement(ElementName = "CancelTransactionResult")]
            public CancelTransactionResult CancelTransactionResult { get; set; }
        }

        public class CancelTransactionResult
        {
            [XmlElement(ElementName = "CODE")]
            public string Code { get; set; }

            [XmlElement(ElementName = "AGENT_SESSION_ID")]
            public string AgentSessionId { get; set; }

            [XmlElement(ElementName = "MESSAGE")]
            public string Message { get; set; }

            [XmlElement(ElementName = "PINNO")]
            public string PinNo { get; set; }

            [XmlElement(ElementName = "AGENT_TXNID")]
            public string AgentTxnId { get; set; }

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

            [XmlElement(ElementName = "TXN_DATE")]
            public string TxnDate { get; set; }

            [XmlElement(ElementName = "SETTLEMENT_RATE")]
            public string SettlementRate { get; set; }
        }
    }

}
