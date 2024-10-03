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
using LaxmiSunriseBank.Services.Models;
using static LaxmiSunriseBank.Models.LaxmiSunriseBank.BankListResponseModelXML;

namespace LaxmiSunriseBank.API.APIServices
{
    public class APIServices : IAPIServices
    {
        private readonly IAPIHandler _apiHandler;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="apiHandler"></param>
        public APIServices(IAPIHandler apiHandler, IMapper mapper)
        {
            _apiHandler = apiHandler;
            _mapper = mapper;
        }

        public async Task<AgentListResponse> GetAgentList(AgentListRequestModel agentListRequestModel)
        {
            AgentListResponse response = new AgentListResponse();
            try
            {
                var purposeRequestXML = new SourceRequestModelXML.Envelope
                {
                    Body = new SourceRequestModelXML.Body
                    {
                        GetAgentList = new SourceRequestModelXML.GetAgentList
                        {
                            AGENT_CODE = "FED001",
                            USER_ID = "FEDERAL1",
                            AGENT_SESSION_ID = "1298709",
                            SIGNATURE = "7d56439f55beff0d0fcd50bc4d887ca24848b676963126a6d317bbb4f377f701",
                            PAYMENTTYPE = "C",
                            PAYOUT_COUNTRY = "NEP",
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
                var apiResponseData = await _apiHandler.SOAPPostCall<AgentListResponseModelXML.Envelope>("https://sunrise.iremit.com.my/SendWSV5/txnservice.asmx", serializedXML);
                if (apiResponseData.IsSuccess)
                {
                    var responseModel = _mapper.Map<List<AgentListResponseModel>>(apiResponseData?.ResponseData?.Body?.GetAgentListResponse?.GetAgentListResult?.Return_AGENTLIST);
                    response.IsSuccess = true;
                    response.AgentList = responseModel;
                }
                else
                {
                    response.IsSuccess = false;
                    if (apiResponseData!.IsTimedOut)
                        response.TimeOut = true;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                if (ex.Message.Contains("Timeout"))
                {
                    response.TimeOut = true;
                }
                return response;
            }
        }

        public async Task<BankListResponse> GetBankList(BankListRequestModel bankListRequestModel)
        {
            BankListResponse response = new BankListResponse();
            try
            {
                var bankListRequestXML = new SourceRequestModelXML.Envelope
                {
                    Body = new SourceRequestModelXML.Body
                    {
                        GetBankList = new SourceRequestModelXML.GetBankList
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
                        serializer.Serialize(xmlWriter, bankListRequestXML, namespaces);
                        serializedXML = stringWriter.ToString();
                    }
                }
                var apiResponseData = await _apiHandler.SOAPPostCall<BankListResponseModelXML.Envelope>("https://sunrise.iremit.com.my/SendWSV5/txnservice.asmx", serializedXML);

                if (apiResponseData.IsSuccess)
                {
                    if (apiResponseData.IsSuccess)
                    {
                        var serializer1 = new XmlSerializer(typeof(SoapEnvelope));
                        using (var reader = new StringReader(apiResponseData.Response))
                        {
                            var responseModel = (SoapEnvelope)serializer1.Deserialize(reader);
                            response.IsSuccess = true;
                            response.BankList = responseModel.Body?.GetBankListResponse?.GetBankListResult?.Return_BankList;
                        }

                        //var responseModel = _mapper.Map<List<BankListResponseModel>>(apiResponseData?.ResponseData?.Body?.GetBankListResponse?.GetBankListResult?.Return_BANKLIST);
                        //response.IsSuccess = true;
                        //response.BankList = responseModel;
                    }

                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                if (ex.Message.Contains("Timeout"))
                {
                    response.TimeOut = true;
                }
                return response;
            }
        }

        public async Task<EchoResponseModel> GetEcho(EchoRequestModel echoRequestModel)
        {
            EchoResponseModel response = new EchoResponseModel();
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
                    //response = _mapper.Map<EchoResponseModel>(apiResponseData?.ResponseData?.Body?.GetEcho);
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    if (apiResponseData!.IsTimedOut)
                        response.TimeOut = true;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                if (ex.Message.Contains("Timeout"))
                {
                    response.TimeOut = true;
                }
                return response;
            }
        }
    }
}