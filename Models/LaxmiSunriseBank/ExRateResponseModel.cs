using System;
using System.Collections.Generic;
using System.Text;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{
    public class ExRateResponseModel
    {
        public string? Code { get; set; }

        public string? AgentSessionId { get; set; }

        public string? Message { get; set; }

        public string? CollectAmount { get; set; }

        public string? CollectCurrency { get; set; }

        public string? ServiceCharge { get; set; }

        public string? ExchangeRate { get; set; }

        public string? PayoutAmt { get; set; }

        public string? PayoutCurrency { get; set; }

        public string? ExRateSessionId { get; set; }

        public string? SettlementRate { get; set; }
    }
}
