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
using LaxmiSunriseBank.CommonUtlilies;

namespace LaxmiSunriseBank.API.APIServices
{
    public class APIServices : IAPIServices
    {
        private readonly IAPIHandler _apiHandler;
        private readonly ICommonUtility _commonUtility;
        private readonly IMapper _mapper;
        private readonly string apiPassword = "123";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="apiHandler"></param>
        public APIServices(IAPIHandler apiHandler, IMapper mapper,ICommonUtility commonUtility)
        {
            _apiHandler = apiHandler;
            _mapper = mapper;
            _commonUtility = commonUtility;
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
                            Signature = await _commonUtility.GenerateSHA256Signature(amendmentRequestModel.AgentCode, amendmentRequestModel.UserId, amendmentRequestModel.AgentSessionId, amendmentRequestModel.PinNo, amendmentRequestModel.AmendmentField, amendmentRequestModel.AmendmentValue, apiPassword),
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

        public async Task<AuthorizedConfirmedResponseModel> AuthorisedConfirmRequest(AuthorizedConfirmedRequestModel authorizedConfirmedRequestModel)
        {
            AuthorizedConfirmedResponseModel response = new AuthorizedConfirmedResponseModel();
            var mappedRequestModel = _mapper.Map<AuthorizedConfirmedRequestModelXML.AuthorizedConfirmModel>(authorizedConfirmedRequestModel);
            mappedRequestModel.Signature = await _commonUtility.GenerateSHA256Signature(authorizedConfirmedRequestModel.AgentCode, authorizedConfirmedRequestModel.UserId, authorizedConfirmedRequestModel.PinNo, authorizedConfirmedRequestModel.AgentSessionId, apiPassword); 
            try
            {
                var currentBalanceRequestXML = new AuthorizedConfirmedRequestModelXML.Envelope
                {
                    Body = new AuthorizedConfirmedRequestModelXML.Body
                    {
                        AuthorizedConfirmModel = mappedRequestModel
                    }
                };

                string serializedXML = string.Empty;
                XmlSerializer serializer = new XmlSerializer(typeof(AuthorizedConfirmedRequestModelXML.Envelope));
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
                var apiResponseData = await _apiHandler.SOAPPostCall<AuthorizedConfirmedResponseModelXML.Envelope>("https://sunrise.iremit.com.my/SendWSV5/txnservice.asmx", serializedXML);

                if (apiResponseData.IsSuccess)
                {
                    if (apiResponseData.IsSuccess)
                    {
                        var deserializer = new XmlSerializer(typeof(AuthorizedConfirmedResponseModelXML.Envelope));
                        using (var reader = new StringReader(apiResponseData.Response))
                        {
                            var responseModel = (AuthorizedConfirmedResponseModelXML.Envelope)deserializer.Deserialize(reader);
                            response.IsSuccess = true;
                            response.GetCurrentBalanceResult = responseModel.Body?.AuthorizedConfirmResponse?.AuthorisedConfirmDetails;
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

        public async Task<CancelTransactionResponseModel> CancelTransaction(CancelTransactionRequestModel cancelTransactionRequestModel)
        {
            CancelTransactionResponseModel response = new CancelTransactionResponseModel();

            var mapper = _mapper.Map<CancelTransactionRequestModelXML.CancelTransaction>(cancelTransactionRequestModel);
            try
            {
                var cancelTransactionRequestXML = new CancelTransactionRequestModelXML.Envelope
                {
                    Body = new CancelTransactionRequestModelXML.Body
                    {
                        CancelTransaction = mapper
                    }
                };

                string serializedXML = string.Empty;
                XmlSerializer serializer = new XmlSerializer(typeof(CancelTransactionRequestModelXML.Envelope));
                var namespaces = new XmlSerializerNamespaces();
                namespaces.Add("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
                namespaces.Add("tem", "http://tempuri.org/");
                var settings = new XmlWriterSettings { OmitXmlDeclaration = true };
                using (StringWriter stringWriter = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
                    {
                        serializer.Serialize(xmlWriter, cancelTransactionRequestXML, namespaces);
                        serializedXML = stringWriter.ToString();
                    }
                }
                var apiResponseData = await _apiHandler.SOAPPostCall<CancelTransactionResponseModelXML.Envelope>("https://sunrise.iremit.com.my/SendWSV5/txnservice.asmx", serializedXML);

                if (apiResponseData.IsSuccess)
                {
                    if (apiResponseData.IsSuccess)
                    {
                        var deserializer = new XmlSerializer(typeof(CancelTransactionResponseModelXML.Envelope));
                        using (var reader = new StringReader(apiResponseData.Response))
                        {
                            var responseModel = (CancelTransactionResponseModelXML.Envelope)deserializer.Deserialize(reader);
                            response.IsSuccess = true;
                            response.CancelTransactionResult = responseModel.Body?.CancelTransactionResponse?.CancelTransactionResult;
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
            var mappedRequestModel = _mapper.Map<AgentListRequestModelXML.GetAgentList>(agentListRequestModel);
            mappedRequestModel.Signature = await _commonUtility.GenerateSHA256Signature(agentListRequestModel.AgentCode, agentListRequestModel.UserId, agentListRequestModel.AgentSessionId, agentListRequestModel.PaymentType, agentListRequestModel.PayoutCountry, apiPassword);
            try
            {
                var agentListRequestXML = new AgentListRequestModelXML.Envelope
                {
                    Body = new AgentListRequestModelXML.Body
                    {
                        GetAgentList = mappedRequestModel
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
            var mappedRequestModel = _mapper.Map<BankListRequestModelXML.GetBankList>(bankListRequestModel);
            mappedRequestModel.Signature = await _commonUtility.GenerateSHA256Signature(bankListRequestModel.AgentCode, bankListRequestModel.UserId, bankListRequestModel.AgentSessionId, apiPassword);
            try
            {
                var bankListRequestXML = new BankListRequestModelXML.Envelope
                {
                    Body = new BankListRequestModelXML.Body
                    {
                        GetBankList = mappedRequestModel
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
            var mappedRequestModel = _mapper.Map<CurrentBalanceRequestModelXML.GetCurrentBalanceRequest>(currentBalanceRequestModel);
            mappedRequestModel.Signature = await _commonUtility.GenerateSHA256Signature(currentBalanceRequestModel.AgentCode, currentBalanceRequestModel.UserId, currentBalanceRequestModel.AgentSessionId, apiPassword);

            try
            {
                var currentBalanceRequestXML = new CurrentBalanceRequestModelXML.Envelope
                {
                    Body = new CurrentBalanceRequestModelXML.Body
                    {
                        GetCurrentBalance = mappedRequestModel
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
            var mappedRequestModel = _mapper.Map<EchoRequestModelXML.GetEcho>(echoRequestModel);
            mappedRequestModel.Signature = await _commonUtility.GenerateSHA256Signature(echoRequestModel.AgentCode, echoRequestModel.UserId, echoRequestModel.AgentSessionId, apiPassword);

            try
            {
                var echoRequestXML = new EchoRequestModelXML.Envelope
                {
                    Body = new EchoRequestModelXML.Body
                    {
                        GetEcho = mappedRequestModel
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
            var mappedRequestModel = _mapper.Map<ExRateRequestModelXML.GetEXRateRequest>(exRateRequestModel);
            mappedRequestModel.Signature = await _commonUtility.GenerateSHA256Signature(exRateRequestModel.AgentCode, exRateRequestModel.UserId, exRateRequestModel.AgentSessionId, exRateRequestModel.TransferAmount, exRateRequestModel.PaymentMode, exRateRequestModel.CalcBy, exRateRequestModel.LocationId, exRateRequestModel.PayoutCountry,apiPassword);

            try
            {
                var exRateRequestXML = new ExRateRequestModelXML.Envelope
                {
                    Body = new ExRateRequestModelXML.Body
                    {
                        GetEXRate = mappedRequestModel
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

        public async Task<ReconcileReportResponseModel> ReconcileReport(ReconcileReportRequestModel reconcileReportRequestModel)
        {
            ReconcileReportResponseModel response = new ReconcileReportResponseModel();

            var reconcileMapper = _mapper.Map<ReconcileReportRequestModelXML.ReconcileReportRequest>(reconcileReportRequestModel);
            reconcileMapper.Signature = await _commonUtility.GenerateSHA256Signature(reconcileReportRequestModel.AgentCode, reconcileReportRequestModel.UserId, reconcileReportRequestModel.AgentSessionId, reconcileReportRequestModel.ReportType, reconcileReportRequestModel.FromDate, reconcileReportRequestModel.FromDateTime, reconcileReportRequestModel.ToDate, reconcileReportRequestModel.ToDateTime, reconcileReportRequestModel.ShowIncremental, apiPassword);
            try
            {
                var amendmentRequestXML = new ReconcileReportRequestModelXML.Envelope
                {
                    Body = new ReconcileReportRequestModelXML.Body
                    {
                        ReconcileReport = reconcileMapper
                    }
                };

                string serializedXML = string.Empty;
                XmlSerializer serializer = new XmlSerializer(typeof(ReconcileReportRequestModelXML.Envelope));
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
                var apiResponseData = await _apiHandler.SOAPPostCall<ReconcileReportResponseModelXML.Envelope>("https://sunrise.iremit.com.my/SendWSV5/txnservice.asmx", serializedXML);

                if (apiResponseData.IsSuccess)
                {
                    if (apiResponseData.IsSuccess)
                    {
                        var deserializer = new XmlSerializer(typeof(ReconcileReportResponseModelXML.Envelope));
                        using (var reader = new StringReader(apiResponseData.Response))
                        {
                            var responseModel = (ReconcileReportResponseModelXML.Envelope)deserializer.Deserialize(reader);
                            response.IsSuccess = true;
                            response.ReturnTransReport = responseModel.Body?.ReconcileReportResponse?.ReconcileReportResult?.ReturnTransReport;
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