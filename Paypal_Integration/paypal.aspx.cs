using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Paypal_Integration
{
    public partial class paypal : System.Web.UI.Page
    {
        string url_token = "https://api.sandbox.paypal.com/v1/oauth2/token";
        protected void Page_Load(object sender, EventArgs e)
        {
            data();           
        }
        public void data()
        {
            string authUrl = "https://api.sandbox.paypal.com/v1/oauth2/token";           
            var sign = "grant_type=client_credentials";       
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest webRequest = WebRequest.Create(authUrl) as HttpWebRequest;

            string APIClientId = "AQOkOOoFj7m_figyEFedCCj2mnnKxcna7txoaKqpKUGGT8LYlCAlRkyRzqpzPTdsaKDhCw_7XxZX5DsL";           
            string APISecret = "EMcd9WB4DCW3YGfmBmyXGJBRKCFwpalI-HHktHecgVNh-TqlWecBUGjivSqfRhtQn3dOKpPneV98kLTY";

            var byteArray = Encoding.UTF8.GetBytes(APIClientId + ":" + APISecret); 
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Headers.Add("Authorization", "Basic "+ Convert.ToBase64String(byteArray) + "");
            webRequest.Headers.Add("Accept-Language", "en_US");        
            Stream dataStream = webRequest.GetRequestStream();
            String postData = String.Empty;           
            byte[] postArray = Encoding.ASCII.GetBytes(sign);
            dataStream.Write(postArray, 0, postArray.Length);
            dataStream.Close();
            WebResponse response = webRequest.GetResponse();
            dataStream = response.GetResponseStream();

            StreamReader responseReader = new StreamReader(dataStream);
            String returnVal = responseReader.ReadToEnd().ToString();

            TokenPaypal objTokenPaypal = JsonConvert.DeserializeObject<TokenPaypal>(returnVal);
            
            responseReader.Close();
            dataStream.Close();
            response.Close();
            var data=   VerifiedPaypal(objTokenPaypal.access_token, objTokenPaypal.token_type, "PAY-5T639032P1475804DLET7PYQ");
           
             
        }

        public string VerifiedPaypal(string access_token, string token_type, string TranID)

        {
            string authUrl = "https://api.sandbox.paypal.com/v1/payments/payment/PAY-5T639032P1475804DLET7PYQ";          
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest webRequest = WebRequest.Create(authUrl) as HttpWebRequest;
                        
            webRequest.Method = "GET";
            webRequest.ContentType = "application/json";
            webRequest.Headers.Add("Authorization", "Bearer " + access_token + "");      
            WebResponse response = webRequest.GetResponse();
            var data= response.GetResponseStream();
            StreamReader responseReader = new StreamReader(data);
            String returnVal = responseReader.ReadToEnd().ToString();
            JavaScriptSerializer js = new JavaScriptSerializer();
            var objPyapalDetails = JsonConvert.DeserializeObject<PyapalDetails.RootObject>(returnVal); 

            responseReader.Close();
          
            response.Close();
            return "";

        }

        public class TokenPaypal
        {
            public string scope { get; set; }
            public string nonce { get; set; }
            public string access_token { get; set; }
            public string token_type { get; set; }
            public string app_id { get; set; }
            public int expires_in { get; set; }
        }
              
       
    }
}