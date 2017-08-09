using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FreshDeskReporting
{
    class Program
    {
        private static Logger _logger = new Logger();

        private const string fdDomain = "confidence";           // Freshdesk domain 
        private const string apiKey = "cR1Hq6vJ4rbheJjp9OB";    // API user key
        private const string apiPath = "/api/v2/";              // API path 
        private const int apiPerPage = 30;                      // API items per page

        static void Main(string[] args)
        {
            _logger.Log("Getting tickets", System.Diagnostics.EventLogEntryType.Information);
            getTickets(1);

            _logger.Log("Getting agents", System.Diagnostics.EventLogEntryType.Information);
            getAgents();

            _logger.Log("Getting groups", System.Diagnostics.EventLogEntryType.Information);
            getGroups();

            _logger.Log("Getting contacts", System.Diagnostics.EventLogEntryType.Information);
            getContacts();

            _logger.Log("Getting companies", System.Diagnostics.EventLogEntryType.Information);
            getCompanies();

            _logger.Log("Complete", System.Diagnostics.EventLogEntryType.Information);
        }

        private static string callApi(string apiTypePath, int page = 0, string filter = null)
        {
            string responseBody = String.Empty;
            string apiUrl = "https://" + fdDomain + ".freshdesk.com" + apiPath + apiTypePath;
            if (page > 0)
            {
                apiUrl += apiUrl.Contains("?") ? "&" : "?";
                apiUrl += "page=" + page;
            }
            if (filter != null)
            {
                apiUrl += apiUrl.Contains("?") ? "&" : "?";
                apiUrl += filter;
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
            request.ContentType = "application/json";
            request.Method = "GET";
            string authInfo = apiKey + ":X"; // It could be your username:password also. 
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            request.Headers["Authorization"] = "Basic " + authInfo;
            try
            {
                _logger.Log("Submitting request to: " + request.RequestUri, System.Diagnostics.EventLogEntryType.Information);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    responseBody = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    //return status code 
                    _logger.Log(string.Format("Status Code: {1} {0}", ((HttpWebResponse)response).StatusCode, (int)((HttpWebResponse)response).StatusCode), System.Diagnostics.EventLogEntryType.Information);
                }
                return responseBody;
            }
            catch (WebException ex)
            {
                Console.WriteLine("API Error: " + ex.ToString());
                if (ex.Response != null)
                {

                    _logger.Log(string.Format("X-Request-Id: {0}", ex.Response.Headers["X-Request-Id"]), System.Diagnostics.EventLogEntryType.Error);
                    _logger.Log(string.Format("Error Status Code: {1} {0}", ((HttpWebResponse)ex.Response).StatusCode, (int)((HttpWebResponse)ex.Response).StatusCode), System.Diagnostics.EventLogEntryType.Error);
                    using (var stream = ex.Response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        _logger.Log("Error Response: " + reader.ReadToEnd(), System.Diagnostics.EventLogEntryType.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Log("ERROR: " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return null;
        }

        private static void getAgents()
        {
            int page = 0;
            Entities.Agents agents = null;
            while (agents == null || agents.Count == apiPerPage)
            {
                string jsonResponse = callApi("agents", page);
                if (jsonResponse != null)
                {
                    agents = JToken.Parse(jsonResponse).ToObject<Entities.Agents>();
                    agents.Save();
                    page++;
                }
                else
                {
                    _logger.Log("JSON Response is null", System.Diagnostics.EventLogEntryType.Warning);
                    break;
                }
            }
        }

        private static void getGroups()
        {
            int page = 0;
            Entities.Groups groups = null;
            while (groups == null || groups.Count == apiPerPage)
            {
                string jsonResponse = callApi("groups", page);
                if (jsonResponse != null)
                {
                    groups = JToken.Parse(jsonResponse).ToObject<Entities.Groups>();
                    groups.Save();
                    page++;
                }
                else
                {
                    _logger.Log("JSON Response is null", System.Diagnostics.EventLogEntryType.Warning);
                    break;
                }
            }
        }

        private static void getCompanies()
        {
            int page = 0;
            Entities.Companies companies = null;
            while (companies == null || companies.Count == apiPerPage)
            {
                string jsonResponse = callApi("companies", page);
                if (jsonResponse != null)
                {
                    companies = JToken.Parse(jsonResponse).ToObject<Entities.Companies>();
                    companies.Save();
                    page++;
                }
                else
                {
                    _logger.Log("JSON Response is null", System.Diagnostics.EventLogEntryType.Warning);
                    break;
                }
            }
        }

        private static void getContacts()
        {
            int page = 0;
            Entities.Contacts contacts = null;
            while (contacts == null || contacts.Count == apiPerPage)
            {
                string jsonResponse = callApi("contacts", page);
                if (jsonResponse != null)
                {
                    contacts = JToken.Parse(jsonResponse).ToObject<Entities.Contacts>();
                    contacts.Save();
                    page++;
                }
                else
                {
                    _logger.Log("JSON Response is null", System.Diagnostics.EventLogEntryType.Warning);
                    break;
                }
            }
        }
        
        private static void getTickets(int daysToFetch)
        {
            int page = 0;
            Entities.Tickets tickets = null;
            while (tickets == null || tickets.Count == apiPerPage)
            {
                string jsonResponse = callApi("tickets", page, "updated_since=" + DateTime.Now.AddDays(-daysToFetch).ToString("yyyy-MM-dd", null) + "&include=stats");
                if (jsonResponse != null)
                {
                    tickets = JToken.Parse(jsonResponse).ToObject<Entities.Tickets>();
                    tickets.Save();
                    page++;
                }
                else
                {
                    _logger.Log("JSON Response is null", System.Diagnostics.EventLogEntryType.Warning);
                    break;
                }
            }
        }
    }
}
