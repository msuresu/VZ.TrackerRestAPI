using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace VZ.RestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITrackerRestService" in both code and config file together.
    [ServiceContract]
    public interface ITrackerRestService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "InsertUserActivity")]
        string InsertUserActivity(UserActivity objUserActiveity);
               
        [OperationContract]
        [WebInvoke(UriTemplate = "Test", Method = "GET")]
        string Test();
       
    }

    [DataContract]
    public class UserActivity
    {
        [DataMember]
        public string ActivityType { get; set; }
        [DataMember]
        public string DomainName { get; set; }
    }

    public class TrackerModule
    {

        public void InsertUserActivity(UserActivity objUserActiveity)
        {

        }
    }
}
