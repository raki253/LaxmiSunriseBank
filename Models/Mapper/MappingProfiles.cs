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
            //CreateMap<ReturnBankList, BankListResponseModel>().ReverseMap();

            //CreateMap<ReturnBankList, List<BankListResponseModel>>().ReverseMap();

            //CreateMap<BankListResponseModelXML.ReturnBankList, BankListResponseModel>()
        }
    }
}
