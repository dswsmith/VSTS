using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace FortressSageExporter
{
    class Sop_Item
    {
        public string ORDER_NUMBER1 { get; set; }
        public string ITEM_NUMBER { get; set; }
        public string JOB_NUMBER { get; set; }
        public string SERVICE_FLAG { get; set; }
        public string DESCRIPTION { get; set; }
        public string TEXT { get; set; }
        public string STOCK_CODE { get; set; }
        public string COMMENT_1 { get; set; }
        public string COMMENT_2 { get; set; }
        public string UNIT_OF_SALE { get; set; }
        public int QTY_ORDER { get; set; }
        public int QTY_ALLOCATED { get; set; }
        public int QTY_DELIVERED { get; set; }
        public int QTY_DESPATCH { get; set; }
        public int QTY_LAST_DESPATCH { get; set; }
        public double UNIT_PRICE { get; set; }
        public double DISCOUNT_AMOUNT { get; set; }
        public double DISCOUNT_RATE { get; set; }
        public DateTime DELIVERY_DATE { get; set; }
        public string NOMINAL_CODE { get; set; }
        public string DEPT_NUMBER { get; set; }
        public string DEPT_NAME { get; set; }
        public string TAX_CODE_ID { get; set; }
        public string TAX_CODE { get; set; }
        public string ADD_DISC_RATE { get; set; }
        public string TAX_RATE { get; set; }
        public double FULL_NET_AMOUNT { get; set; }
        public double NET_AMOUNT { get; set; }
        public double TAX_AMOUNT { get; set; }
        public double GROSS_AMOUNT { get; set; }
        public string EXT_ORDER_REF { get; set; }
        public string EXT_ORDER_LINE_REF { get; set; }
        public string PROJECT_ID1 { get; set; }
        public string THIS_RECORD { get; set; }
        public string POP_ITEM_ID { get; set; }
        public DateTime DUE_DATE { get; set; }
        public string ITEMID { get; set; }
        public string GENERATED_MESSAGE { get; set; }

        public Sop_Item(DataRow dr)
        {
            PopulateFromDataRow(dr);
        }

        private bool PopulateFromDataRow(DataRow dr)
        {
            if (dr != null)
            {
                if (dr["ORDER_NUMBER1"] != DBNull.Value) ORDER_NUMBER1 = dr["ORDER_NUMBER1"].ToString();
                if (dr["ITEM_NUMBER"] != DBNull.Value) ITEM_NUMBER = dr["ITEM_NUMBER"].ToString();
                if (dr["JOB_NUMBER"] != DBNull.Value) JOB_NUMBER = dr["JOB_NUMBER"].ToString();
                if (dr["SERVICE_FLAG"] != DBNull.Value) SERVICE_FLAG = dr["SERVICE_FLAG"].ToString();
                if (dr["DESCRIPTION"] != DBNull.Value) DESCRIPTION = dr["DESCRIPTION"].ToString();
                if (dr["TEXT"] != DBNull.Value) TEXT = dr["TEXT"].ToString();
                if (dr["STOCK_CODE"] != DBNull.Value) STOCK_CODE = dr["STOCK_CODE"].ToString();
                if (dr["COMMENT_1"] != DBNull.Value) COMMENT_1 = dr["COMMENT_1"].ToString();
                if (dr["COMMENT_2"] != DBNull.Value) COMMENT_2 = dr["COMMENT_2"].ToString();
                if (dr["UNIT_OF_SALE"] != DBNull.Value) UNIT_OF_SALE = dr["UNIT_OF_SALE"].ToString();
                if (dr["QTY_ORDER"] != DBNull.Value) QTY_ORDER = Convert.ToInt16(dr["QTY_ORDER"]);
                if (dr["QTY_ALLOCATED"] != DBNull.Value) QTY_ALLOCATED = Convert.ToInt16(dr["QTY_ALLOCATED"]);
                if (dr["QTY_DELIVERED"] != DBNull.Value) QTY_DELIVERED = Convert.ToInt16(dr["QTY_DELIVERED"]);
                if (dr["QTY_DESPATCH"] != DBNull.Value) QTY_DESPATCH = Convert.ToInt16(dr["QTY_DESPATCH"]);
                if (dr["QTY_LAST_DESPATCH"] != DBNull.Value) QTY_LAST_DESPATCH = Convert.ToInt16(dr["QTY_LAST_DESPATCH"]);
                if (dr["UNIT_PRICE"] != DBNull.Value) UNIT_PRICE = Convert.ToDouble(dr["UNIT_PRICE"]);
                if (dr["DISCOUNT_AMOUNT"] != DBNull.Value) DISCOUNT_AMOUNT = Convert.ToDouble(dr["DISCOUNT_AMOUNT"]);
                if (dr["DISCOUNT_RATE"] != DBNull.Value) DISCOUNT_RATE = Convert.ToDouble(dr["DISCOUNT_RATE"]);
                if (dr["DELIVERY_DATE"] != DBNull.Value)
                {
                    DateTime deliveryDate;
                    DateTime.TryParse(dr["DELIVERY_DATE"].ToString(), out deliveryDate);
                    DELIVERY_DATE = deliveryDate;
                }
                if (dr["NOMINAL_CODE"] != DBNull.Value) NOMINAL_CODE = dr["NOMINAL_CODE"].ToString();
                if (dr["DEPT_NUMBER"] != DBNull.Value) DEPT_NUMBER = dr["DEPT_NUMBER"].ToString();
                if (dr["DEPT_NAME"] != DBNull.Value) DEPT_NAME = dr["DEPT_NAME"].ToString();
                if (dr["TAX_CODE_ID"] != DBNull.Value) TAX_CODE_ID = dr["TAX_CODE_ID"].ToString();
                if (dr["TAX_CODE"] != DBNull.Value) TAX_CODE = dr["TAX_CODE"].ToString();
                if (dr["ADD_DISC_RATE"] != DBNull.Value) ADD_DISC_RATE = dr["ADD_DISC_RATE"].ToString();
                if (dr["TAX_RATE"] != DBNull.Value) TAX_RATE = dr["TAX_RATE"].ToString();
                if (dr["FULL_NET_AMOUNT"] != DBNull.Value) FULL_NET_AMOUNT = Convert.ToDouble(dr["FULL_NET_AMOUNT"]);
                if (dr["NET_AMOUNT"] != DBNull.Value) NET_AMOUNT = Convert.ToDouble(dr["NET_AMOUNT"]);
                if (dr["TAX_AMOUNT"] != DBNull.Value) TAX_AMOUNT = Convert.ToDouble(dr["TAX_AMOUNT"]);
                if (dr["GROSS_AMOUNT"] != DBNull.Value) GROSS_AMOUNT = Convert.ToDouble(dr["GROSS_AMOUNT"]);
                if (dr["EXT_ORDER_REF"] != DBNull.Value) EXT_ORDER_REF = dr["EXT_ORDER_REF"].ToString();
                if (dr["EXT_ORDER_LINE_REF"] != DBNull.Value) EXT_ORDER_LINE_REF = dr["EXT_ORDER_LINE_REF"].ToString();
                if (dr["PROJECT_ID1"] != DBNull.Value) PROJECT_ID1 = dr["PROJECT_ID1"].ToString();
                if (dr["THIS_RECORD"] != DBNull.Value) THIS_RECORD = dr["THIS_RECORD"].ToString();
                if (dr["POP_ITEM_ID"] != DBNull.Value) POP_ITEM_ID = dr["POP_ITEM_ID"].ToString();
                if (dr["DUE_DATE"] != DBNull.Value)
                {
                    DateTime dueDate;
                    DateTime.TryParse(dr["DUE_DATE"].ToString(), out dueDate);
                    DUE_DATE = dueDate;
                }
                if (dr["ITEMID"] != DBNull.Value) ITEMID = dr["ITEMID"].ToString();
                if (dr["GENERATED_MESSAGE"] != DBNull.Value) GENERATED_MESSAGE = dr["GENERATED_MESSAGE"].ToString();

                return true;
            }
            return false;
        }
    }
}
