using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshDeskReporting.Entities
{

    public class Company
    {
        private Logger _logger = new Logger();
        private DataAccess _data = new DataAccess();

        public int citid { get; set; }
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string note { get; set; }
        public List<object> domains { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public CustomFields custom_fields { get; set; }

        public class CustomFields
        {
            public string address { get; set; }
            public string office_phone { get; set; }
            public string connection_information { get; set; }
            public string domain_amp_server_information { get; set; }
            public string hosted_services_information { get; set; }
            public string other_information { get; set; }
        }

        public bool Save()
        {
            try
            {
                citid = _data.SaveCompany(this);
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
