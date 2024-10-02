using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{
    public class AgentListRequestModel : SourceRequestModel
    {
        [StringLength(1)]
        public string? PaymentType { get; set; }

        [StringLength(3)]
        public string? PayoutCountry { get; set; }
    }
}
