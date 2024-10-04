using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{
    public class AmendmentRequestModel : SourceRequestModel
    {
        public string PinNo { get; set; }

        public string AmendmentField { get; set; }

        public string AmendmentValue { get; set; }

    }
}
