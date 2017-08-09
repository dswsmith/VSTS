using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshDeskReporting.Entities
{

    public class Contact
    {
        private Logger _logger = new Logger();
        private DataAccess _data = new DataAccess();

        public int citid { get; set; }
        public bool active { get; set; }
        public string address { get; set; }
        public long? company_id { get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public object id { get; set; }
        public string job_title { get; set; }
        public string language { get; set; }
        public string mobile { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string time_zone { get; set; }
        public string twitter_id { get; set; }
        public CustomFields custom_fields { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string last_login_at { get; set; }

        public class CustomFields
        {
        }

        public bool Save()
        {
            try
            {
                citid = _data.SaveContact(this);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Log(ex.ToString(), System.Diagnostics.EventLogEntryType.Error);
            }
            return false;
        }
    }
}
