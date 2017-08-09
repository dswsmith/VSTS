using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshDeskReporting.Entities
{

    public class Group
    {
        private Logger _logger = new Logger();
        private DataAccess _data = new DataAccess();

        public int citid { get; set; }
        public object id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public long? escalate_to { get; set; }
        public string unassigned_for { get; set; }
        public object business_hour_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

        public bool Save()
        {
            try
            {
                citid = _data.SaveGroup(this);
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
