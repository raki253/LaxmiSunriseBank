using AutoMapper;
using LaxmiSunriseBank.Models.LaxmiSunriseBank;
using System.Collections.Generic;
using static LaxmiSunriseBank.Models.LaxmiSunriseBank.BankListResponseModelXML;

namespace LaxmiSunriseBank.Services.Models.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AgentListResponseModelXML.ReturnAgentList, AgentListResponseModel>().ReverseMap();
            CreateMap<ReconcileReportRequestModel, ReconcileReportRequestModelXML.ReconcileReportRequest>().ReverseMap();
            CreateMap<CancelTransactionRequestModel, CancelTransactionRequestModelXML.CancelTransaction>().ReverseMap();
            CreateMap<BankListRequestModel, BankListRequestModelXML.GetBankList>().ReverseMap();
            CreateMap<EchoRequestModel, EchoRequestModelXML.GetEcho>().ReverseMap();
            CreateMap<AmendmentRequestModel, AmendmentRequestModelXML.AmendmentRequest>().ReverseMap();
            CreateMap<CurrentBalanceRequestModel, CurrentBalanceRequestModelXML.GetCurrentBalanceRequest>().ReverseMap();
            CreateMap<ExRateRequestModel, ExRateRequestModelXML.GetEXRateRequest>().ReverseMap();
            CreateMap<SendTransactionRequestModel, SendTransactionRequestModelXML.SendTransaction>().ReverseMap();
            CreateMap<AuthorizedConfirmedRequestModel, AuthorizedConfirmedRequestModelXML.AuthorizedConfirmModel>().ReverseMap();

            //CreateMap<ReturnBankList, BankListResponseModel>().ReverseMap();

            //CreateMap<ReturnBankList, List<BankListResponseModel>>().ReverseMap();

            //CreateMap<BankListResponseModelXML.ReturnBankList, BankListResponseModel>()
        }
    }
}
