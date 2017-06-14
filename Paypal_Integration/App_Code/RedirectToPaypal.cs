using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


/// <summary>
/// Summary description for RedirectToPaypal
/// </summary>
public class RedirectToPaypal
{
	public RedirectToPaypal()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// This Method returns 
    /// </summary>
    /// <param name="itemName"></param>
    /// <param name="itemCost"></param>
    /// <returns></returns>
    public static string  getItemNameAndCost(string itemName, string itemCost)
    {

        //Converting String Money Value Into Decimal
       decimal price = Convert.ToDecimal(itemCost);
        //declaring empty String
       string returnURL = "";
       returnURL +="https://www.paypal.com/xclick/business=raju.gupta@sirez.com";
        //Passing Item Name as dynamic
       returnURL +="&item_name="+itemName;
        //Assigning Name as Statically to Parameter
       string fname = "Raju";
       returnURL += "&first_name" +fname;
       //Assigning City as Statically to Parameter
        string myCity="Delhi";
       returnURL += "&city" + myCity;
       //Assigning State as Statically to Parameter
       string myState = "new Delhi";
       returnURL += "&state" + myState;
       //Passing Amount as Dynamic
       returnURL += "&amount=" + price;
       //Passing Currency as your 
       returnURL += "&currency=USD";
       //retturn Url if Customer wants To return to Previous Page
       returnURL += "&return=http://bangarubabupureti.spaces.live.com";
       //retturn Url if Customer Wants To Cancel the Transaction
       returnURL += "&cancel_return=http://bangarubabupureti.spaces.live.com";
       return returnURL;

    }
}
