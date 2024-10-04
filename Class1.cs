using System;
using System.ComponentModel.DataAnnotations;

namespace LaxmiSunriseBank
{
    public class RemittanceRequestModel
    {
        public string AgentTxnId { get; set; } 
        public string LocationId { get; set; } 
        public string SenderFirstName { get; set; }
        public string SenderMiddleName { get; set; }
        public string SenderLastName { get; set; } 
        public string SenderGender { get; set; }
        public string SenderAddress { get; set; }
        public string SenderCity { get; set; }
        public string SenderCountry { get; set; }
        public string SenderIdType { get; set; } 
        public string SenderIdNumber { get; set; }
        public string SenderIdIssueDate { get; set; }
        public string SenderIdExpireDate { get; set; }
        public string SenderDateOfBirth { get; set; }
        public string SenderMobile { get; set; } 
        public string SourceOfFund { get; set; }
        public string SenderOccupation { get; set; } 
        public string SenderNationality { get; set; } 
        public string ReceiverFirstName { get; set; } 
        public string ReceiverMiddleName { get; set; } 
        public string ReceiverLastName { get; set; }
        public string ReceiverAddress { get; set; } 
        public string ReceiverCity { get; set; } 
        public string ReceiverCountry { get; set; } 
        public string ReceiverContactNumber { get; set; } 
        public string RelationshipToBeneficiary { get; set; } 
        public string PaymentMode { get; set; } 
        public string BankId { get; set; } 
        public string BankName { get; set; } 
        public string BankBranchName { get; set; } 
        public string BankAccountNumber { get; set; } 
        public string CalcBy { get; set; } 
        public decimal TransferAmount { get; set; } 
        public decimal OurServiceCharge { get; set; } 
        public decimal TransactionExchangeRate { get; set; } 
        public decimal SettlementDollarRate { get; set; }
        public string PurposeOfRemittance { get; set; } 
        public string AuthorizedRequired { get; set; } 
    }
}
