using System.Net;

namespace Supplier.REST
{
    public interface IRequsetRestAgent
    {
        HttpWebRequest GenerateGETRequest(string uri);
        HttpWebRequest GeneratePOSTRequest(string uri, string content);
        HttpWebRequest GenerateRequest(string uri, string content, string method);
        T GetJsonDeserializedContent<T>(HttpWebResponse response) where T : class;
        HttpWebResponse GetResponse(HttpWebRequest request);
        string GetResponseContent(HttpWebResponse response);
        HttpWebResponse SendGETRequest(string uri);
        HttpWebResponse SendPOSTRequest(string uri, string content);
    }
}