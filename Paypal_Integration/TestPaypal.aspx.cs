using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Paypal_Integration
{
    public partial class TestPaypal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        public async Task<ResponseT> InvokePostAsync<RequestT, ResponseT>(RequestT request, string actionUrl)
        {
            ResponseT result;

            // 'HTTP Basic Auth Post' <http://stackoverflow.com/questions/21066622/how-to-send-a-http-basic-auth-post>
            string clientId = "AQOkOOoFj7m_figyEFedCCj2mnnKxcna7txoaKqpKUGGT8LYlCAlRkyRzqpzPTdsaKDhCw_7XxZX5DsL";
            string secret = "EMcd9WB4DCW3YGfmBmyXGJBRKCFwpalI-HHktHecgVNh-TqlWecBUGjivSqfRhtQn3dOKpPneV98kLTY";
            string oAuthCredentials = Convert.ToBase64String(Encoding.Default.GetBytes(clientId + ":" + secret));

            //base uri to PayPAl 'live' or 'stage' based on 'productionMode'
            string uriString = "https://api.sandbox.paypal.com/v1/oauth2/token";

            HttpClient client = new HttpClient();

            //construct request message
            var h_request = new HttpRequestMessage(HttpMethod.Post, uriString);
            h_request.Headers.Authorization = new AuthenticationHeaderValue("Basic", oAuthCredentials);
            h_request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            h_request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue("en_US"));

            h_request.Content = new StringContent("grant_type=client_credentials", UTF8Encoding.UTF8, "application/x-www-form-urlencoded");

            try
            {
                HttpResponseMessage response = await client.SendAsync(h_request);

                //if call failed ErrorResponse created...simple class with response properties
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    ErrorResponse errResp = JsonConvert.DeserializeObject<ErrorResponse>(error);
                    throw new PayPalException { error_name = errResp.name, details = errResp.details, message = errResp.message };
                }

                var success = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<ResponseT>(success);
            }
            catch (Exception)
            {
                throw new HttpRequestException("Request to PayPal Service failed.");
            }

            return result;
        }

    }
}