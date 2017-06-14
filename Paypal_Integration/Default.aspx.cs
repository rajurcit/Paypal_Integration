using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Paypal_Integration
{
    public partial class Paypal_Integration : System.Web.UI.Page
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString());
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        DataRow dr;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Add some column to datatable display some products information           
                dt.Columns.Add("prodName");
                dt.Columns.Add("prodDesc");
                dt.Columns.Add("prodPrice");

                //Add rows with datatable and bind in the grid view
                dr = dt.NewRow();
                dr["prodName"] = "MindStick Cleaner";
                dr["prodDesc"] = "Cleans all system dummy data";
                dr["prodPrice"] = "$100.00";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["prodName"] = "MindStick DataConverter";
                dr["prodDesc"] = "Helps to import export data in different format";
                dr["prodPrice"] = "$120.00";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["prodName"] = "MindStick SurveyManager";
                dr["prodDesc"] = "Helps creating survey page with specified format dll";
                dr["prodPrice"] = "$140.00";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["prodName"] = "MindStick TeraByte Importer";
                dr["prodDesc"] = "Data transfer utility";
                dr["prodPrice"] = "$30.00";
                dt.Rows.Add(dr);
               
                gvPayPal1.DataSource = dt;
                gvPayPal1.DataBind();
            }
        }
        protected void gvPayPal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "buy")
            {
                ImageButton ib = (ImageButton)e.CommandSource;
                int index = Convert.ToInt32(ib.CommandArgument);
                GridViewRow row = gvPayPal1.Rows[index];

                //Get each Column label value from grid view and store it in label
                Label pName = (Label)row.FindControl("lblName");
                Label pDescription = (Label)row.FindControl("lblDescription");
                Label pPrice = (Label)row.FindControl("lblProductPrice");

                //Here store that person name who are going to make transaction
                Session["user"] = "Arun Singh";

                // make query string to store logged in user information in sql server table         
                string query = "";
                query = "insert into purchase(pname,pdesc,price,uname) values('" + pName.Text + "','" + pDescription.Text + "','" + pPrice.Text.Replace("$", "") + "','" + Session["user"].ToString() + "')";
                Con.Open();
                cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                Con.Close();

                //Pay pal process Refer for what are the variable are need to send http://www.paypalobjects.com/IntegrationCenter/ic_std-variable-ref-buy-now.html

                string redirectUrl = "";

                //Mention URL to redirect content to paypal site
                redirectUrl += "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=" + ConfigurationManager.AppSettings["paypalemail"].ToString();

                //First name I assign static based on login details assign this value
                redirectUrl += "&first_name=Arun_Seller";

                //Product Name
                redirectUrl += "&item_name=" + pName.Text;

                //Product Amount
                redirectUrl += "&amount=" + pPrice.Text;

                //Business contact paypal EmailID
                redirectUrl += "&business=arunse_1358772383_biz@gmail.com";

                //Shipping charges if any, or available or using shopping cart system
                redirectUrl += "&shipping=5";

                //Handling charges if any, or available or using shopping cart system
                redirectUrl += "&handling=5";

                //Tax charges if any, or available or using shopping cart system
                redirectUrl += "&tax=5";

                //Quantiy of product, Here statically added quantity 1
                redirectUrl += "&quantity=1";

                //If transactioin has been successfully performed, redirect SuccessURL page- this page will be designed by developer
                redirectUrl += "&return=" + ConfigurationManager.AppSettings["SuccessURL"].ToString();

                //If transactioin has been failed, redirect FailedURL page- this page will be designed by developer
                redirectUrl += "&cancel_return=" + ConfigurationManager.AppSettings["FailedURL"].ToString();
                
                Response.Redirect(redirectUrl);
            }
        }

        protected void gvPayPal1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}