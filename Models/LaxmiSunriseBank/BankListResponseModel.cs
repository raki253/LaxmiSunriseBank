using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using static LaxmiSunriseBank.Models.LaxmiSunriseBank.BankListResponseModelXML;

namespace LaxmiSunriseBank.Models.LaxmiSunriseBank
{
    public class BankListResponseModel
    {
        public string? BankId { get; set; }

        public string? BankName { get; set; }

        public string? PaymentMode { get; set; }
    }

    public class BankListResponse
    {
        public bool? IsSuccess { get; set; }
        public bool? TimeOut { get; set; }
        public string? URL { get; set; }
        public string? Response { get; set; }
        public List<BankList> BankList { get; set; }
    }

    public class BankListResponseModelXML
    {
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {
            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body Body { get; set; }
        }

        public class Body
        {
            [XmlElement("GetBankListResponse", Namespace = "ClientWebService")]
            public GetBankListResponse GetBankListResponse { get; set; }
        }

        public class GetBankListResponse
        {
            [XmlElement(ElementName = "GetBankListResult")]
            public GetBankListResult GetBankListResult { get; set; }
        }

        public class GetBankListResult
        {
            [XmlElement(ElementName = "Return_BANKLIST")]
            public List<BankList> Return_BankList { get; set; }
        }

        public class BankList
        {
            [XmlElement("BANK_ID")]
            public string BankId { get; set; }

            [XmlElement("BANKNAME")]
            public string BankName { get; set; }

            [XmlElement("PAYMENT_MODE")]
            public string PaymentMode { get; set; }
        }
    }
}
