using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AtlasDTO;


namespace AtlasMVCAPI
{
    public class ServiceHelper : IDisposable
    {
        HttpClient client = new HttpClient();

        public string BaseServiceURL { get; set; }

        public ServiceHelper(string routePrefix)
        {
            BaseServiceURL = $"{ConfigurationManager.AppSettings["apiURL"]}{routePrefix}";

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Dispose()
        {
            client.Dispose();
        }

        // Get + T
        public T GetAsyncT<T>(string path)
        {
            string url = $"{BaseServiceURL}/{path}";

            T obj = default(T);
            HttpResponseMessage res = client.GetAsync(url).Result;
            if (res.IsSuccessStatusCode)
            {
                string mss = res.Content.ReadAsStringAsync().Result;
                T result = JsonConvert.DeserializeObject<T>(mss);

                return result;
            }

            return obj;
        }

        // Get + ResMessage
        public ResMessage GetAsyncNone(string path)
        {
            string url = $"{BaseServiceURL}/{path}";

            HttpResponseMessage res = client.GetAsync(url).Result;
            if (res.IsSuccessStatusCode)
            {
                string mss = res.Content.ReadAsStringAsync().Result;
                ResMessage result = JsonConvert.DeserializeObject<ResMessage>(mss);

                return result;
            }

            return null;
        }

        // Get + ResMessage<T>
        public ResMessage<T> GetAsync<T>(string path)
        {
            string url = $"{BaseServiceURL}/{path}";

            HttpResponseMessage res = client.GetAsync(url).Result;
            if (res.IsSuccessStatusCode)
            {
                string mss = res.Content.ReadAsStringAsync().Result;
                ResMessage<T> result = JsonConvert.DeserializeObject<ResMessage<T>>(mss);

                return result;
            }

            return null;
        }

        // Post + ResMessage
        public ResMessage PostAsyncNone<T>(string path, T t)
        {
            string url = $"{BaseServiceURL}/{path}";

            HttpResponseMessage res = client.PostAsJsonAsync(url, t).Result;
            if (res.IsSuccessStatusCode)
            {
                ResMessage result = JsonConvert.DeserializeObject<ResMessage>(res.Content.ReadAsStringAsync().Result);

                return result;
            }
            else
            {
                return null;
            }
        }

        // Post + ResMessage<T>
        public ResMessage<R> PostAsync<T, R>(string path, T t)
        {
            string url = $"{BaseServiceURL}/{path}";

            HttpResponseMessage res = client.PostAsJsonAsync(url, t).Result;
            if (res.IsSuccessStatusCode)
            {
                ResMessage<R> result = JsonConvert.DeserializeObject<ResMessage<R>>(res.Content.ReadAsStringAsync().Result);

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
