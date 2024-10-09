using LaxmiSunriseBank.CommonUtlilies;
using LaxmiSunriseBank.Models.LaxmiSunriseBank;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaxmiSunriseBank.API.APIServices
{
    public interface IAPIServices
    {
        //Job
        public Task<AuthorizedConfirmedResponseModel> AuthorisedConfirmRequest(AuthorizedConfirmedRequestModel authorizedConfirmedRequestModel);

        //Not yet confirm
        public Task<ReconcileReportResponseModel> ReconcileReport(ReconcileReportRequestModel reconcileReportRequestModel);

        //Job both public
        public Task<QueryTXNStatusResponseModel> QueryTXNStatus(QueryTXNStatusRequestModel queryTXNStatusRequestModel);
    }
}
