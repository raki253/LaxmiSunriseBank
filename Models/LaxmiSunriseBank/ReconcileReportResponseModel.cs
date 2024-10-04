using System;
using System.Collections.Generic;
using System.Text;
using static LaxmiSunriseBank.Models.LaxmiSunriseBank.AmendmentResponseModelXML;
using System.Xml.Serialization;
using static LaxmiSunriseBank.Models.LaxmiSunriseBank.ReconcileReportResponseModelXML;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{
    public class ReconcileReportResponseModel
    {
        public bool? IsSuccess { get; set; }
        public bool? TimeOut { get; set; }
        public string? URL { get; set; }
        public string? Response { get; set; }
        public ReturnTransReport ReturnTransReport { get; set; }
    }


    public class ReconcileReportResponseModelXML
    {
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {

            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body? Body { get; set; }
        }

        public class Body
        {
            [XmlElement(ElementName = "ReconcileReportResponse", Namespace = "ClientWebService")]
            public ReconcileReportResponse ReconcileReportResponse { get; set; }
        }

        public class ReconcileReportResponse
        {
            [XmlElement(ElementName = "ReconcileReportResult")]
            public ReconcileReportResult ReconcileReportResult { get; set; }
        }

        public class ReconcileReportResult
        {
            [XmlElement(ElementName = "Return_TRANSREPORT")]
            public ReturnTransReport ReturnTransReport { get; set; }
        }

        public class ReturnTransReport
        {
            [XmlElement(ElementName = "CODE")]
            public string Code { get; set; }

            [XmlElement(ElementName = "AGENT_NAME")]
            public string AgentName { get; set; }

            [XmlElement(ElementName = "AGENT_BRANCH")]
            public string AgentBranch { get; set; }

            [XmlElement(ElementName = "TRANSACTION_STATUS")]
            public string TransactionStatus { get; set; }

            [XmlElement(ElementName = "PINNO")]
            public string PinNo { get; set; }

            [XmlElement(ElementName = "SENDER_NAME")]
            public string SenderName { get; set; }

            [XmlElement(ElementName = "SENDER_ID_TYPE")]
            public string SenderIdType { get; set; }

            [XmlElement(ElementName = "SENDER_ID")]
            public string SenderId { get; set; }

            [XmlElement(ElementName = "SENDER_NATIONALITY")]
            public string SenderNationality { get; set; }

            [XmlElement(ElementName = "SENDER_ADDRESS")]
            public string SenderAddress { get; set; }

            [XmlElement(ElementName = "SENDER_CONTACT_NO")]
            public string SenderContactNo { get; set; }

            [XmlElement(ElementName = "RECEIVER_NAME")]
            public string ReceiverName { get; set; }

            [XmlElement(ElementName = "RECEIVER_COUNTRY")]
            public string ReceiverCountry { get; set; }

            [XmlElement(ElementName = "RECEIVER_ADDRESS")]
            public string ReceiverAddress { get; set; }

            [XmlElement(ElementName = "REMIT_AMT")]
            public string RemitAmt { get; set; }

            [XmlElement(ElementName = "REMIT_CCY")]
            public string RemitCcy { get; set; }

            [XmlElement(ElementName = "REMIT_CHARGE")]
            public string RemitCharge { get; set; }

            [XmlElement(ElementName = "REMIT_EXRATE")]
            public string RemitExRate { get; set; }

            [XmlElement(ElementName = "PAYOUT_AMT")]
            public string PayoutAmt { get; set; }

            [XmlElement(ElementName = "PAYOUT_CCY")]
            public string PayoutCcy { get; set; }

            [XmlElement(ElementName = "TRANSACTION_TYPE")]
            public string TransactionType { get; set; }

            [XmlElement(ElementName = "TRANSACTION_DATE")]
            public string TransactionDate { get; set; }

            [XmlElement(ElementName = "USER_ID")]
            public string UserId { get; set; }

            [XmlElement(ElementName = "APPROVE_DATE")]
            public string ApproveDate { get; set; }

            [XmlElement(ElementName = "APPROVE_USER_ID")]
            public string ApproveUserId { get; set; }

            [XmlElement(ElementName = "BANK_NAME")]
            public string BankName { get; set; }

            [XmlElement(ElementName = "BANK_BRANCH_NAME")]
            public string BankBranchName { get; set; }

            [XmlElement(ElementName = "BANK_ACCOUNT_NUMBER")]
            public string BankAccountNumber { get; set; }

            [XmlElement(ElementName = "STATUS")]
            public string Status { get; set; }

            [XmlElement(ElementName = "PAID_DATE")]
            public string PaidDate { get; set; }

            [XmlElement(ElementName = "PAYOUT_AGENT")]
            public string PayoutAgent { get; set; }

            [XmlElement(ElementName = "CANCEL_DATE")]
            public string CancelDate { get; set; }

            [XmlElement(ElementName = "MESSAGE")]
            public string Message { get; set; }

            [XmlElement(ElementName = "AGENT_SESSION_ID")]
            public string AgentSessionId { get; set; }
        }
    }

}
