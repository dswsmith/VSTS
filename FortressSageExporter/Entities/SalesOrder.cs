using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSageExporter
{
    class SalesOrder
    {
        private Logger _logger = new Logger();

        public DataRow dataRow { get; set; }
        public string ORDER_NUMBER { get; set; }
        public string ORDER_TYPE_CODE { get; set; }
        public string ORDER_OR_QUOTE { get; set; }
        public DateTime ORDER_DATE { get; set; }
        public DateTime DESPATCH_DATE { get; set; }
        public string ALLOCATED_STATUS_CODE { get; set; }
        public string ALLOCATED_STATUS { get; set; }
        public string DESPATCH_STATUS_CODE { get; set; }
        public string DESPATCH_STATUS { get; set; }
        public string ACCOUNT_REF { get; set; }
        public string NAME { get; set; }
        public string ADDRESS_1 { get; set; }
        public string ADDRESS_2 { get; set; }
        public string ADDRESS_3 { get; set; }
        public string ADDRESS_4 { get; set; }
        public string ADDRESS_5 { get; set; }
        public string C_ADDRESS_1 { get; set; }
        public string C_ADDRESS_2 { get; set; }
        public string C_ADDRESS_3 { get; set; }
        public string C_ADDRESS_4 { get; set; }
        public string C_ADDRESS_5 { get; set; }
        public string DEL_NAME { get; set; }
        public string DEL_ADDRESS_1 { get; set; }
        public string DEL_ADDRESS_2 { get; set; }
        public string DEL_ADDRESS_3 { get; set; }
        public string DEL_ADDRESS_4 { get; set; }
        public string DEL_ADDRESS_5 { get; set; }
        public string VAT_REG_NUMBER { get; set; }
        public string INVOICE_NUMBER { get; set; }
        public string INVOICE_NUMBER_NUMERIC { get; set; }
        public string CONTACT_NAME { get; set; }
        public string TAKEN_BY { get; set; }
        public string CUST_ORDER_NUMBER { get; set; }
        public string CUST_TEL_NUMBER { get; set; }
        public string NOTES_1 { get; set; }
        public string NOTES_2 { get; set; }
        public string NOTES_3 { get; set; }
        public double ITEMS_NET { get; set; }
        public double ITEMS_TAX { get; set; }
        public double ITEMS_GROSS { get; set; }

        public Sop_Item SOP_ITEM { get; set; }

        public string CustomerOrderNumber()
        {
            if (!string.IsNullOrEmpty(CUST_ORDER_NUMBER)) return CUST_ORDER_NUMBER;
            return DateTime.Now.ToString("ddMMyyyy");
        }

        public string Address1()
        {
            if (!string.IsNullOrEmpty(DEL_ADDRESS_1)) return DEL_ADDRESS_1;
            if (!string.IsNullOrEmpty(C_ADDRESS_1)) return C_ADDRESS_1;
            return ADDRESS_1;
        }

        public string Address2()
        {
            if (!string.IsNullOrEmpty(DEL_ADDRESS_1)) return DEL_ADDRESS_2;
            if (!string.IsNullOrEmpty(C_ADDRESS_1)) return C_ADDRESS_2;
            return ADDRESS_2;
        }

        public string Address3()
        {
            if (!string.IsNullOrEmpty(DEL_ADDRESS_1)) return DEL_ADDRESS_3;
            if (!string.IsNullOrEmpty(C_ADDRESS_1)) return C_ADDRESS_3;
            return ADDRESS_3;
        }

        public string Address4()
        {
            if (!string.IsNullOrEmpty(DEL_ADDRESS_1)) return DEL_ADDRESS_4;
            if (!string.IsNullOrEmpty(C_ADDRESS_1)) return C_ADDRESS_4;
            return ADDRESS_4;
        }

        public string Address5()
        {
            if (!string.IsNullOrEmpty(DEL_ADDRESS_1)) return DEL_ADDRESS_5;
            if (!string.IsNullOrEmpty(C_ADDRESS_1)) return C_ADDRESS_5;
            return ADDRESS_5;
        }

        public string City()
        {
            //// assumes postcode is last address entry, then county, so calculates city as third-to-last address entry
            //string city = Address3();
            //if (string.IsNullOrEmpty(Address5())) city = Address2();        // 4 line address (street/city/county/postcode), city will likely be line 2
            //if (string.IsNullOrEmpty(Address4())) city = Address2();        // 3 line address (street/city/postcode), city will likely be line 2
            //return city;
            return Address3();
        }

        public string County()
        {
            //// assumes postcode is last address entry, so calculates county as second-to-last address entry
            //string county = Address4();
            //if (string.IsNullOrEmpty(Address5())) county = string.Empty;    // 4 line address (street/city/county/postcode), county will likely be omitted
            //if (string.IsNullOrEmpty(Address4())) county = string.Empty;    // 3 line address (street/city/postcode), county will likely be omitted
            //if (string.IsNullOrEmpty(Address3())) county = string.Empty;    // 2 line address, county will likely be omitted
            //return county;
            return Address4();
        }

        public string PostCode()
        {
            //// assume postcode is last address entry
            //if (!string.IsNullOrEmpty(Address5())) return Address5();
            //if (!string.IsNullOrEmpty(Address4())) return Address4();
            //if (!string.IsNullOrEmpty(Address3())) return Address3();
            //if (!string.IsNullOrEmpty(Address2())) return Address2();
            //return Address1();
            return Address5();
        }

        public string Notes()
        {
            string notes = string.Empty;
            if (!string.IsNullOrEmpty(NOTES_1)) notes += NOTES_1;
            if (!string.IsNullOrEmpty(NOTES_2))
            {
                if (!string.IsNullOrEmpty(notes)) notes += " ";
                notes += NOTES_2;
            }
            if (!string.IsNullOrEmpty(NOTES_3))
            {
                if (!string.IsNullOrEmpty(notes)) notes += " ";
                notes += NOTES_3;
            }
            return notes;
        }

        public SalesOrder(DataRow dr)
        {
            PopulateFromDataRow(dr);
        }

        private bool PopulateFromDataRow(DataRow dr) {
            if(dr != null)
            {
                dataRow = dr;

                if (dr["ORDER_NUMBER"] != DBNull.Value) ORDER_NUMBER = dr["ORDER_NUMBER"].ToString();
                if (dr["ORDER_NUMBER"] != DBNull.Value) ORDER_TYPE_CODE = dr["ORDER_TYPE_CODE"].ToString();
                if (dr["ORDER_NUMBER"] != DBNull.Value) ORDER_OR_QUOTE = dr["ORDER_OR_QUOTE"].ToString();
                if (dr["ORDER_DATE"] != DBNull.Value)
                {
                    DateTime orderDate;
                    DateTime.TryParse(dr["ORDER_DATE"].ToString(), out orderDate);
                    ORDER_DATE = orderDate;
                }
                if (dr["DESPATCH_DATE"] != DBNull.Value)
                {
                    DateTime despatchDate;
                    DateTime.TryParse(dr["DESPATCH_DATE"].ToString(), out despatchDate);
                    DESPATCH_DATE = despatchDate;
                }
                if (dr["ALLOCATED_STATUS_CODE"] != DBNull.Value) ALLOCATED_STATUS_CODE = dr["ALLOCATED_STATUS_CODE"].ToString();
                if (dr["ALLOCATED_STATUS"] != DBNull.Value) ALLOCATED_STATUS = dr["ALLOCATED_STATUS"].ToString();
                if (dr["DESPATCH_STATUS_CODE"] != DBNull.Value) DESPATCH_STATUS_CODE = dr["DESPATCH_STATUS_CODE"].ToString();
                if (dr["DESPATCH_STATUS"] != DBNull.Value) DESPATCH_STATUS = dr["DESPATCH_STATUS"].ToString();
                if (dr["ACCOUNT_REF"] != DBNull.Value) ACCOUNT_REF = dr["ACCOUNT_REF"].ToString();
                if (dr["NAME"] != DBNull.Value) NAME = dr["NAME"].ToString();
                if (dr["ADDRESS_1"] != DBNull.Value) ADDRESS_1 = dr["ADDRESS_1"].ToString();
                if (dr["ADDRESS_2"] != DBNull.Value) ADDRESS_2 = dr["ADDRESS_2"].ToString();
                if (dr["ADDRESS_3"] != DBNull.Value) ADDRESS_3 = dr["ADDRESS_3"].ToString();
                if (dr["ADDRESS_4"] != DBNull.Value) ADDRESS_4 = dr["ADDRESS_4"].ToString();
                if (dr["ADDRESS_5"] != DBNull.Value) ADDRESS_5 = dr["ADDRESS_5"].ToString();
                if (dr["C_ADDRESS_1"] != DBNull.Value) C_ADDRESS_1 = dr["C_ADDRESS_1"].ToString();
                if (dr["C_ADDRESS_2"] != DBNull.Value) C_ADDRESS_2 = dr["C_ADDRESS_2"].ToString();
                if (dr["C_ADDRESS_3"] != DBNull.Value) C_ADDRESS_3 = dr["C_ADDRESS_3"].ToString();
                if (dr["C_ADDRESS_4"] != DBNull.Value) C_ADDRESS_4 = dr["C_ADDRESS_4"].ToString();
                if (dr["C_ADDRESS_5"] != DBNull.Value) C_ADDRESS_5 = dr["C_ADDRESS_5"].ToString();
                if (dr["DEL_NAME"] != DBNull.Value) DEL_NAME = dr["DEL_NAME"].ToString();
                if (dr["DEL_ADDRESS_1"] != DBNull.Value) DEL_ADDRESS_1 = dr["DEL_ADDRESS_1"].ToString();
                if (dr["DEL_ADDRESS_2"] != DBNull.Value) DEL_ADDRESS_2 = dr["DEL_ADDRESS_2"].ToString();
                if (dr["DEL_ADDRESS_3"] != DBNull.Value) DEL_ADDRESS_3 = dr["DEL_ADDRESS_3"].ToString();
                if (dr["DEL_ADDRESS_4"] != DBNull.Value) DEL_ADDRESS_4 = dr["DEL_ADDRESS_4"].ToString();
                if (dr["DEL_ADDRESS_5"] != DBNull.Value) DEL_ADDRESS_5 = dr["DEL_ADDRESS_5"].ToString();
                if (dr["VAT_REG_NUMBER"] != DBNull.Value) VAT_REG_NUMBER = dr["VAT_REG_NUMBER"].ToString();
                if (dr["INVOICE_NUMBER"] != DBNull.Value) INVOICE_NUMBER = dr["INVOICE_NUMBER"].ToString();
                if (dr["INVOICE_NUMBER_NUMERIC"] != DBNull.Value) INVOICE_NUMBER_NUMERIC = dr["INVOICE_NUMBER_NUMERIC"].ToString();
                if (dr["CONTACT_NAME"] != DBNull.Value) CONTACT_NAME = dr["CONTACT_NAME"].ToString();
                if (dr["TAKEN_BY"] != DBNull.Value) TAKEN_BY = dr["TAKEN_BY"].ToString();
                if (dr["CUST_ORDER_NUMBER"] != DBNull.Value) CUST_ORDER_NUMBER = dr["CUST_ORDER_NUMBER"].ToString();
                if (dr["CUST_TEL_NUMBER"] != DBNull.Value) CUST_TEL_NUMBER = dr["CUST_TEL_NUMBER"].ToString();
                if (dr["NOTES_1"] != DBNull.Value) NOTES_1 = dr["NOTES_1"].ToString();
                if (dr["NOTES_2"] != DBNull.Value) NOTES_2 = dr["NOTES_2"].ToString();
                if (dr["NOTES_3"] != DBNull.Value) NOTES_3 = dr["NOTES_3"].ToString();
                if (dr["ITEMS_NET"] != DBNull.Value) ITEMS_NET = Convert.ToDouble(dr["ITEMS_NET"]);
                if (dr["ITEMS_TAX"] != DBNull.Value) ITEMS_TAX = Convert.ToDouble(dr["ITEMS_TAX"]);
                if (dr["ITEMS_GROSS"] != DBNull.Value) ITEMS_GROSS = Convert.ToDouble(dr["ITEMS_GROSS"]);

                SOP_ITEM = new Sop_Item(dr);

                return true;
            }
            return false;
        }

        public string WarehouseCSVHeader()
        {
            return "Customer Order No.," +
                   "Outbound PO," +
                   "SKU / Item," +
                   "Units - Committed," +
                   "Est.Ship Date," +
                   "Est.Delivery Date," +
                   "Carrier," +
                   "Service Level Code," +
                   "Service Level Description," +
                   "Carrier Ref.No.," +
                   "Consignee Code," +
                   "Consignee," +
                   "Consignee Address Line 1," +
                   "Consignee Address Line 2," +
                   "Consignee Address Line 4," +
                   "Consignee Email," +
                   "Consignee Address Line 6," +
                   "Consignee City," +
                   "Consignee State," +
                   "Consignee Postal Code," +
                   "Consignee Country," +
                   "Consignee Contact Name," +
                   "Consignee Contact Phone," +
                   "Your Despatch Ref," +
                   "Shipment Notes," + 
                   "Client Batch No.";
        }

        public string WarehouseCSVLine()
        {
            /*
            Customer Order No.
            Outbound PO
            SKU / Item
            Units - Committed
            Est. Ship Date
            Est. Delivery Date
            Carrier
            Service Level Code
            Service Level Description
            Carrier Ref. No.
            Consignee Code
            Consignee
            Consignee Address Line 1
            Consignee Address Line 2
            Consignee Address Line 4
            Consignee Email
            Consignee Address Line 6
            Consignee City
            Consignee State
            Consignee Postal Code
            Consignee Country
            Consignee Contact Name
            Consignee Contact Phone
            Your Despatch Ref
            Shipment Notes
            Client Batch No.
            */

            //string address3 = Address3();
            //string city = City();
            //string county = County();
            //string postcode = PostCode();
            //if (address3 == city || address3 == county || address3 == postcode) address3 = string.Empty;
            //if (city == county) county = string.Empty;

            return CustomerOrderNumber() + "," +
                   "," +
                   SOP_ITEM.STOCK_CODE + "," +
                   SOP_ITEM.QTY_DESPATCH + "," +
                   "," +
                   DateTime.Now.ToString("dd/MM/yyyy") + "," +
                   "," +
                   "," +
                   "," +
                   "," +
                   ACCOUNT_REF + "," +
                   PrepareForCSV(NAME) + "," +
                   PrepareForCSV(Address1()) + "," +
                   PrepareForCSV(Address2()) + "," +
                   PrepareForCSV(Address3()) + "," +
                   "," +
                   "," +
                   PrepareForCSV(City()) + "," +
                   PrepareForCSV(County()) + "," +
                   PrepareForCSV(PostCode()) + "," +
                   "GB," +
                   PrepareForCSV(CONTACT_NAME) + "," +
                   CUST_TEL_NUMBER + "," +
                   ORDER_NUMBER + "," +
                   PrepareForCSV(Notes()) + "," +
                   "";

            //SOP_ITEM.DELIVERY_DATE.ToString("dd/MM/yyyy") + "," +
        }


        public string CSVHeader()
        {
            string header = string.Empty;
            for (int i = 0; i < dataRow.Table.Columns.Count; i++)
            {
                if (!string.IsNullOrEmpty(header)) header += ",";
                header += dataRow.Table.Columns[i].ColumnName;
            }
            return WarehouseCSVHeader() + "," + header;
        }

        public string CSVLine()
        {
            string output = string.Empty;
            for (int i = 0; i < dataRow.Table.Columns.Count; i++)
            {
                if (!string.IsNullOrEmpty(output)) output += ",";
                output += PrepareForCSV(dataRow[i].ToString());
            }
            return WarehouseCSVLine() + "," + output;
        }

        private string PrepareForCSV(string value)
        {
            value = value.Replace(",", " ");
            return value;
        }
    }
}
