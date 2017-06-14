using System;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;

namespace Paypal_Integration
{
    public partial class BulkCopy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Upload(object sender, EventArgs e)
        {
            //Upload and save the file
            string excelPath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(excelPath);

            string conString = string.Empty;
            string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            switch (extension)
            {
                case ".xls": //Excel 97-03
                    
                    conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07 or higher
                    conString = ConfigurationManager.AppSettings["Excel07+ConString"].ToString();
                    break;

            }
            conString = string.Format(conString, excelPath);
            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();
                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                DataTable dtExcelData = new DataTable();

                //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                dtExcelData.Columns.AddRange(new DataColumn[9] { new DataColumn("Id", typeof(int)),
                new DataColumn("CategoryName", typeof(string)),
                new DataColumn("ProductName", typeof(string)),
                new DataColumn("ImgName", typeof(string)),
                new DataColumn("Brand", typeof(string)),
                new DataColumn("MaxCartLimit",typeof(decimal)),
                 new DataColumn("UnitValue", typeof(string)),
                new DataColumn("Unit", typeof(string)),
                new DataColumn("Price",typeof(decimal)),
                });

                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                {
                    oda.Fill(dtExcelData);
                }
                excel_con.Close();

                string consString = ConfigurationManager.AppSettings["SZConn"].ToString();
                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "dbo.Importproductstemp";

                        //[OPTIONAL]: Map the Excel columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("Id", "Id");
                        sqlBulkCopy.ColumnMappings.Add("CategoryName", "CategoryName");
                        sqlBulkCopy.ColumnMappings.Add("ProductName", "ProductName");
                        sqlBulkCopy.ColumnMappings.Add("ImgName", "ImgName");
                        sqlBulkCopy.ColumnMappings.Add("Description", "Description");
                        sqlBulkCopy.ColumnMappings.Add("Brand", "Brand");
                        sqlBulkCopy.ColumnMappings.Add("MaxCartLimit", "MaxCartLimit");
                        sqlBulkCopy.ColumnMappings.Add("UnitValue", "UnitValue");
                        sqlBulkCopy.ColumnMappings.Add("Unit", "Unit");
                        sqlBulkCopy.ColumnMappings.Add("Price", "Price");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dtExcelData);

                      int  count = BulkImportProducts(consString, "BulkImportProducts");
                        con.Close();
                    }
                }
            }
        }
        public static int BulkImportProducts(string ConnString, string strSP)
        {
            string status = "";
            int pid = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(ConnString))
                {
                    //Prepare the Command

                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.Connection = con;

                        cmd.CommandText = strSP;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Connection.Open();

                        int i = cmd.ExecuteNonQuery();

                        cmd.Connection.Close();
                    }
                }

                return 1;
            }
            catch (Exception ex) { return -1; }

        }
    }
}