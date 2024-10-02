using System;
using System.Collections.Generic;
using System.Text;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{
    public class CurrentBalanceResponseModel
    {
        public string? Code { get; set; }

        public string? AgentSessionId { get; set; }

        public string? AgentName { get; set; }

        public string? Country { get; set; }

        public string? CurrentBalance { get; set; }

        public string? Currency { get; set; }

        public string? ServiceCharge { get; set; }
    }
}
