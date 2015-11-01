using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Hosting;
using System.Xml;
using System.Xml.Serialization;

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
        [WebInvoke(Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "InsertPagePerformance")]
        string InsertPagePerformance(PagePerformance objPerformance);
        [OperationContract]
        [WebInvoke(Method = "GET",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "GetPagePerformance")]
        List<PagePerformance> GetPagePerformance();

        [OperationContract]
        [WebInvoke(UriTemplate = "Test", Method = "GET")]
        string Test();
        [OperationContract]
        [WebInvoke(Method = "GET",
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "GetDashboard/{DomainName}")]
        TrackerDashboard GetDasboardContent(string DomainName);

    }
    [Serializable]
    [DataContract]
    public class UserActivity
    {
        [DataMember]
        public string ActivityType { get; set; }
        [DataMember]
        public string TrackerId { get; set; }
        [DataMember]
        public string DomainName { get; set; }
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public string PagePath { get; set; }
        [DataMember]
        public string PageTitle { get; set; }
        [DataMember]
        public string PageType { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string IPAddress { get; set; }
        [DataMember]
        public string SessionId { get; set; }
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public string ElementName { get; set; }
        [DataMember]
        public string ElementType { get; set; }

    }
    [DataContract]
    public class TrackerDashboard
    {
        [DataMember]
        public List<SiteMetrics> objSiteMetrics { get; set; }
        [DataMember]
        public TotalVisits objTotalVisits { get; set; }
        [DataMember]
        public List<PagesPerViews> objPagesPerViews { get; set; }
        [DataMember]
        public List<UniqueVisits> objUniqueVisits { get; set; }
        [DataMember]
        public VisterType objVisterType { get; set; }
        [DataMember]
        public List<TopContents> objTopContents { get; set; }
        [DataMember]
        public List<TopContents> objActionContents { get; set; }
    }
    public class SiteMetrics
    {
        public string Date { get; set; }
        public int VisitCount { get; set; }
    }

    public class TotalVisits
    {

        public int VisitCount { get; set; }
        public List<SiteMetrics> SiteMetricDataPoints { get; set; }
    }

    public class PagesPerViews
    {
        public string TrackerId { get; set; }
        public int PagePerViewCount { get; set; }
        public string Date { get; set; }
    }

    public class UniqueVisits
    {
        public string UserId { get; set; }
        public string Date { get; set; }
        public int UniqueCount { get; set; }
    }
    public class TopContents
    {
        public string PagePath { get; set; }
        public string Date { get; set; }
        public string PageTitle { get; set; }
        public int PageViwes { get; set; }
        public int Visits { get; set; }
        public int UniquePageViwes { get; set; }
        public string ElementType { get; set; }
        public string ElementName { get; set; }

        
    }
    public class VisterType
    {
        public double Newvistor { get; set; }
        public double Repeatvistor { get; set; }
        public double NewvistorPer { get; set; }
        public double RepeatvistorPer { get; set; }
    }
    [Serializable]
    [DataContract]
    public class PagePerformance
    {
        [DataMember]
        public string PagePath { get; set; }
        [DataMember]
        public string StartTime { get; set; }
        [DataMember]
        public string EndTime { get; set; }
        [DataMember]
        public string LoadTime { get; set; }
        [DataMember]
        public string BrowserInfo { get; set; }

    }

    public class TrackerModule
    {
        public string USERACTIVITYFILEPATH = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", "XML.txt");//Server.MapPath("~/App_Data/CustomerData.json");"D:\\XML.txt";
        public string USERACTIVITYTEMPFILEPATH = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", "XMLTemp.txt");//"D:\\XMLTemp.txt";
        public string PAGEPERFORMANCEFILEPATH = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", "PagePerformanceXML.txt");//"D:\\PagePerformanceXML.txt";
        public string PAGEPERFORMANCETEMPFILEPATH = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", "PagePerformanceXMLTemp.txt");// "D:\\PagePerformanceXMLTemp.txt";

        public void InsertUserActivity(UserActivity objUserActivity)
        {
            SerializeUserActivityObject(objUserActivity, USERACTIVITYTEMPFILEPATH);
        }
        public void InsertPagePerformance(PagePerformance objPagePerformance)
        {
            SerializePagePerformanceObject(objPagePerformance, PAGEPERFORMANCETEMPFILEPATH);
        }





        [Serializable]
        public class UserActivities
        {
            [XmlElement]
            public UserActivity[] UserActivity { get; set; }
        }

        [Serializable]
        public class PagePerformances
        {
            [XmlElement]
            public PagePerformance[] PagePerformance { get; set; }
        }




        //[Serializable]
        //public class PagePerformance
        //{
        //    public string PagePath { get; set; }
        //    public string StartTime { get; set; }
        //    public string EndTime { get; set; }
        //    public string LoadTime { get; set; }
        //}

      


        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your app description page.";

        //    UserActivity objUserActivity = null;
        //    objUserActivity = new UserActivity()
        //    {
        //        TrackerId = "bcd2345",
        //        DomainName = "localhost",
        //        Date = DateTime.Now.AddDays(-1),
        //        PagePath = "/Home/Index",
        //        PageTitle = "Dashboard",
        //        PageType = "Home",
        //        State = "NewYork",
        //        Country = "US"
        //    };

        //    PagePerformance objPagePerformance = null;
        //    objPagePerformance = new PagePerformance
        //    {
        //        PagePath = "/Home/Index",
        //        StartTime = "10:20:00",
        //        EndTime = "10:20:30",
        //        LoadTime = "00:00:30"
        //    };

        //    SerializeUserActivityObject(objUserActivity, USERACTIVITYTEMPFILEPATH);

        //    DeSerializeObject<UserActivities>(USERACTIVITYFILEPATH);

        //    SerializePagePerformanceObject(objPagePerformance, PAGEPERFORMANCETEMPFILEPATH);

        //    DeSerializeObject<PagePerformances>(PAGEPERFORMANCEFILEPATH);

        //    GetSiteMetrics();

        //    GetTotalVisits();

        //    GetPagesPerViews();

        //    GetUniqueVisits();

        //    GetPageLoadTime();

        //    return View();
        //}

        public TrackerDashboard GetDasboardConetnt(string DomainName)
        {
            UserActivities lstUserActivities = DeSerializeObject<UserActivities>(USERACTIVITYFILEPATH);
            lstUserActivities.UserActivity = lstUserActivities.UserActivity.Where(k => k.DomainName == DomainName).ToArray();
            //  lstUserActivities = lstUserActivities.UserActivity.Where(k=>k.DomainName==DomainName);
            TrackerDashboard objTrackerDashboard = new TrackerDashboard();
            objTrackerDashboard.objSiteMetrics = GetSiteMetrics(lstUserActivities);
            objTrackerDashboard.objUniqueVisits = GetUniqueVisits(lstUserActivities);
            objTrackerDashboard.objPagesPerViews = GetPagesViews(lstUserActivities);
            objTrackerDashboard.objVisterType = GetVisitorType(lstUserActivities);
            objTrackerDashboard.objTopContents = GetActivityContents(lstUserActivities);
            objTrackerDashboard.objActionContents = GetActionContents(lstUserActivities);
            return objTrackerDashboard;
        }
        public List<TopContents> GetActionContents(UserActivities ltUserActivities, int pastDays = 7)
        {
            List<TopContents> lstTopContents = new List<TopContents>();
            UserActivities lstUserActivities = ltUserActivities;
            var checkDate = DateTime.Now.AddDays(-pastDays);

            //var arrDistinctDate = lstUserActivities.UserActivity.Where(x => Convert.ToDateTime(x.Date) >= checkDate).GroupBy(i => i.Date, (key, group) => group.First()).ToArray();
            var arrActivities = lstUserActivities.UserActivity.Where(x => !string.IsNullOrWhiteSpace(x.Date) && x.ActivityType == "Action").ToList();
            foreach (var activity in arrActivities)
            {
               
                TopContents content = new TopContents();
                content.Date = Convert.ToDateTime(activity.Date).Date.ToShortDateString(); //Convert.ToDateTime(activity.Date).Date.ToShortDateString();
                content.PagePath = activity.PagePath;
                content.PageTitle = activity.PageTitle;
                content.ElementName = activity.ElementName;
                content.ElementType = activity.ElementType;
                
                lstTopContents.Add(content);
            }

            return lstTopContents;
        }
        public List<TopContents> GetActivityContents(UserActivities ltUserActivities, int pastDays = 7)
        {
            List<TopContents> lstTopContents = new List<TopContents>();
            UserActivities lstUserActivities = ltUserActivities;
            var checkDate = DateTime.Now.AddDays(-pastDays);

            //var arrDistinctDate = lstUserActivities.UserActivity.Where(x => Convert.ToDateTime(x.Date) >= checkDate).GroupBy(i => i.Date, (key, group) => group.First()).ToArray();
            var arrActivities = lstUserActivities.UserActivity.Where(x => !string.IsNullOrWhiteSpace(x.Date) && Convert.ToDateTime(x.Date).Date >= checkDate.Date).GroupBy(x => new { Convert.ToDateTime(x.Date).Date, x.PagePath, x.PageTitle }).ToList();
            foreach (var activity in arrActivities)
            {
               // foreach(var values in activity)
               // {
                   // values.
               // }
                TopContents content = new TopContents();
                content.Date = Convert.ToDateTime(activity.Key.Date).Date.ToShortDateString(); //Convert.ToDateTime(activity.Date).Date.ToShortDateString();
                content.PagePath = activity.Key.PagePath;
                content.PageTitle = activity.Key.PageTitle;
                content.PageViwes = lstUserActivities.UserActivity.Where(x => !string.IsNullOrWhiteSpace(x.Date) && Convert.ToDateTime(x.Date).Date == Convert.ToDateTime(activity.Key.Date).Date).GroupBy(k => new { k.TrackerId, k.PagePath, k.PageTitle }).Count();
                content.UniquePageViwes = lstUserActivities.UserActivity.Where(x => !string.IsNullOrWhiteSpace(x.Date) && Convert.ToDateTime(x.Date).Date == Convert.ToDateTime(activity.Key.Date).Date).GroupBy(k => new { k.PagePath, k.PageTitle }).Count();
                content.Visits = lstUserActivities.UserActivity.Where(k => !string.IsNullOrWhiteSpace(k.Date) && Convert.ToDateTime(k.Date).Date == Convert.ToDateTime(activity.Key.Date).Date).GroupBy(l => l.TrackerId).Count();
                lstTopContents.Add(content);
            }

            return lstTopContents;
        }
        public VisterType GetVisitorType(UserActivities ltUserActivities, int pastDays = 7)
        {
            VisterType objVisterType = new VisterType();
            var ltUserActivities1 = ltUserActivities.UserActivity.GroupBy(k => k.IPAddress).ToList();
            double newViste = 0;
            double repeatViste = 0;
            foreach (var lstactivi in ltUserActivities1)
            {
                if (lstactivi.Count() == 1)
                {
                    newViste += 1;
                }
                else
                {
                    repeatViste += lstactivi.Count();
                }
            }
            objVisterType.Newvistor = newViste;
            objVisterType.Repeatvistor = repeatViste;
            double total = 0;
            if (newViste != 0 && repeatViste != 0)
            {
                total = newViste + repeatViste;
            }
            else if (newViste != 0)
            {
                total = newViste;
            }
            else
            {
                total = repeatViste;
            }
            if (newViste != 0 && total != 0)
            {
                objVisterType.NewvistorPer = Math.Round(((newViste / total) * 100), 2);
            }
            if (repeatViste != 0 && total != 0)
            {
                objVisterType.RepeatvistorPer = Math.Round(((repeatViste / total) * 100), 2);
            }

            return objVisterType;
        }

        public List<SiteMetrics> GetSiteMetrics(UserActivities ltUserActivities, int pastDays = 7)
        {
            UserActivities lstUserActivities = ltUserActivities;
            List<SiteMetrics> lstSiteMetrics = new List<SiteMetrics>();
            try
            {


                //  lstUserActivities = DeSerializeObject<UserActivities>(USERACTIVITYFILEPATH);
                var checkDate = DateTime.Now.AddDays(-pastDays);

                //var arrDistinctDate = lstUserActivities.UserActivity.Where(x => Convert.ToDateTime(x.Date) >= checkDate).GroupBy(i => i.Date, (key, group) => group.First()).ToArray();
                var arrDistinctDate = lstUserActivities.UserActivity.Where(x => !string.IsNullOrWhiteSpace(x.Date) && Convert.ToDateTime(x.Date).Date >= checkDate.Date).Select(x => Convert.ToDateTime(x.Date).Date).Distinct().ToArray();
                int intVisitCount = 0;
                foreach (var date in arrDistinctDate)
                {
                    if (date.Date != null)
                    {
                        //intVisitCount = lstUserActivities.UserActivity.Count(x => Convert.ToDateTime(x.Date).Date == date.Date);
                        intVisitCount = lstUserActivities.UserActivity.Where(k => !string.IsNullOrWhiteSpace(k.Date) && Convert.ToDateTime(k.Date).Date == date.Date).GroupBy(l => l.TrackerId).Count();
                        SiteMetrics objSiteMetrics = new SiteMetrics();

                        objSiteMetrics.Date = date.Date.ToString("MM-dd-yyyy");
                        objSiteMetrics.VisitCount = intVisitCount;
                        lstSiteMetrics.Add(objSiteMetrics);
                    }

                }
            }

            catch
            {
            }

            return lstSiteMetrics;

        }


        public List<PagesPerViews> GetPagesViews(UserActivities ltUserActivities, int pastDays = 7)
        {
            UserActivities lstUserActivities = ltUserActivities;

            List<PagesPerViews> lstPagePerViews = new List<PagesPerViews>();

            try
            {
                var checkDate = DateTime.Now.AddDays(-pastDays);
                // lstUserActivities = DeSerializeObject<UserActivities>(USERACTIVITYFILEPATH);
                //  var arrDistinctSessiionId = lstUserActivities.UserActivity.GroupBy(i => i.TrackerId, (key, group) => group.First()).ToArray();
                //   var arrDistinctSessiionId = lstUserActivities.UserActivity.GroupBy(i => i.PagePath, (key, group) => group.First()).ToArray();
                var arrDistinctSessiionId = lstUserActivities.UserActivity.Where(x => !string.IsNullOrWhiteSpace(x.Date) && Convert.ToDateTime(x.Date).Date >= checkDate.Date).Select(x => Convert.ToDateTime(x.Date).Date).Distinct().ToArray();
                //var arrDistinctDate = lstUserActivities.UserActivity.Where(x => Convert.ToDateTime(x.Date) >= checkDate).Select(x => Convert.ToDateTime(x.Date).Date).Distinct().ToArray();
                // var checkDate = DateTime.Now.AddDays(-pastDays);
                int intVisitCount = 0;
                foreach (var date in arrDistinctSessiionId)
                {
                    if (date != null)
                    {
                        intVisitCount = lstUserActivities.UserActivity.Count(x => !string.IsNullOrWhiteSpace(x.Date) && Convert.ToDateTime(x.Date).Date == date.Date);
                        PagesPerViews objPagePerViews = new PagesPerViews();

                        // objPagePerViews.TrackerId = distinctTrackerIdId.TrackerId;
                        objPagePerViews.PagePerViewCount = intVisitCount;
                        objPagePerViews.Date = Convert.ToDateTime(date.Date).ToString("MM-dd-yyyy");
                        lstPagePerViews.Add(objPagePerViews);
                    }
                }

            }
            catch
            {
            }

            return lstPagePerViews;
        }

        public List<PagesPerViews> GetPagesPerViews(UserActivities ltUserActivities, int pastDays = 7)
        {
            UserActivities lstUserActivities = ltUserActivities;

            List<PagesPerViews> lstPagePerViews = new List<PagesPerViews>();

            try
            {
                // lstUserActivities = DeSerializeObject<UserActivities>(USERACTIVITYFILEPATH);
                var arrDistinctSessiionId = lstUserActivities.UserActivity.GroupBy(i => i.TrackerId, (key, group) => group.First()).ToArray();
                var checkDate = DateTime.Now.AddDays(-pastDays);
                int intVisitCount = 0;
                foreach (var distinctTrackerIdId in arrDistinctSessiionId)
                {
                    if (distinctTrackerIdId.TrackerId != null)
                    {
                        intVisitCount = lstUserActivities.UserActivity.Where(x => !string.IsNullOrWhiteSpace(x.Date) && Convert.ToDateTime(x.Date).Date >= checkDate.Date).Count(x => x.TrackerId == distinctTrackerIdId.TrackerId);
                        PagesPerViews objPagePerViews = new PagesPerViews();

                        objPagePerViews.TrackerId = distinctTrackerIdId.TrackerId;
                        objPagePerViews.PagePerViewCount = intVisitCount;
                        objPagePerViews.Date = Convert.ToDateTime(distinctTrackerIdId.Date).ToString("MM-dd-yyyy");
                        lstPagePerViews.Add(objPagePerViews);
                    }
                }

            }
            catch
            {
            }

            return lstPagePerViews;
        }

        public List<UniqueVisits> GetUniqueVisits(UserActivities ltUserActivities, int pastDays = 7)
        {
            UserActivities lstUserActivities = ltUserActivities;

            List<UniqueVisits> lstUniqueVisits = new List<UniqueVisits>();
            var checkDate = DateTime.Now.AddDays(-pastDays);
            try
            {
                // lstUserActivities = DeSerializeObject<UserActivities>(USERACTIVITYFILEPATH);
                var arrDistinctUserId = lstUserActivities.UserActivity.GroupBy(i => i.IPAddress, (key, group) => group.First()).ToArray();

                var intTotalUniqueVisitCount = lstUserActivities.UserActivity.Where(x => !string.IsNullOrWhiteSpace(x.Date) && Convert.ToDateTime(x.Date) >= checkDate).GroupBy(l => l.IPAddress).Count();

                int intUniqueVisitCount = 0;
                foreach (var distinctUserId in arrDistinctUserId)
                {
                    if (!string.IsNullOrWhiteSpace(distinctUserId.IPAddress) && !string.IsNullOrWhiteSpace(distinctUserId.Date))
                    {
                        intUniqueVisitCount = lstUserActivities.UserActivity.Where(s => !string.IsNullOrWhiteSpace(s.Date) && Convert.ToDateTime(s.Date).Date == Convert.ToDateTime(distinctUserId.Date).Date).GroupBy(l => l.IPAddress).Count();
                        UniqueVisits objPagePerViews = new UniqueVisits();

                        objPagePerViews.UserId = distinctUserId.UserId;
                        objPagePerViews.UniqueCount = intUniqueVisitCount;
                        objPagePerViews.Date = Convert.ToDateTime(distinctUserId.Date).ToString("MM-dd-yyyy");
                        lstUniqueVisits.Add(objPagePerViews);
                    }
                }

            }
            catch
            {
            }

            return lstUniqueVisits;
        }

        public void GetPageLoadTime(string pagePath = null)
        {
            PagePerformance lstPagePerformance = null;

            try
            {
                lstPagePerformance = DeSerializeObject<PagePerformance>(PAGEPERFORMANCEFILEPATH);

            }
            catch
            {
            }
        }

        public PagePerformances GetPagePerformance()
        {
            PagePerformances lstPagePer = null;
            try
            {

                lstPagePer = DeSerializeObject<PagePerformances>(PAGEPERFORMANCEFILEPATH);
            }
            catch (Exception ex)
            {

            }
            return lstPagePer;
        }



        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializableObject"></param>
        /// <param name="fileName"></param>
        public void SerializeUserActivityObject<T>(T serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    //xmlDocument.DocumentElement.AppendChild(stream);  
                    xmlDocument.Save(fileName);

                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(USERACTIVITYFILEPATH);
                    XmlDocument newDoc = new XmlDocument();
                    newDoc.Load(USERACTIVITYTEMPFILEPATH);

                    //Use Xpath to specify node
                    XmlNode insertNode = xmldoc.SelectSingleNode("UserActivities");
                    XmlNode newObj = newDoc.SelectSingleNode("UserActivity");
                    XmlElement news = xmldoc.CreateElement("UserActivity");   // creating the wrapper news node
                    //Import the node into the context of the new document. NB the second argument = true imports all children of the node, too
                    XmlNode importNewsItem = xmldoc.ImportNode(newDoc.SelectSingleNode("UserActivity"), true);
                    //news.AppendChild(importNewsItem);
                    insertNode.AppendChild(importNewsItem);

                    xmldoc.Save(USERACTIVITYFILEPATH);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }
        }


        /// <summary>
        /// Deserializes an xml file into an object list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public T DeSerializeObject<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { return default(T); }

            T objectOut = default(T);

            try
            {
                string attributeXml = string.Empty;

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(T);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }

            return objectOut;
        }

        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializableObject"></param>
        /// <param name="fileName"></param>
        public void SerializePagePerformanceObject<T>(T serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    //xmlDocument.DocumentElement.AppendChild(stream);  
                    xmlDocument.Save(fileName);

                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(PAGEPERFORMANCEFILEPATH);
                    XmlDocument newDoc = new XmlDocument();
                    newDoc.Load(PAGEPERFORMANCETEMPFILEPATH);

                    //Use Xpath to specify node
                    XmlNode insertNode = xmldoc.SelectSingleNode("PerformanceActivities");
                    XmlNode newObj = newDoc.SelectSingleNode("PerformanceActivity");
                    XmlElement news = xmldoc.CreateElement("PerformanceActivity");   // creating the wrapper news node
                    //Import the node into the context of the new document. NB the second argument = true imports all children of the node, too
                    XmlNode importNewsItem = xmldoc.ImportNode(newDoc.SelectSingleNode("PerformanceActivity"), true);
                    //news.AppendChild(importNewsItem);
                    insertNode.AppendChild(importNewsItem);

                    xmldoc.Save(PAGEPERFORMANCEFILEPATH);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }
        }





    }



}
