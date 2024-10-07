using LaxmiSunriseBank.Models.LaxmiSunriseBank;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaxmiSunriseBank.Public
{
    public interface IServiceMethods
    {
        public Task<EchoResponseModel> GetEcho(EchoRequestModel echoRequestModel);

        public Task<AgentListResponse> GetAgentList(AgentListRequestModel agentListRequestModel);

        public Task<BankListResponse> GetBankList(BankListRequestModel bankListRequestModel);

        public Task<CurrentBalanceResponse> GetCurrentBalance(CurrentBalanceRequestModel currentBalanceRequestModel);

        public Task<ExRateResponse> GetEXRate(ExRateRequestModel exRateRequestModel);

        public Task<AmendmentResponse> AmendmentRequest(AmendmentRequestModel amendmentRequestModel);

        public Task<SendTransactionResponseModel> SendTransactionRequest(SendTransactionRequestModel sendTransactionRequestModel);

        public Task<CancelTransactionResponseModel> CancelTransaction(CancelTransactionRequestModel cancelTransactionRequestModel);

        //Not yet confirm
        //public Task<ReconcileReportResponseModel> ReconcileReport(ReconcileReportRequestModel reconcileReportRequestModel);

        public Task<QueryTXNStatusResponseModel> QueryTXNStatus(QueryTXNStatusRequestModel queryTXNStatusRequestModel);

    }
}