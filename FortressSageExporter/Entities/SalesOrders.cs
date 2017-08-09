using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.Odbc;

namespace FortressSageExporter
{
    class SalesOrders : List<SalesOrder>
    {
        private Logger _logger = new Logger();

        string[] DsnNames = new string[] { "Sage50Accounts2016", "SageLine50v23"};
        const string Username = "alan";
        const string Password = "hiexp22hh";
        const string DsnException = "Data source name not found";

        const string ConnectionString = "DSN={0};uid={1};pwd={2}";
        const string CsvFileName = "SalesOrders.csv";

        public string CsvFilePath { get; set; }

        public bool GetAll(int DsnIndex = 1)
        {
            this.Clear();

            OdbcConnection dbConnection = null;
            try
            {
                StringBuilder csv = new StringBuilder();

                dbConnection = new OdbcConnection();
                dbConnection.ConnectionString = string.Format(ConnectionString, DsnNames[DsnIndex - 1], Username, Password);
                dbConnection.Open();

                DataTable dt = new DataTable();

                OdbcDataAdapter dbDataAdapter = new OdbcDataAdapter("SELECT " +
                    "   * " +
                    "FROM " +
                    "   SALES_ORDER, " +
                    "   SOP_ITEM " +
                    "WHERE " +
                    "   SALES_ORDER.ORDER_NUMBER = SOP_ITEM.ORDER_NUMBER",
                    dbConnection);
                
                dbDataAdapter.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    this.Add(new SalesOrder(dr));
                }

                return true;
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains(DsnException) && DsnIndex <= DsnNames.Length) return GetAll(2);
                _logger.Log("GetPendingDeliveries failed: Index [" + DsnIndex + "] - " + ex.ToString(), System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                if (dbConnection != null)
                {
                    dbConnection.Close();
                }
            }
            return false;
        }

        public bool GetPendingDeliveries(int DsnIndex = 1)
        {
            this.Clear();

            OdbcConnection dbConnection = null;
            try
            {
                StringBuilder csv = new StringBuilder();

                dbConnection = new OdbcConnection();
                dbConnection.ConnectionString = string.Format(ConnectionString, DsnNames[DsnIndex - 1], Username, Password);
                dbConnection.Open();

                DataTable dt = new DataTable();

                OdbcDataAdapter dbDataAdapter = new OdbcDataAdapter("SELECT " +
                    "   * " +
                    "FROM " +
                    "   SALES_ORDER, " +
                    "   SOP_ITEM " +
                    "WHERE " +
                    "   SALES_ORDER.ORDER_NUMBER = SOP_ITEM.ORDER_NUMBER "+
                    "   AND SOP_ITEM.STOCK_CODE <> 'M' " +
                    "   AND SALES_ORDER.CUST_ORDER_NUMBER NOT LIKE '%CONSIGNMENT%' " +
                    "   AND SOP_ITEM.QTY_DESPATCH > 0" + 
                    "   AND SALES_ORDER.DESPATCH_STATUS_CODE <> 2" +
                    "   AND (SALES_ORDER.ANALYSIS_1 IS NULL OR SALES_ORDER.ANALYSIS_1 = '')", 
                    dbConnection);

                // "   AND SALES_ORDER.ALLOCATED_STATUS_CODE IN (0,1) " +
                //"   AND SOP_ITEM.DELIVERY_DATE > '" + DateTime.Now.AddDays(-100).ToString("yyyy-MM-dd") + "' " +

                dbDataAdapter.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    this.Add(new SalesOrder(dr));
                }

                return true;
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains(DsnException) && DsnIndex <= DsnNames.Length) return GetPendingDeliveries(2);
                _logger.Log("GetPendingDeliveries failed: Index [" + DsnIndex + "] - " + ex.ToString(), System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                if (dbConnection != null)
                {
                    dbConnection.Close();
                }
            }
            return false;
        }

        public bool WriteToWarehouseCSV(string csvFilePath)
        {
            try
            {
                StringBuilder output = new StringBuilder();

                Int32 lineNo = 0;
                foreach (SalesOrder so in this)
                {
                    if (lineNo == 0) output.AppendLine(so.WarehouseCSVHeader());
                    output.AppendLine(so.WarehouseCSVLine());
                    lineNo++;
                }

                if (!Directory.Exists(CsvFilePath)) Directory.CreateDirectory(CsvFilePath);
                File.WriteAllText(CsvFilePath + CsvFileName.Replace(".csv", DateTime.Now.ToString("_ddMMyyyyHHmmss")) + ".csv", output.ToString());

                return true;
            } catch(Exception ex)
            {
                _logger.Log("WrtieToCSV failed: " + ex.ToString(), System.Diagnostics.EventLogEntryType.Error);
            }
            return false;
        }

        public bool WriteToCSV(string csvFilePath)
        {
            try
            {
                StringBuilder output = new StringBuilder();

                Int32 lineNo = 0;
                foreach (SalesOrder so in this)
                {
                    if (lineNo == 0) output.AppendLine(so.CSVHeader());
                    output.AppendLine(so.CSVLine());
                    lineNo++;
                }

                if (!Directory.Exists(CsvFilePath)) Directory.CreateDirectory(CsvFilePath);
                File.WriteAllText(CsvFilePath + CsvFileName.Replace(".csv", DateTime.Now.ToString("_ddMMyyyyHHmmss")) + ".csv", output.ToString());

                return true;
            }
            catch (Exception ex)
            {
                _logger.Log("WrtieToCSV failed: " + ex.ToString(), System.Diagnostics.EventLogEntryType.Error);
            }
            return false;
        }
    }
}
