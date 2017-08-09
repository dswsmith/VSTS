using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace FortressSageExporter
{
    class Program
    {
        private static Logger _logger;

        static string CsvFilePath = AppDomain.CurrentDomain.BaseDirectory;

        static void Main(string[] args)
        {
            try
            {
                _logger = new Logger();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Please Run as Administrator the first time you use this tool");
                Console.ReadKey();
                return;
            }

            _logger.Log("Getting Sales Orders", System.Diagnostics.EventLogEntryType.Information);
            SalesOrders orders = new SalesOrders();
            orders.CsvFilePath = CsvFilePath;
            if (orders.GetPendingDeliveries())
            {
                _logger.Log("Data found (" + orders.Count + "), writing to CSV", System.Diagnostics.EventLogEntryType.Information);
                orders.WriteToWarehouseCSV(CsvFilePath);
            }
            else
            {
                _logger.Log("No data found", System.Diagnostics.EventLogEntryType.Information);
            }

            //if (orders.GetAll())
            //{
            //    _logger.Log("Data found (" + orders.Count + "), writing to CSV", System.Diagnostics.EventLogEntryType.Information);
            //    orders.WriteToCSV(CsvFilePath);
            //}
            //else
            //{
            //    _logger.Log("No data found", System.Diagnostics.EventLogEntryType.Information);
            //}

            _logger.Log("Complete", System.Diagnostics.EventLogEntryType.Information);
        }
    }
}
