using AutoMapper;
using LaxmiSunriseBank.Models.LaxmiSunriseBank;
using LaxmiSunriseBank.Services.API.APIHandler;
using LaxmiSunriseBank.Services.Models.Constants;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using static LaxmiSunriseBank.Models.LaxmiSunriseBank.SourceRequestModelXML;

namespace LaxmiSunriseBank.API.APIServices
{
   
    public class APIServices : IAPIServices
    {
        private readonly IAPIHandler _apiHandler;
       
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="apiHandler"></param>
        public APIServices(IAPIHandler apiHandler)
        {
            _apiHandler = apiHandler;
        }
        public async Task<EchoResponseModel> GetEcho(EchoRequestModel echoRequestModel)
        {
            try
            {
                var purposeRequestXML = new SourceRequestModelXML.Envelope
                {
                    Body = new SourceRequestModelXML.Body
                    {
                        GetEcho = new SourceRequestModelXML.GetEcho
                        {
                            AGENT_CODE = "FED001",
                            USER_ID = "FEDERAL1",
                            AGENT_SESSION_ID = "1298709",
                            SIGNATURE = "7d56439f55beff0d0fcd50bc4d887ca24848b676963126a6d317bbb4f377f701"
                        }
                    }
                };

                string serializedXML = string.Empty;
                XmlSerializer serializer = new XmlSerializer(typeof(SourceRequestModelXML.Envelope));
                var namespaces = new XmlSerializerNamespaces();
                namespaces.Add("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
                namespaces.Add("tem", "http://tempuri.org/");
                var settings = new XmlWriterSettings { OmitXmlDeclaration = true };
                using (StringWriter stringWriter = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
                    {
                        serializer.Serialize(xmlWriter, purposeRequestXML, namespaces);
                        serializedXML = stringWriter.ToString();
                    }
                }
                var apiResponseData = await _apiHandler.SOAPPostCall<SourceRequestModelXML.Envelope>("https://sunrise.iremit.com.my/SendWSV5/txnservice.asmx", serializedXML);
                if (apiResponseData.IsSuccess)
                {
                  
                }
                else
                {
                   
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}