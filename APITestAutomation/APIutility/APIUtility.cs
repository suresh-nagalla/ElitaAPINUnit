using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITestAutomation.APIutility
{
    public class APIUtility
    {

        private RestClient restClient;
        private RestRequest restRequest;
        public APIUtility(string url)
        {
            restClient = new RestClient(url);
        }
        public RestRequest GetRestRequest(string endpoint, Method method)
        {
            return new RestRequest(endpoint, method);
        }

        public T Get<T>(string endpoint, Dictionary<string, string> headers = null, Dictionary<string, string> parameters = null)
        {
            restRequest = GetRestRequest(endpoint, Method.GET);

            if (headers != null) AddHeaders(headers);
            if (parameters != null) AddParameters(parameters);

            var data = restClient.Execute(restRequest);
            return JsonConvert.DeserializeObject<T>(data.Content);
        }

        public T Post<T, T1>(string endpoint, T1 body, Dictionary<string, string> headers = null, Dictionary<string, string> parameters = null)
        {
            restRequest = GetRestRequest(endpoint, Method.POST);
            if (headers != null) AddHeaders(headers);
            if (parameters != null) AddParameters(parameters);

            restRequest.AddJsonBody(JsonConvert.SerializeObject(body));

            var data = restClient.Execute(restRequest);

            return JsonConvert.DeserializeObject<T>(data.Content);
        }

        public void AddHeaders(Dictionary<string, string> headers)
        {
            foreach (KeyValuePair<string, string> key in headers)
            {
                restRequest.AddHeader(key.Key, key.Value);
            }
        }
        public void AddParameters(Dictionary<string, string> parameters)
        {
            foreach (KeyValuePair<string, string> key in parameters)
            {
                restRequest.AddParameter(key.Key, key.Value);
            }
        }
    }
}
