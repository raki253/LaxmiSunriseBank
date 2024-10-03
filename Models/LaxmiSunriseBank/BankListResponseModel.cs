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
        public BankList1[] BankList { get; set; } = { };
    }

    public class BankListResponseModelXML
    {
        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {
            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body? Body { get; set; }
        }

        public class Body
        {
            [XmlElement(ElementName = "GetBankListResponse", Namespace = "Web Services")]
            public GetBankListResponse? GetBankListResponse { get; set; }
        }

        
        public class GetBankListResponse
        {
            [XmlElement(ElementName = "GetBankListResult")]
            public GetBankListResult? GetBankListResult { get; set; }
        }

        public class GetBankListResult
        {
            [XmlArray("Return_BANKLIST")]
            [XmlArrayItem("Return_BANKLIST")]
            public List<ReturnBankList>? Return_BANKLIST { get; set; }
        }

        public class ReturnBankList
        {
            [XmlElement("BANK_ID")]
            public string BankId { get; set; }

            [XmlElement("BANKNAME")]
            public string BankName { get; set; }

            [XmlElement("PAYMENT_MODE")]
            public string PaymentMode { get; set; }
        }
    }


    //////////////////test
    /// <summary>

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class SoapEnvelope
    {
        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public SoapBody Body { get; set; }
    }

    public class SoapBody
    {
        [XmlElement("GetBankListResponse", Namespace = "ClientWebService")]
        public GetBankListResponse1 GetBankListResponse { get; set; }
    }

    public class GetBankListResponse1
    {
        [XmlElement(ElementName = "GetBankListResult")]
        public GetBankListResult1 GetBankListResult { get; set; }
    }

    public class GetBankListResult1
    {
        [XmlElement(ElementName = "Return_BANKLIST")]
        public BankList1[] Return_BankList { get; set; }
    }

    public class BankList1
    {
        [XmlElement("BANK_ID")]
        public string BankId { get; set; }

        [XmlElement("BANKNAME")]
        public string BankName { get; set; }

        [XmlElement("PAYMENT_MODE")]
        public string PaymentMode { get; set; }
    }


}
