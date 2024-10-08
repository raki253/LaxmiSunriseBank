﻿using LaxmiSunriseBank.Models.LaxmiSunriseBank;
using LaxmiSunriseBank.Services.API.APIHandler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using AutoMapper;
using LaxmiSunriseBank.CommonUtility;
using LaxmiSunriseBank.CommonUtlilies;

namespace LaxmiSunriseBank.Public
{
    public class ServiceMethods : IServiceMethods
    {
        private readonly IAPIHandler _apiHandler;
        private readonly IMapper _mapper;
        private readonly ICommonUtility _commonUtility;
        private readonly string apiPassword = "FEDERAL1";

        public ServiceMethods(IAPIHandler apiHandler, IMapper mapper, ICommonUtility commonUtility)
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
                    var deserializer = new XmlSerializer(typeof(AmendmentResponseModelXML.Envelope));
                    using (var reader = new StringReader(apiResponseData.Response))
                    {
                        var responseModel = (AmendmentResponseModelXML.Envelope)deserializer.Deserialize(reader);
                        response.IsSuccess = true;
                        response.AmendmentRequestResult = responseModel.Body?.AmendmentRequestResponse?.AmendmentRequestResult;
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
                    var deserializer = new XmlSerializer(typeof(CancelTransactionResponseModelXML.Envelope));
                    using (var reader = new StringReader(apiResponseData.Response))
                    {
                        var responseModel = (CancelTransactionResponseModelXML.Envelope)deserializer.Deserialize(reader);
                        response.IsSuccess = true;
                        response.CancelTransactionResult = responseModel.Body?.CancelTransactionResponse?.CancelTransactionResult;
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
                    var deserializer = new XmlSerializer(typeof(BankListResponseModelXML.Envelope));
                    using (var reader = new StringReader(apiResponseData.Response))
                    {
                        var responseModel = (BankListResponseModelXML.Envelope)deserializer.Deserialize(reader);
                        response.IsSuccess = true;
                        response.BankList = responseModel.Body?.GetBankListResponse?.GetBankListResult?.Return_BankList;
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
                    var deserializer = new XmlSerializer(typeof(CurrentBalanceResponseModelXML.Envelope));
                    using (var reader = new StringReader(apiResponseData.Response))
                    {
                        var responseModel = (CurrentBalanceResponseModelXML.Envelope)deserializer.Deserialize(reader);
                        response.IsSuccess = true;
                        response.GetCurrentBalanceResult = responseModel.Body?.GetCurrentBalanceResponse?.GetCurrentBalanceResult;
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
            mappedRequestModel.Signature = await _commonUtility.GenerateSHA256Signature(exRateRequestModel.AgentCode, exRateRequestModel.UserId, exRateRequestModel.AgentSessionId, exRateRequestModel.TransferAmount, exRateRequestModel.PaymentMode, exRateRequestModel.CalcBy, exRateRequestModel.LocationId, exRateRequestModel.PayoutCountry, apiPassword);

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
                    var deserializer = new XmlSerializer(typeof(ExRateResponseModelXML.Envelope));
                    using (var reader = new StringReader(apiResponseData.Response))
                    {
                        var responseModel = (ExRateResponseModelXML.Envelope)deserializer.Deserialize(reader);
                        response.IsSuccess = true;
                        response.GetEXRateResult = responseModel.Body?.GetEXRateResponse?.GetEXRateResult;
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

        public async Task<QueryTXNStatusResponseModel> QueryTXNStatus(QueryTXNStatusRequestModel queryTXNStatusRequestModel)
        {
            QueryTXNStatusResponseModel response = new QueryTXNStatusResponseModel();
            var mappedRequestModel = _mapper.Map<QueryTXNStatusRequestModelXML.QueryTXNStatusRequest>(queryTXNStatusRequestModel);
            mappedRequestModel.Signature = await _commonUtility.GenerateSHA256Signature(queryTXNStatusRequestModel.AgentCode, queryTXNStatusRequestModel.UserId, queryTXNStatusRequestModel.PinNo, queryTXNStatusRequestModel.AgentSessionId, queryTXNStatusRequestModel.AgentTxnId, apiPassword);
            try
            {
                var queryTXNStatusRequestXML = new QueryTXNStatusRequestModelXML.Envelope
                {
                    Body = new QueryTXNStatusRequestModelXML.Body
                    {
                        QueryTXNStatus = mappedRequestModel
                    }
                };

                string serializedXML = string.Empty;
                XmlSerializer serializer = new XmlSerializer(typeof(QueryTXNStatusRequestModelXML.Envelope));
                var namespaces = new XmlSerializerNamespaces();
                namespaces.Add("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
                namespaces.Add("tem", "http://tempuri.org/");
                var settings = new XmlWriterSettings { OmitXmlDeclaration = true };
                using (StringWriter stringWriter = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
                    {
                        serializer.Serialize(xmlWriter, queryTXNStatusRequestXML, namespaces);
                        serializedXML = stringWriter.ToString();
                    }
                }
                var apiResponseData = await _apiHandler.SOAPPostCall<QueryTXNStatusResponseModelXML.Envelope>("https://sunrise.iremit.com.my/SendWSV5/txnservice.asmx", serializedXML);

                if (apiResponseData.IsSuccess)
                {
                    var deserializer = new XmlSerializer(typeof(QueryTXNStatusResponseModelXML.Envelope));
                    using (var reader = new StringReader(apiResponseData.Response))
                    {
                        var responseModel = (QueryTXNStatusResponseModelXML.Envelope)deserializer.Deserialize(reader);
                        response.IsSuccess = true;
                        response.QueryTXNStatusResult = responseModel.Body?.QueryTXNStatusResponse?.QueryTXNStatusResult;
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

            var mappedRequestModel = _mapper.Map<ReconcileReportRequestModelXML.ReconcileReportRequest>(reconcileReportRequestModel);
            mappedRequestModel.Signature = await _commonUtility.GenerateSHA256Signature(reconcileReportRequestModel.AgentCode, reconcileReportRequestModel.UserId, reconcileReportRequestModel.AgentSessionId, reconcileReportRequestModel.ReportType, reconcileReportRequestModel.FromDate, reconcileReportRequestModel.FromDateTime, reconcileReportRequestModel.ToDate, reconcileReportRequestModel.ToDateTime, reconcileReportRequestModel.ShowIncremental, apiPassword);
            try
            {
                var amendmentRequestXML = new ReconcileReportRequestModelXML.Envelope
                {
                    Body = new ReconcileReportRequestModelXML.Body
                    {
                        ReconcileReport = mappedRequestModel
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
                    var deserializer = new XmlSerializer(typeof(ReconcileReportResponseModelXML.Envelope));
                    using (var reader = new StringReader(apiResponseData.Response))
                    {
                        var responseModel = (ReconcileReportResponseModelXML.Envelope)deserializer.Deserialize(reader);
                        response.IsSuccess = true;
                        response.ReturnTransReport = responseModel.Body?.ReconcileReportResponse?.ReconcileReportResult?.ReturnTransReport;
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
            var mappedRequestModel = _mapper.Map<SendTransactionRequestModelXML.SendTransaction>(sendTransactionRequestModel);
            mappedRequestModel.Signature = await _commonUtility.GenerateSHA256Signature(sendTransactionRequestModel.AgentCode, sendTransactionRequestModel.UserId, sendTransactionRequestModel.AgentSessionId, sendTransactionRequestModel.AgentTxnId, sendTransactionRequestModel.LocationId, sendTransactionRequestModel.SenderFirstName, sendTransactionRequestModel.SenderMiddleName, sendTransactionRequestModel.SenderLastName, sendTransactionRequestModel.SenderGender, sendTransactionRequestModel.SenderAddress, sendTransactionRequestModel.SenderCity, sendTransactionRequestModel.SenderCountry, sendTransactionRequestModel.SenderIdType, sendTransactionRequestModel.SenderIdNumber, sendTransactionRequestModel.SenderIdIssueDate, sendTransactionRequestModel.SenderIdExpireDate, sendTransactionRequestModel.SenderDateOfBirth, sendTransactionRequestModel.SenderMobile, sendTransactionRequestModel.SourceOfFund, sendTransactionRequestModel.SenderOccupation, sendTransactionRequestModel.SenderNationality, sendTransactionRequestModel.ReceiverFirstName, sendTransactionRequestModel.ReceiverMiddleName, sendTransactionRequestModel.ReceiverLastName, sendTransactionRequestModel.ReceiverAddress, sendTransactionRequestModel.ReceiverCity, sendTransactionRequestModel.ReceiverCountry, sendTransactionRequestModel.ReceiverContactNumber, sendTransactionRequestModel.RelationshipToBeneficiary, sendTransactionRequestModel.PaymentMode, sendTransactionRequestModel.BankId, sendTransactionRequestModel.BankName, sendTransactionRequestModel.BankBranchName, sendTransactionRequestModel.BankAccountNumber, sendTransactionRequestModel.CalcBy, sendTransactionRequestModel.TransferAmount.ToString(), sendTransactionRequestModel.OurServiceCharge.ToString(), sendTransactionRequestModel.TransactionExchangeRate.ToString(), sendTransactionRequestModel.SettlementDollarRate.ToString(), sendTransactionRequestModel.PurposeOfRemittance, sendTransactionRequestModel.AuthorizedRequired, apiPassword);
            try
            {
                var sendTransactionRequestXML = new SendTransactionRequestModelXML.Envelope
                {
                    Body = new SendTransactionRequestModelXML.Body
                    {
                        SendTransactionObject = mappedRequestModel
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
                    var deserializer = new XmlSerializer(typeof(SendTransactionResponseXMLModel.Envelope));
                    using (var reader = new StringReader(apiResponseData.Response))
                    {
                        var responseModel = (SendTransactionResponseXMLModel.Envelope)deserializer.Deserialize(reader);
                        response.IsSuccess = true;
                        response.TransactionDetails = responseModel.Body?.SendTransactionResponse?.TransactionDetails;
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
