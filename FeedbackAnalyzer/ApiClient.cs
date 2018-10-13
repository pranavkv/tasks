using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FeedbackAnalyzer
{
    class ApiClient
    {

        private static string apitoken = ""; // this will be set once login is success
        private System.Net.HttpWebRequest request;
        private System.IO.Stream dataStream;
        private string baseUrl = "https://api.getcloudcherry.com/";
        private static bool isLogin = false;
        private string status;
        private string contentType = ApiConstants.APPLICATION_JSON;

        public void SetContentType(string type) // in case need to change the baseUrl
        {
            this.contentType = type;
        }

        public void SetBaseUrl(String baseurl) // in case need to change the baseUrl
        {
            this.baseUrl = baseurl;
        }

        public String Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }


        public ApiClient(string url, string method, string data, bool isLoginRequest, bool isAuth)
        {

            if (!isLogin && !isLoginRequest) { // one time login to get the token
                login();
                if (!isLogin)
                    throw new ApiException("Unbale to proceed! Login Token not generated");
            }

            url = this.baseUrl + url;
            Console.WriteLine("API Request: {0}", url);
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            request.Headers.Clear();
            request.Method = method;

            if (isAuth)
            {
                //string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password)); // setting the auth header in request
                request.Headers.Add("Authorization", "Bearer " + apitoken);
                Console.WriteLine("apitoken={0}", apitoken);
            }

           if (method.Equals("POST"))
            {

                // Create POST data and convert it to a byte array.
                string postData = data;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentLength = byteArray.Length;
                dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                
                // Close the Stream object.
                dataStream.Close();
            }

        }

        public void login()
        {
            string credentials = "grant_type=password&username=sarancem&password=Cl0udCherry%40123";
            ApiClient api = new ApiClient(ApiConstants.LOGIN_API, "POST", credentials, true, false);
            api.SetContentType(ApiConstants.APPLICATION_FORM_URL);
            string loginResponse = api.GetResponse();

            var tokenStructure = new { access_token = string.Empty, expires_in = 0 };
            var token = JsonConvert.DeserializeAnonymousType(loginResponse, tokenStructure);
            apitoken = token.access_token;
            if (!string.IsNullOrEmpty(token.access_token))
                isLogin = true;
            Console.WriteLine("Authenticated : " + !string.IsNullOrEmpty(token.access_token));
        }

        public string GetResponse()
        {
            // Get the api response.

            string apiResponse = "";
            try
            {
                System.Net.WebResponse response = request.GetResponse();

                this.Status = ((System.Net.HttpWebResponse)response).StatusDescription;
                dataStream = response.GetResponseStream();

                System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                apiResponse = reader.ReadToEnd();
               // Console.WriteLine("API Response : {0}", apiResponse);

                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
           

            return apiResponse;
        }
    }
}
