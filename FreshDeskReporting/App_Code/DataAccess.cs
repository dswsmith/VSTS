using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace FreshDeskReporting
{
    class DataAccess
    {
        private string _connectionString;

        public DataAccess()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }

        private int insertOrUpdate(SqlCommand command)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(_connectionString);
                connection.Open();

                command.Connection = connection;
                command.ExecuteNonQuery();

                return (int)command.Parameters["@citid"].Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (command != null) command.Dispose();
            }
        }

        public int SaveAgent(Entities.Agent agent)
        {
            SqlCommand command = new SqlCommand("freshdesk.InsertOrUpdateAgent");
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@id", SqlDbType.BigInt);
            param.Value = Convert.ToInt64(agent.id);
            command.Parameters.Add(param);

            param = new SqlParameter("@contactId", SqlDbType.BigInt);
            if (agent.contact.id != null)
                param.Value = Convert.ToInt64(agent.contact.id);
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@contactcitid", SqlDbType.Int);
            param.Value = agent.contact.citid;
            command.Parameters.Add(param);

            param = new SqlParameter("@available", SqlDbType.Bit);
            param.Value = agent.available;
            command.Parameters.Add(param);

            param = new SqlParameter("@citid", SqlDbType.Int);
            param.Direction = ParameterDirection.ReturnValue;
            command.Parameters.Add(param);

            return insertOrUpdate(command);
        }

        public int SaveGroup(Entities.Group group)
        {
            SqlCommand command = new SqlCommand("freshdesk.InsertOrUpdateGroup");
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@id", SqlDbType.BigInt);
            if (group.id != null)
                param.Value = Convert.ToInt64(group.id);
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);
            
            param = new SqlParameter("@name", SqlDbType.NVarChar);
            if (group.name != null)
                param.Value = group.name;
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@description", SqlDbType.NVarChar);
            if (group.description != null)
                param.Value = group.description;
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@escalateto", SqlDbType.BigInt);
            if (group.escalate_to != null)
                param.Value = group.escalate_to;
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@unassignedfor", SqlDbType.NVarChar);
            if (group.unassigned_for != null)
                param.Value = group.unassigned_for;
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@businesshourid", SqlDbType.BigInt);
            if (group.business_hour_id != null)
                param.Value = group.business_hour_id;
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@created", SqlDbType.DateTime);
            param.Value = DateTime.ParseExact(group.created_at, "MM/dd/yyyy HH:mm:ss", null);
            command.Parameters.Add(param);

            param = new SqlParameter("@updated", SqlDbType.DateTime);
            if (group.updated_at != null)
                param.Value = DateTime.ParseExact(group.updated_at, "MM/dd/yyyy HH:mm:ss", null);
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);
            
            param = new SqlParameter("@citid", SqlDbType.Int);
            param.Direction = ParameterDirection.ReturnValue;
            command.Parameters.Add(param);

            return insertOrUpdate(command);
        }

        public int SaveContact(Entities.Contact contact)
        {
            SqlCommand command = new SqlCommand("freshdesk.InsertOrUpdateContact");
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@id", SqlDbType.BigInt);
            if (contact.id != null)
                param.Value = Convert.ToInt64(contact.id);
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@companyid", SqlDbType.BigInt);
            if (contact.company_id != null)
                param.Value = contact.company_id;
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@name", SqlDbType.NVarChar);
            if (contact.name != null)
                param.Value = contact.name;
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@phone", SqlDbType.NVarChar);
            if (contact.phone != null)
                param.Value = contact.phone;
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@email", SqlDbType.NVarChar);
            if (contact.email != null)
                param.Value = contact.email;
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@created", SqlDbType.DateTime);
            param.Value = DateTime.ParseExact(contact.created_at, "MM/dd/yyyy HH:mm:ss", null);
            command.Parameters.Add(param);

            param = new SqlParameter("@updated", SqlDbType.DateTime);
            if (contact.updated_at != null)
                param.Value = DateTime.ParseExact(contact.updated_at, "MM/dd/yyyy HH:mm:ss", null);
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@lastlogin", SqlDbType.DateTime);
            if (contact.last_login_at != null)
                param.Value = DateTime.ParseExact(contact.last_login_at, "MM/dd/yyyy HH:mm:ss", null);
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@active", SqlDbType.Bit);
            param.Value = contact.active;
            command.Parameters.Add(param);

            param = new SqlParameter("@citid", SqlDbType.Int);
            param.Direction = ParameterDirection.ReturnValue;
            command.Parameters.Add(param);

            return insertOrUpdate(command);
        }

        public int SaveCompany(Entities.Company company)
        {
            SqlCommand command = new SqlCommand("freshdesk.InsertOrUpdateCompany");
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@id", SqlDbType.BigInt);
            param.Value = Convert.ToInt64(company.id);
            command.Parameters.Add(param);
            
            param = new SqlParameter("@name", SqlDbType.NVarChar);
            param.Value = company.name;
            command.Parameters.Add(param);

            param = new SqlParameter("@description", SqlDbType.NVarChar);
            if (company.description != null)
                param.Value = company.description;
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@note", SqlDbType.NVarChar);
            if (company.note != null)
                param.Value = company.note;
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@domains", SqlDbType.NVarChar);
            if (company.domains != null)
                param.Value = string.Join(",", company.domains);
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@created", SqlDbType.DateTime);
            param.Value = DateTime.ParseExact(company.created_at, "MM/dd/yyyy HH:mm:ss", null);
            command.Parameters.Add(param);

            param = new SqlParameter("@updated", SqlDbType.DateTime);
            if (company.updated_at != null)
                param.Value = DateTime.ParseExact(company.updated_at, "MM/dd/yyyy HH:mm:ss", null);
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@citid", SqlDbType.Int);
            param.Direction = ParameterDirection.ReturnValue;
            command.Parameters.Add(param);

            return insertOrUpdate(command);
        }

        public int SaveTicket(Entities.Ticket ticket)
        {
            SqlCommand command = new SqlCommand("freshdesk.InsertOrUpdateTicket");
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@id", SqlDbType.BigInt);
            param.Value = Convert.ToInt64(ticket.id);
            command.Parameters.Add(param);

            param = new SqlParameter("@groupid", SqlDbType.BigInt);
            if (ticket.group_id != null)
                param.Value = ticket.group_id;
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@companyid", SqlDbType.BigInt);
            if (ticket.company_id != null)
                param.Value = ticket.company_id;
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@requesterid", SqlDbType.BigInt);
            if (ticket.requester_id != null)
                param.Value = ticket.requester_id;
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@responderid", SqlDbType.BigInt);
            if (ticket.responder_id != null)
                param.Value = ticket.responder_id;
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@spam", SqlDbType.Bit);
            param.Value = ticket.spam;
            command.Parameters.Add(param);

            param = new SqlParameter("@source", SqlDbType.NVarChar);
            param.Value = Utils.GetEnumDescription(ticket.source);
            command.Parameters.Add(param);

            param = new SqlParameter("@type", SqlDbType.NVarChar);
            if (ticket.type != null)
                param.Value = ticket.type;
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@priority", SqlDbType.NVarChar);
            param.Value = Utils.GetEnumDescription(ticket.priority);
            command.Parameters.Add(param);

            param = new SqlParameter("@subject", SqlDbType.NVarChar);
            if (ticket.subject != null)
                param.Value = ticket.subject;
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@description", SqlDbType.NVarChar);
            if (ticket.description != null)
                param.Value = ticket.description;
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@status", SqlDbType.NVarChar);
            param.Value = Utils.GetEnumDescription(ticket.status);
            command.Parameters.Add(param);

            param = new SqlParameter("@created", SqlDbType.DateTime);
            param.Value = DateTime.ParseExact(ticket.created_at, "MM/dd/yyyy HH:mm:ss", null);
            command.Parameters.Add(param);

            param = new SqlParameter("@updated", SqlDbType.DateTime);
            if (ticket.updated_at != null)
                param.Value = DateTime.ParseExact(ticket.updated_at, "MM/dd/yyyy HH:mm:ss", null);
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@itemsupplied", SqlDbType.Bit);
            if (ticket.custom_fields.item_supplied != null)
                param.Value = ticket.custom_fields.item_supplied;
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@invoicenumber", SqlDbType.NVarChar);
            if (ticket.custom_fields.invoice_number != null)
                param.Value = ticket.custom_fields.invoice_number;
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@invoiced", SqlDbType.Bit);
            if (ticket.custom_fields.invoiced != null)
                param.Value = ticket.custom_fields.invoiced;
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@resolvedat", SqlDbType.DateTime);
            if (ticket.stats.resolved_at != null)
                param.Value = DateTime.ParseExact(ticket.stats.resolved_at, "MM/dd/yyyy HH:mm:ss", null);
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@firstrespondedat", SqlDbType.DateTime);
            if (ticket.stats.first_responded_at != null)
                param.Value = DateTime.ParseExact(ticket.stats.first_responded_at, "MM/dd/yyyy HH:mm:ss", null);
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@closedat", SqlDbType.DateTime);
            if (ticket.stats.closed_at != null)
                param.Value = DateTime.ParseExact(ticket.stats.closed_at, "MM/dd/yyyy HH:mm:ss", null);
            else
                param.Value = DBNull.Value;
            command.Parameters.Add(param);

            param = new SqlParameter("@citid", SqlDbType.Int);
            param.Direction = ParameterDirection.ReturnValue;
            command.Parameters.Add(param);

            return insertOrUpdate(command);
        }
    }
}
