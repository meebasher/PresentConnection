using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Supplier.REST
{
    public class RequsetRestAgent : IRequsetRestAgent
    {
        public string GetResponseContent(HttpWebResponse response)
        {
            if (response == null)
            {
                throw new ArgumentNullException("response");
            }

            string responseFromServer = null;
            try
            {
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    responseFromServer = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                response.Close();
            }
            return responseFromServer;
        }

        public T GetJsonDeserializedContent<T>(HttpWebResponse response) where T : class
        {
            if (response == null)
            {
                throw new ArgumentNullException("response");
            }

            T parsedObject = null;
            try
            {
                string responseFromServer = null;
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    responseFromServer = streamReader.ReadToEnd();
                }
                parsedObject = JsonConvert.DeserializeObject<T>(responseFromServer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                response.Close();
            }
            return parsedObject;
        }

        public HttpWebResponse SendPOSTRequest(string uri, string content)
        {
            HttpWebRequest request = GeneratePOSTRequest(uri, content);
            return GetResponse(request);
        }

        public HttpWebResponse SendGETRequest(string uri)
        {
            HttpWebRequest request = GenerateGETRequest(uri);
            return GetResponse(request);
        }

        public HttpWebRequest GenerateGETRequest(string uri)
        {
            return GenerateRequest(uri, null, "GET");
        }

        public HttpWebRequest GeneratePOSTRequest(string uri, string content)
        {
            return GenerateRequest(uri, content, "POST");
        }

        public HttpWebRequest GenerateRequest(string uri, string content, string method)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }
            // Create a request using a URL that can receive a post. 
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            // Set the Method property of the request to POST.
            request.Method = method;

            if (method == "POST")
            {
                // Convert POST data to a byte array.
                byte[] byteArray = Encoding.UTF8.GetBytes(content);
                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/json";
                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;
                // Get the request stream.
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.
                dataStream.Close();
            }
            return request;
        }

        public HttpWebResponse GetResponse(HttpWebRequest request)
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
    }
}
