using System;
using System.ComponentModel.DataAnnotations;

namespace LaxmiSunriseBank
{
    public class RemittanceRequestModel
    {
        public string AgentTxnId { get; set; } // AGENT_TXNID string 20 Agent TXN ID (Must be unique)
        public string LocationId { get; set; } // LOCATION_ID string 10 Payout Location ID
        public string SenderFirstName { get; set; } // SENDER_FIRST_NAME string 50 Sender First Name
        public string SenderMiddleName { get; set; } // SENDER_MIDDLE_NAME string 50 Sender Middle Name
        public string SenderLastName { get; set; } // SENDER_LAST_NAME string 50 Sender Last Name
        public string SenderGender { get; set; } // SENDER_GENDER string 6 MALE/FEMALE
        public string SenderAddress { get; set; } // SENDER_ADDRESS string 100 Sender Full Address
        public string SenderCity { get; set; } // SENDER_CITY string 50 Sender City
        public string SenderCountry { get; set; } // SENDER_COUNTRY string 3 Sender country
        public string SenderIdType { get; set; } // SENDER_ID_TYPE string 50 Sender Valid ID Type
        public string SenderIdNumber { get; set; } // SENDER_ID_NUMBER string 20 Sender ID Number
        public string SenderIdIssueDate { get; set; } // SENDER_ID_ISSUE_DATE string 10 ID Issue Date (yyyy-dd-mm)
        public string SenderIdExpireDate { get; set; } // SENDER_ID_EXPIRE_DATE string 10 ID Expiry Date (yyyy-mm-dd)
        public string SenderDateOfBirth { get; set; } // SENDER_DATE_OF_BIRTH string 10 Date Of Birth (yyyy-mm-dd)
        public string SenderMobile { get; set; } // SENDER_MOBILE string 16 Sender Mobile No.
        public string SourceOfFund { get; set; } // SOURCE_OF_FUND string 20 Sender Source of Funds
        public string SenderOccupation { get; set; } // SENDER_OCCUPATION string 20 Occupation of Sender
        public string SenderNationality { get; set; } // SENDER_NATIONALITY string 50 Sender Nationality Country Name
        public string ReceiverFirstName { get; set; } // RECEIVER_FIRST_NAME string 50 Receiver First Name
        public string ReceiverMiddleName { get; set; } // RECEIVER_MIDDLE_NAME string 50 Receiver Middle Name
        public string ReceiverLastName { get; set; } // RECEIVER_LAST_NAME string 50 Receiver Last Name
        public string ReceiverAddress { get; set; } // RECEIVER_ADDRESS string 100 Receiver Address
        public string ReceiverCity { get; set; } // RECEIVER_CITY string 50 Receiver City
        public string ReceiverCountry { get; set; } // RECEIVER_COUNTRY string 3 Must be Nepal
        public string ReceiverContactNumber { get; set; } // RECEIVER_CONTACT_NUMBER string 20 Receiver Contact Number
        public string RelationshipToBeneficiary { get; set; } // RELATIONSHIP_TO_BENEFICIARY string 20 Relationship of Sender to Beneficiary
        public string PaymentMode { get; set; } // PAYMENT_MODE string 1 C- Cash Pickup, B- Account Deposit, I – Instant Credit, W – Mobile Wallet
        public string BankId { get; set; } // BANK_ID string 10 Optional, Mandatory for PAYMENTMODE I
        public string BankName { get; set; } // BANK_NAME string 150 Beneficiary Bank Name
        public string BankBranchName { get; set; } // BANK_BRANCH_NAME string 150 Beneficiary Bank branch name
        public string BankAccountNumber { get; set; } // BANK_ACCOUNT_NUMBER string 50 Beneficiary Bank Account Number
        public string CalcBy { get; set; } // CALC_BY string 1 P – Calculation by Payout Amount
        public decimal TransferAmount { get; set; } // TRANSFER_AMOUNT Money Total Amount Collected from Customer
        public decimal OurServiceCharge { get; set; } // OURSERVICE_CHARGE Money If Partner passes their Service Fee
        public decimal TransactionExchangeRate { get; set; } // TRANSACTION_EXCHANGERATE Money Partner defines the Customer Rate
        public decimal SettlementDollarRate { get; set; } // SETTLEMENT_DOLLARRATE Money Partner defines the Cost Rate for USD/NPR
        public string PurposeOfRemittance { get; set; } // PURPOSE_OF_REMITTANCE string 20 Purpose of Remit
        public string AuthorizedRequired { get; set; } // AUTHORIZED_REQUIRED string 1 Pass "y" to commit Authorized_Confirmed
    }
}
