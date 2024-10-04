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
                var amendmentRequestXML = new AmendmentRequestModelXML.Envelope
                {
                    Body = new AmendmentRequestModelXML.Body
                    {
                        AmendmentRequest = new AmendmentRequestModelXML.AmendmentRequest
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
                XmlSerializer serializer = new XmlSerializer(typeof(AmendmentRequestModelXML.Envelope));
                var namespaces = new XmlSerializerNamespaces();
                namespaces.Add("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
                namespaces.Add("tem", "http://tempuri.org/");
                var settings = new XmlWriterSettings { OmitXmlDeclaration = true };
                using (StringWriter stringWriter = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
                    {
                        serializer.Serialize(xmlWriter, amendmentRequestXML, namespaces);
                        serializedXML = stringWriter.ToString();
                    }
                }
                var apiResponseData = await _apiHandler.SOAPPostCall<AmendmentResponseModelXML.Envelope>("https://sunrise.iremit.com.my/SendWSV5/txnservice.asmx", serializedXML);

                if (apiResponseData.IsSuccess)
                {
                    if (apiResponseData.IsSuccess)
                    {
                        var deserializer = new XmlSerializer(typeof(AmendmentResponseModelXML.Envelope));
                        using (var reader = new StringReader(apiResponseData.Response))
                        {
                            var responseModel = (AmendmentResponseModelXML.Envelope)deserializer.Deserialize(reader);
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
                var agentListRequestXML = new AgentListRequestModelXML.Envelope
                {
                    Body = new AgentListRequestModelXML.Body
                    {
                        GetAgentList = new AgentListRequestModelXML.GetAgentList
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
                XmlSerializer serializer = new XmlSerializer(typeof(AgentListRequestModelXML.Envelope));
                var namespaces = new XmlSerializerNamespaces();
                namespaces.Add("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
                namespaces.Add("tem", "http://tempuri.org/");
                var settings = new XmlWriterSettings { OmitXmlDeclaration = true };
                using (StringWriter stringWriter = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
                    {
                        serializer.Serialize(xmlWriter, agentListRequestXML, namespaces);
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
                var bankListRequestXML = new BankListRequestModelXML.Envelope
                {
                    Body = new BankListRequestModelXML.Body
                    {
                        GetBankList = new BankListRequestModelXML.GetBankList
                        {
                            AGENT_CODE = bankListRequestModel.AgentCode,
                            USER_ID = bankListRequestModel.UserId,
                            AGENT_SESSION_ID = bankListRequestModel.AgentSessionId,
                            SIGNATURE = bankListRequestModel.Signature
                        }
                    }
                };

                string serializedXML = string.Empty;
                XmlSerializer serializer = new XmlSerializer(typeof(BankListRequestModelXML.Envelope));
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
                        var deserializer = new XmlSerializer(typeof(BankListResponseModelXML.Envelope));
                        using (var reader = new StringReader(apiResponseData.Response))
                        {
                            var responseModel = (BankListResponseModelXML.Envelope)deserializer.Deserialize(reader);
                            response.IsSuccess = true;
                            response.BankList = responseModel.Body?.GetBankListResponse?.GetBankListResult?.Return_BankList;
                        }
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
                var currentBalanceRequestXML = new CurrentBalanceRequestModelXML.Envelope
                {
                    Body = new CurrentBalanceRequestModelXML.Body
                    {
                        GetCurrentBalance = new CurrentBalanceRequestModelXML.GetCurrentBalanceRequest
                        {
                            AgentCode = currentBalanceRequestModel.AgentCode,
                            UserId = currentBalanceRequestModel.UserId,
                            AgentSessionId = currentBalanceRequestModel.AgentSessionId,
                            Signature = currentBalanceRequestModel.Signature
                        }
                    }
                };

                string serializedXML = string.Empty;
                XmlSerializer serializer = new XmlSerializer(typeof(CurrentBalanceRequestModelXML.Envelope));
                var namespaces = new XmlSerializerNamespaces();
                namespaces.Add("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
                namespaces.Add("tem", "http://tempuri.org/");
                var settings = new XmlWriterSettings { OmitXmlDeclaration = true };
                using (StringWriter stringWriter = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
                    {
                        serializer.Serialize(xmlWriter, currentBalanceRequestXML, namespaces);
                        serializedXML = stringWriter.ToString();
                    }
                }
                var apiResponseData = await _apiHandler.SOAPPostCall<CurrentBalanceResponseModelXML.Envelope>("https://sunrise.iremit.com.my/SendWSV5/txnservice.asmx", serializedXML);

                if (apiResponseData.IsSuccess)
                {
                    if (apiResponseData.IsSuccess)
                    {
                        var deserializer = new XmlSerializer(typeof(CurrentBalanceResponseModelXML.Envelope));
                        using (var reader = new StringReader(apiResponseData.Response))
                        {
                            var responseModel = (CurrentBalanceResponseModelXML.Envelope)deserializer.Deserialize(reader);
                            response.IsSuccess = true;
                            response.GetCurrentBalanceResult = responseModel.Body?.GetCurrentBalanceResponse?.GetCurrentBalanceResult;
                        }
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
                var echoRequestXML = new EchoRequestModelXML.Envelope
                {
                    Body = new EchoRequestModelXML.Body
                    {
                        GetEcho = new EchoRequestModelXML.GetEcho
                        {
                            AGENT_CODE = echoRequestModel.AgentCode,
                            USER_ID = echoRequestModel.UserId,
                            AGENT_SESSION_ID = echoRequestModel.AgentSessionId,
                            SIGNATURE = echoRequestModel.Signature
                        }
                    }
                };

                string serializedXML = string.Empty;
                XmlSerializer serializer = new XmlSerializer(typeof(EchoRequestModelXML.Envelope));
                var namespaces = new XmlSerializerNamespaces();
                namespaces.Add("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
                namespaces.Add("tem", "http://tempuri.org/");
                var settings = new XmlWriterSettings { OmitXmlDeclaration = true };
                using (StringWriter stringWriter = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
                    {
                        serializer.Serialize(xmlWriter, echoRequestXML, namespaces);
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
                var exRateRequestXML = new ExRateRequestModelXML.Envelope
                {
                    Body = new ExRateRequestModelXML.Body
                    {
                        GetEXRate = new ExRateRequestModelXML.GetEXRateRequest
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
                XmlSerializer serializer = new XmlSerializer(typeof(ExRateRequestModelXML.Envelope));
                var namespaces = new XmlSerializerNamespaces();
                namespaces.Add("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
                namespaces.Add("tem", "http://tempuri.org/");
                var settings = new XmlWriterSettings { OmitXmlDeclaration = true };
                using (StringWriter stringWriter = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
                    {
                        serializer.Serialize(xmlWriter, exRateRequestXML, namespaces);
                        serializedXML = stringWriter.ToString();
                    }
                }
                var apiResponseData = await _apiHandler.SOAPPostCall<ExRateResponseModelXML.Envelope>("https://sunrise.iremit.com.my/SendWSV5/txnservice.asmx", serializedXML);

                if (apiResponseData.IsSuccess)
                {
                    if (apiResponseData.IsSuccess)
                    {
                        var deserializer = new XmlSerializer(typeof(ExRateResponseModelXML.Envelope));
                        using (var reader = new StringReader(apiResponseData.Response))
                        {
                            var responseModel = (ExRateResponseModelXML.Envelope)deserializer.Deserialize(reader);
                            response.IsSuccess = true;
                            response.GetEXRateResult = responseModel.Body?.GetEXRateResponse?.GetEXRateResult;
                        }
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

        public async Task<SendTransactionResponseModel> SendTransactionRequest(SendTransactionRequestModel sendTransactionRequestModel)
        {
            SendTransactionResponseModel response = new SendTransactionResponseModel();
            try
            {
                var sendTransactionRequestXML = new SendTransactionRequestModelXML.Envelope
                {
                    Body = new SendTransactionRequestModelXML.Body
                    {
                        SendTransactionObject = new SendTransactionRequestModelXML.SendTransaction
                        {
                            AgentTxnId = sendTransactionRequestModel.AgentTxnId,
                            AuthorizedRequired = sendTransactionRequestModel.AuthorizedRequired,
                            BankAccountNumber = sendTransactionRequestModel.BankAccountNumber,
                            BankBranchName = sendTransactionRequestModel.BankBranchName,
                            BankId = sendTransactionRequestModel.BankId,
                            BankName = sendTransactionRequestModel.BankName,
                            CalcBy = sendTransactionRequestModel.CalcBy,
                            LocationId = sendTransactionRequestModel.LocationId,
                            OurServiceCharge = sendTransactionRequestModel.OurServiceCharge,
                            PaymentMode = sendTransactionRequestModel.PaymentMode,
                            PurposeOfRemittance = sendTransactionRequestModel.PurposeOfRemittance,
                            ReceiverAddress = sendTransactionRequestModel.ReceiverAddress,
                            ReceiverCity = sendTransactionRequestModel.ReceiverCity,
                            ReceiverContactNumber = sendTransactionRequestModel.ReceiverContactNumber,
                            ReceiverCountry = sendTransactionRequestModel.ReceiverCountry,
                            ReceiverFirstName = sendTransactionRequestModel.ReceiverFirstName,
                            ReceiverLastName = sendTransactionRequestModel.ReceiverLastName,
                            ReceiverMiddleName = sendTransactionRequestModel.ReceiverMiddleName,
                            RelationshipToBeneficiary = sendTransactionRequestModel.RelationshipToBeneficiary,
                            SenderAddress = sendTransactionRequestModel.SenderAddress,
                            SenderCity = sendTransactionRequestModel.SenderCity,
                            SenderCountry = sendTransactionRequestModel.SenderCountry,
                            SenderDateOfBirth = sendTransactionRequestModel.SenderDateOfBirth,
                            SenderFirstName = sendTransactionRequestModel.SenderFirstName,
                            SenderGender = sendTransactionRequestModel.SenderGender,
                            SenderIdExpireDate = sendTransactionRequestModel.SenderIdExpireDate,
                            SenderIdIssueDate = sendTransactionRequestModel.SenderIdIssueDate,
                            SenderIdNumber = sendTransactionRequestModel.SenderIdNumber,
                            SenderIdType = sendTransactionRequestModel.SenderIdType,
                            SenderLastName = sendTransactionRequestModel.SenderLastName,
                            SenderMiddleName = sendTransactionRequestModel.SenderMiddleName,
                            SenderMobile = sendTransactionRequestModel.SenderMobile,
                            SenderNationality = sendTransactionRequestModel.SenderNationality,
                            SenderOccupation = sendTransactionRequestModel.SenderOccupation,
                            SettlementDollarRate = sendTransactionRequestModel.SettlementDollarRate,
                            SourceOfFund = sendTransactionRequestModel.SourceOfFund,
                            TransactionExchangeRate = sendTransactionRequestModel.TransactionExchangeRate,
                            TransferAmount = sendTransactionRequestModel.TransferAmount
                        }
                    }
                };

                string serializedXML = string.Empty;
                XmlSerializer serializer = new XmlSerializer(typeof(SendTransactionRequestModelXML.Envelope));
                var namespaces = new XmlSerializerNamespaces();
                namespaces.Add("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
                namespaces.Add("tem", "http://tempuri.org/");
                var settings = new XmlWriterSettings { OmitXmlDeclaration = true };
                using (StringWriter stringWriter = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
                    {
                        serializer.Serialize(xmlWriter, sendTransactionRequestXML, namespaces);
                        serializedXML = stringWriter.ToString();
                    }
                }
                var apiResponseData = await _apiHandler.SOAPPostCall<SendTransactionRequestModelXML.Envelope>("https://sunrise.iremit.com.my/SendWSV5/txnservice.asmx", serializedXML);

                if (apiResponseData.IsSuccess)
                {
                    if (apiResponseData.IsSuccess)
                    {
                        var deserializer = new XmlSerializer(typeof(SendTransactionResponseXMLModel.Envelope));
                        using (var reader = new StringReader(apiResponseData.Response))
                        {
                            var responseModel = (SendTransactionResponseXMLModel.Envelope)deserializer.Deserialize(reader);
                            response.IsSuccess = true;
                            response.TransactionDetails = responseModel.Body?.SendTransactionResponse?.TransactionDetails;
                        }
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