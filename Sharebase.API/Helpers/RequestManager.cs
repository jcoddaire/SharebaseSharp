using System;
using System.IO;
using System.Net;
using System.Text;

namespace Sharebase.API.Helpers
{
    public class RequestManager
    {
        private readonly string _origin;
        private readonly string _token;

        public RequestManager(string origin, string token){
            _origin = origin;
            _token = token;
        }

        public HttpWebRequest GeneratePOSTRequest(string uri, string content, string login, string password, bool allowAutoRedirect)
        {
            return GenerateRequest(uri, content, "POST", null, null, allowAutoRedirect);
        }

        internal HttpWebRequest GenerateRequest(string uri, byte[] content, string method, string login, string password, bool allowAutoRedirect)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = method;

            request.AllowAutoRedirect = allowAutoRedirect;
            request.Headers.Add("Authorization", _token);
            request.Headers.Add("x-phoenix-app-id", "ShareBase");


            if (method == "POST" || method == "PATCH" || method == "PUT")
            {
                request.Accept = "application/json";
                request.ContentType = "application/json";

                if(content != null){
                    using (var streamWriter = request.GetRequestStream())
                    {
                        streamWriter.Write(content, 0, content.Length);
                    }
                }
                request.Headers.Add("Origin", _origin);	

            }
            return request;
        }
        internal HttpWebRequest GenerateRequest(string uri, string content, string method, string login, string password, bool allowAutoRedirect)
        {
            byte[] contentBytes = Encoding.UTF8.GetBytes(content);
            return GenerateRequest(uri, contentBytes, method, login, password, allowAutoRedirect);
        }

        internal HttpWebResponse GetResponse(HttpWebRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();

            }
            catch (WebException ex)
            {
                Console.WriteLine("Web exception occurred. Status code: {0}", ex.Status);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        public string GetResponseContent(HttpWebResponse response)
        {
            if (response == null)
            {
                throw new ArgumentNullException("response");
            }
            Stream dataStream = null;
            StreamReader reader = null;
            string responseFromServer = null;

            try
            {
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
                // Cleanup the streams and the response.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (dataStream != null)
                {
                    dataStream.Close();
                }
                response.Close();
            }
            return responseFromServer;
        }
    }
}