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
        private readonly string apiPassword = "FEDERAL1";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="apiHandler"></param>
        public APIServices(IAPIHandler apiHandler, IMapper mapper, ICommonUtility commonUtility)
        {
            _apiHandler = apiHandler;
            _mapper = mapper;
            _commonUtility = commonUtility;
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
                    var deserializer = new XmlSerializer(typeof(AuthorizedConfirmedResponseModelXML.Envelope));
                    using (var reader = new StringReader(apiResponseData.Response))
                    {
                        var responseModel = (AuthorizedConfirmedResponseModelXML.Envelope)deserializer.Deserialize(reader);
                        response.IsSuccess = true;
                        response.GetCurrentBalanceResult = responseModel.Body?.AuthorizedConfirmResponse?.AuthorisedConfirmDetails;
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
    }
}