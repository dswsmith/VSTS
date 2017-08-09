using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshDeskReporting.Entities
{
    class Ticket
    {
        private Logger _logger = new Logger();
        private DataAccess _data = new DataAccess();

        public int citid { get; set; }
        public List<object> cc_emails { get; set; }
        public List<object> fwd_emails { get; set; }
        public List<object> reply_cc_emails { get; set; }
        public bool fr_escalated { get; set; }
        public bool spam { get; set; }
        public object email_config_id { get; set; }
        public object group_id { get; set; }
        public Priority priority { get; set; }
        public object requester_id { get; set; }
        public long? responder_id { get; set; }
        public Source source { get; set; }
        public object company_id { get; set; }
        public Status status { get; set; }
        public string subject { get; set; }
        public List<string> to_emails { get; set; }
        public object product_id { get; set; }
        public long id { get; set; }
        public object type { get; set; }
        public string due_by { get; set; }
        public string fr_due_by { get; set; }
        public bool is_escalated { get; set; }
        public string description { get; set; }
        public string description_text { get; set; }
        public CustomFields custom_fields { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public Stats stats { get; set; }


        public class CustomFields
        {
            public bool? item_supplied { get; set; }
            public object internal_notes { get; set; }
            public object invoice_number { get; set; }
            public bool? invoiced { get; set; }
        }

        public class Stats
        {
            public string resolved_at { get; set; }
            public string first_responded_at { get; set; }
            public string closed_at { get; set; }
        }

        public enum Source
        {
            Email = 1,
            Portal = 2,
            Phone = 3,
            Chat = 7,
            Mobihelp = 8,
            FeedbackWidget = 9,
            OutboundEmail = 10
        }

        public enum Status
        {
            Open = 2,
            Pending = 3,
            Resolved = 4,
            Closed = 5,
            [Description("Waiting on Customer")]
            WaitingOnCustomer = 6,
            [Description("Waiting on Third Party")]
            WaitingOnThirdParty = 7,
            [Description("Bill Pending")]
            BillPending = 8,
            Scheduled = 9,
            [Description("Testing Resolution")]
            TestingResolution = 10,
            [Description("In Development")]
            InDevelopment = 11
        }

        public enum Priority
        {
            Low = 1,
            Meduim = 2,
            High = 3,
            Urgent = 4
        }

        public bool Save()
        {
            try
            {
                citid = _data.SaveTicket(this);
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
