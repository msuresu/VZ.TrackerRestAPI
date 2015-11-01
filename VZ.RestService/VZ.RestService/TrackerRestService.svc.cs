using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace VZ.RestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TrackerRestService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TrackerRestService.svc or TrackerRestService.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements
    (RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class TrackerRestService : ITrackerRestService
    {
       
        public string Test()
        {
            return DateTime.Now.ToShortDateString();
        }


        public string InsertUserActivity(UserActivity objUserActiveity)
        {
            TrackerModule Objtracker = new TrackerModule();
            Objtracker.InsertUserActivity(objUserActiveity);
            return "Sucess";
        }
        public string InsertPagePerformance(PagePerformance objPerformance)
        {
            TrackerModule objTracker = new TrackerModule();
            objTracker.InsertPagePerformance(objPerformance);
            return "Success";
        }
        public List<PagePerformance> GetPagePerformance()
        {
            TrackerModule objTracker = new TrackerModule();
            return objTracker.GetPagePerformance().PagePerformance.ToList();
        }
        public string UserActivityPerformance(UserActivityPerformance objActPer)
        {
            TrackerModule objTracker = new TrackerModule();
            objTracker.InsertUserActivity(objActPer.Activity);
            objTracker.InsertPagePerformance(objActPer.Performance);
            return "Success";
        }
        public TrackerDashboard GetDasboardContent(string DomainName)
        {
            TrackerModule Objtracker = new TrackerModule();
            return Objtracker.GetDasboardConetnt(DomainName);
        }
    }

}
