﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{
    public class BankListRequestModel : SourceRequestModel
    {
    }

    public class BankListRequestModelXML
    {
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {

            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body? Body { get; set; }
        }

        public class Body
        {
            [XmlElement(ElementName = "GetBankList", Namespace = "ClientWebService")]
            public GetBankList? GetBankList { get; set; }
        }

        public class GetBankList
        {
            [XmlElement(ElementName = "AGENT_CODE")]
            public string? AGENT_CODE { get; set; }

            [XmlElement(ElementName = "USER_ID")]
            public string? USER_ID { get; set; }

            [XmlElement(ElementName = "AGENT_SESSION_ID")]
            public string? AGENT_SESSION_ID { get; set; }

            [XmlElement(ElementName = "SIGNATURE")]
            public string? SIGNATURE { get; set; }
        }
    }
}
