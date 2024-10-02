using LaxmiSunriseBank.Models.LaxmiSunriseBank;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaxmiSunriseBank.API.APIServices
{
    public interface IAPIServices
    {
        Task<EchoResponseModel> GetEcho(EchoRequestModel echoRequestModel);
    }
}
