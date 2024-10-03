using AutoMapper;
using LaxmiSunriseBank.Models.LaxmiSunriseBank;

namespace LaxmiSunriseBank.Services.Models.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AgentListResponseModelXML.ReturnAgentList, AgentListResponseModel>().ReverseMap();
        }
    }
}
