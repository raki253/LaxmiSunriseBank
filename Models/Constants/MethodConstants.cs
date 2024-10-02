namespace LaxmiSunriseBank.Services.Models.Constants
{
    #region Method Constants
    public static class MethodConstants
    {
        public const string UpdateBankAPITransactionStatus = "PreInitiated";
        public const string SendMethodName = "Send";
        public const string SendTransactionMethodName = "Send Transaction";
        public const string StatusEnquiryMethodName = "Status Enquiry";
        public const string CancelTransactionMethodName = "Cancel Transaction";
        public const string InsertBankAPIHistoryMethodName = "Account Balance";
        public const string GetBankMethodName = "Get Bank";
        public const string GetBranchMethodName = "Get Branch";
    }
    #endregion

    #region General Constants
    public class GeneralTermConstants
    {
        public const string InSufficientFund = "Insufficient Fund";
        public const string TimedOut = "Timed Out";
        public const string APIFailed = "API Failed";
        public const string InProgress = "Inprogress";
        public const string SuccessAPICode = "000";
        public const string InvalidAmount = "117";
        public const string AmountReceived = "AMOUNT RECEIVED";
        public const string Success = "Success";
        public const string Paid = "PAID";
        public const string Cancelled = "CANCELLED";
        public const string Initiated = "Initiated";
        public const string Processed = "Processed";
        public const string LowAgentBalance = "LOW AGENT BALANCE";
        public const string Failed = "Failed";
        public const string ClientCode = "ADIB";
        public const string ResponseTransTypeName = "Response";
        public const string StatusTransTypeName = "Status";
        public const string SuccessStatusTypeName = "Success";
        public const string FailedStatusTypeName = "Failed";
        public const string InitiatedStatusTypeName = "Initiated";
        public const string UnknownStatusTypeName = "Inprogress";
        public const string TimedOutStatusTypeName = "Timed Out";
        public const string InSufficientFundStatusTypeName = "Insufficient Fund";
    }
    #endregion
}
