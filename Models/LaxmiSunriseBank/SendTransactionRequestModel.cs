using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{
    public class SendTransactionRequestModel : SourceRequestModel
    {
        public string AgentTxnId { get; set; }
        public string AuthorizedRequired { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankBranchName { get; set; }
        public string BankId { get; set; }
        public string BankName { get; set; }
        public string CalcBy { get; set; }
        public string LocationId { get; set; }
        public string PaymentMode { get; set; }
        public string PurposeOfRemittance { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverCity { get; set; }
        public string ReceiverContactNumber { get; set; }
        public string ReceiverCountry { get; set; }
        public string ReceiverFirstName { get; set; }
        public string ReceiverLastName { get; set; }
        public string ReceiverMiddleName { get; set; }
        public string RelationshipToBeneficiary { get; set; }
        public decimal OurServiceCharge { get; set; }
        public decimal SettlementDollarRate { get; set; }
        public string SenderAddress { get; set; }
        public string SenderCity { get; set; }
        public string SenderCountry { get; set; }
        public string SenderDateOfBirth { get; set; }
        public string SenderFirstName { get; set; }
        public string SenderGender { get; set; }
        public string SenderIdExpireDate { get; set; }
        public string SenderIdIssueDate { get; set; }
        public string SenderIdNumber { get; set; }
        public string SenderIdType { get; set; }
        public string SenderLastName { get; set; }
        public string SenderMiddleName { get; set; }
        public string SenderMobile { get; set; }
        public string SenderNationality { get; set; }
        public string SenderOccupation { get; set; }
        public string SourceOfFund { get; set; }
        public decimal TransactionExchangeRate { get; set; }
        public decimal TransferAmount { get; set; }
    }

    public class SendTransactionRequestModelXML
    {
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {

            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body? Body { get; set; }
        }

        public class Body
        {
            [XmlElement(ElementName = "SendTransaction", Namespace = "ClientWebService")]
            public SendTransaction? SendTransactionObject { get; set; }
        }

        public class SendTransaction
        {
            [XmlElement(ElementName = "AGENT_TXN_ID")]
            public string AgentTxnId { get; set; }

            [XmlElement(ElementName = "AUTHORIZED_REQUIRED")]
            public string AuthorizedRequired { get; set; }

            [XmlElement(ElementName = "BANK_ACCOUNT_NUMBER")]
            public string BankAccountNumber { get; set; }

            [XmlElement(ElementName = "BANK_BRANCH_NAME")]
            public string BankBranchName { get; set; }

            [XmlElement(ElementName = "BANK_ID")]
            public string BankId { get; set; }

            [XmlElement(ElementName = "BANK_NAME")]
            public string BankName { get; set; }

            [XmlElement(ElementName = "CALC_BY")]
            public string CalcBy { get; set; }

            [XmlElement(ElementName = "LOCATION_ID")]
            public string LocationId { get; set; }

            [XmlElement(ElementName = "PAYMENT_MODE")]
            public string PaymentMode { get; set; }

            [XmlElement(ElementName = "PURPOSE_OF_REMITTANCE")]
            public string PurposeOfRemittance { get; set; }

            [XmlElement(ElementName = "RECEIVER_ADDRESS")]
            public string ReceiverAddress { get; set; }

            [XmlElement(ElementName = "RECEIVER_CITY")]
            public string ReceiverCity { get; set; }

            [XmlElement(ElementName = "RECEIVER_CONTACT_NUMBER")]
            public string ReceiverContactNumber { get; set; }

            [XmlElement(ElementName = "RECEIVER_COUNTRY")]
            public string ReceiverCountry { get; set; }

            [XmlElement(ElementName = "RECEIVER_FIRST_NAME")]
            public string ReceiverFirstName { get; set; }

            [XmlElement(ElementName = "RECEIVER_LAST_NAME")]
            public string ReceiverLastName { get; set; }

            [XmlElement(ElementName = "RECEIVER_MIDDLE_NAME")]
            public string ReceiverMiddleName { get; set; }

            [XmlElement(ElementName = "RELATIONSHIP_TO_BENEFICIARY")]
            public string RelationshipToBeneficiary { get; set; }

            [XmlElement(ElementName = "OUR_SERVICE_CHARGE")]
            public decimal OurServiceCharge { get; set; }

            [XmlElement(ElementName = "SETTLEMENT_DOLLAR_RATE")]
            public decimal SettlementDollarRate { get; set; }

            [XmlElement(ElementName = "SENDER_ADDRESS")]
            public string SenderAddress { get; set; }

            [XmlElement(ElementName = "SENDER_CITY")]
            public string SenderCity { get; set; }

            [XmlElement(ElementName = "SENDER_COUNTRY")]
            public string SenderCountry { get; set; }

            [XmlElement(ElementName = "SENDER_DATE_OF_BIRTH")]
            public string SenderDateOfBirth { get; set; }

            [XmlElement(ElementName = "SENDER_FIRST_NAME")]
            public string SenderFirstName { get; set; }

            [XmlElement(ElementName = "SENDER_GENDER")]
            public string SenderGender { get; set; }

            [XmlElement(ElementName = "SENDER_ID_EXPIRE_DATE")]
            public string SenderIdExpireDate { get; set; }

            [XmlElement(ElementName = "SENDER_ID_ISSUE_DATE")]
            public string SenderIdIssueDate { get; set; }

            [XmlElement(ElementName = "SENDER_ID_NUMBER")]
            public string SenderIdNumber { get; set; }

            [XmlElement(ElementName = "SENDER_ID_TYPE")]
            public string SenderIdType { get; set; }

            [XmlElement(ElementName = "SENDER_LAST_NAME")]
            public string SenderLastName { get; set; }

            [XmlElement(ElementName = "SENDER_MIDDLE_NAME")]
            public string SenderMiddleName { get; set; }

            [XmlElement(ElementName = "SENDER_MOBILE")]
            public string SenderMobile { get; set; }

            [XmlElement(ElementName = "SENDER_NATIONALITY")]
            public string SenderNationality { get; set; }

            [XmlElement(ElementName = "SENDER_OCCUPATION")]
            public string SenderOccupation { get; set; }

            [XmlElement(ElementName = "SOURCE_OF_FUND")]
            public string SourceOfFund { get; set; }

            [XmlElement(ElementName = "TRANSACTION_EXCHANGE_RATE")]
            public decimal TransactionExchangeRate { get; set; }

            [XmlElement(ElementName = "TRANSFER_AMOUNT")]
            public decimal TransferAmount { get; set; }
        }
    }
}
