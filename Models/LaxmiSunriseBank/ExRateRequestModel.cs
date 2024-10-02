using System;
using System.Collections.Generic;
using System.Text;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{
    public class ExRateRequestModel : SourceRequestModel
    {
        public string? TransferAmount { get; set; }

        public string? PaymentMode { get; set; }

        public string? CalcBy { get; set; }

        public string? LocationId { get; set; }

        public string? PayoutCountry { get; set; }
    }
}
