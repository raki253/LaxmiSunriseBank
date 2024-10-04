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

        public async Task<AmendmentResponse> AmendmentRequest(AmendmentRequestModel amendmentRequestModel)
        {
            AmendmentResponse response = new AmendmentResponse();
            try
            {
                var bankListRequestXML = new SourceRequestModelXML.Envelope
                {
                    Body = new SourceRequestModelXML.Body
                    {
                        AmendmentRequest = new SourceRequestModelXML.AmendmentRequest
                        {
                            AgentCode = amendmentRequestModel.AgentCode,
                            UserId = amendmentRequestModel.UserId,
                            AgentSessionId = amendmentRequestModel.AgentSessionId,
                            Signature = amendmentRequestModel.Signature,
                            AmendmentField = amendmentRequestModel.AmendmentField,
                            AmendmentValue = amendmentRequestModel.AmendmentValue,
                            PinNo = amendmentRequestModel.PinNo
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
                var apiResponseData = await _apiHandler.SOAPPostCall<AmendmentResponseModelXML.Envelope>("https://sunrise.iremit.com.my/SendWSV5/txnservice.asmx", serializedXML);

                if (apiResponseData.IsSuccess)
                {
                    if (apiResponseData.IsSuccess)
                    {
                        var serializer1 = new XmlSerializer(typeof(AmendmentResponseModelXML.Envelope));
                        using (var reader = new StringReader(apiResponseData.Response))
                        {
                            var responseModel = (AmendmentResponseModelXML.Envelope)serializer1.Deserialize(reader);
                            response.IsSuccess = true;
                            response.AmendmentRequestResult = responseModel.Body?.AmendmentRequestResponse?.AmendmentRequestResult;
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
                            AGENT_CODE = agentListRequestModel.AgentCode,
                            USER_ID = agentListRequestModel.UserId,
                            AGENT_SESSION_ID = agentListRequestModel.AgentSessionId,
                            SIGNATURE = agentListRequestModel.Signature,
                            PAYMENTTYPE = agentListRequestModel.PaymentType,
                            PAYOUT_COUNTRY = agentListRequestModel.PayoutCountry,
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
                    var deserializer = new XmlSerializer(typeof(AgentListResponseModelXML.Envelope));
                    using (var reader = new StringReader(apiResponseData.Response))
                    {
                        var responseModel = (AgentListResponseModelXML.Envelope)deserializer.Deserialize(reader);
                        response.IsSuccess = true;
                        response.AgentList = responseModel.Body?.GetAgentListResponse?.GetAgentListResult?.Return_AGENTLIST;
                    }
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
                            AGENT_CODE = bankListRequestModel.AgentCode,
                            USER_ID = bankListRequestModel.UserId,
                            AGENT_SESSION_ID = bankListRequestModel.AgentSessionId,
                            SIGNATURE = bankListRequestModel.Signature
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
                        var serializer1 = new XmlSerializer(typeof(BankListResponseModelXML.Envelope));
                        using (var reader = new StringReader(apiResponseData.Response))
                        {
                            var responseModel = (BankListResponseModelXML.Envelope)serializer1.Deserialize(reader);
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

        public async Task<CurrentBalanceResponse> GetCurrentBalance(CurrentBalanceRequestModel currentBalanceRequestModel)
        {
            CurrentBalanceResponse response = new CurrentBalanceResponse();
            try
            {
                var bankListRequestXML = new SourceRequestModelXML.Envelope
                {
                    Body = new SourceRequestModelXML.Body
                    {
                        GetCurrentBalance = new SourceRequestModelXML.GetCurrentBalanceRequest
                        {
                            AgentCode = currentBalanceRequestModel.AgentCode,
                            UserId = currentBalanceRequestModel.UserId,
                            AgentSessionId = currentBalanceRequestModel.AgentSessionId,
                            Signature = currentBalanceRequestModel.Signature
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
                var apiResponseData = await _apiHandler.SOAPPostCall<CurrentBalanceResponseModelXML.Envelope>("https://sunrise.iremit.com.my/SendWSV5/txnservice.asmx", serializedXML);

                if (apiResponseData.IsSuccess)
                {
                    if (apiResponseData.IsSuccess)
                    {
                        var serializer1 = new XmlSerializer(typeof(CurrentBalanceResponseModelXML.Envelope));
                        using (var reader = new StringReader(apiResponseData.Response))
                        {
                            var responseModel = (CurrentBalanceResponseModelXML.Envelope)serializer1.Deserialize(reader);
                            response.IsSuccess = true;
                            response.GetCurrentBalanceResult = responseModel.Body?.GetCurrentBalanceResponse?.GetCurrentBalanceResult;
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
                            AGENT_CODE = echoRequestModel.AgentCode,
                            USER_ID = echoRequestModel.UserId,
                            AGENT_SESSION_ID = echoRequestModel.AgentSessionId,
                            SIGNATURE = echoRequestModel.Signature
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
                var apiResponseData = await _apiHandler.SOAPPostCall<EchoResponseModelXML.Envelope>("https://sunrise.iremit.com.my/SendWSV5/txnservice.asmx", serializedXML);
                if (apiResponseData.IsSuccess)
                {
                    var serializer1 = new XmlSerializer(typeof(EchoResponseModelXML.Envelope));
                    using (var reader = new StringReader(apiResponseData.Response))
                    {
                        var responseModel = (EchoResponseModelXML.Envelope)serializer1.Deserialize(reader);
                        response.IsSuccess = true;
                        response.GetEchoResult = responseModel.Body?.GetEchoResponse?.GetEchoResult;
                    }
                    //response = _mapper.Map<EchoResponseModel>(apiResponseData?.ResponseData?.Body?.GetEcho);
                    //response.IsSuccess = true;
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

        public async Task<ExRateResponse> GetEXRate(ExRateRequestModel exRateRequestModel)
        {
            ExRateResponse response = new ExRateResponse();
            try
            {
                var bankListRequestXML = new SourceRequestModelXML.Envelope
                {
                    Body = new SourceRequestModelXML.Body
                    {
                        GetEXRate = new SourceRequestModelXML.GetEXRateRequest
                        {
                            AgentCode = exRateRequestModel.AgentCode,
                            UserId = exRateRequestModel.UserId,
                            AgentSessionId = exRateRequestModel.AgentSessionId,
                            Signature = exRateRequestModel.Signature,
                            CalcBy = exRateRequestModel.CalcBy,
                            LocationId = exRateRequestModel.LocationId,
                            PayoutCountry = exRateRequestModel.PayoutCountry,
                            PaymentMode = exRateRequestModel.PaymentMode,
                            TransferAmount = exRateRequestModel.TransferAmount,
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
                var apiResponseData = await _apiHandler.SOAPPostCall<ExRateResponseModelXML.Envelope>("https://sunrise.iremit.com.my/SendWSV5/txnservice.asmx", serializedXML);

                if (apiResponseData.IsSuccess)
                {
                    if (apiResponseData.IsSuccess)
                    {
                        var serializer1 = new XmlSerializer(typeof(ExRateResponseModelXML.Envelope));
                        using (var reader = new StringReader(apiResponseData.Response))
                        {
                            var responseModel = (ExRateResponseModelXML.Envelope)serializer1.Deserialize(reader);
                            response.IsSuccess = true;
                            response.GetEXRateResult = responseModel.Body?.GetEXRateResponse?.GetEXRateResult;
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
    }
}