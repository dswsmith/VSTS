using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshDeskReporting.Entities
{

    public class Agent
    {
        private Logger _logger = new Logger();
        private DataAccess _data = new DataAccess();

        public int citid { get; set; }
        public bool available { get; set; }
        public bool occasional { get; set; }
        public object id { get; set; }
        public string signature { get; set; }
        public int ticket_scope { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public object available_since { get; set; }
        public Contact contact { get; set; }

        public class CustomFields
        {
        }

        public bool Save()
        {
            try
            {
                if (contact.Save())
                {
                    citid = _data.SaveAgent(this);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.Log(ex.ToString(), System.Diagnostics.EventLogEntryType.Error);
            }
            return false;
        }
    }
}
