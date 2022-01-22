using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TransactionDEMO_WEB.Models;

namespace TransactionDEMO_WEB.APIRepo
{
    public class APIRequest<T>
    {
        #region Base API
        public const string APIKeyToCheck = "G93979A51C8C46712DD2C8271587B262";
        private static string BaseUrl = "https://localhost:44387/";

        public static async Task<T> Get(string url)
        {
            url = BaseUrl + url;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("APIKey", APIKeyToCheck);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (var response = await client.GetAsync(string.Format(url)))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var objsJsonString = await response.Content.ReadAsStringAsync();
                            var jsonContent = JsonConvert.DeserializeObject<T>(objsJsonString);

                            return jsonContent;
                        }
                        else
                        {
                            return default(T);
                        }
                    }
                }
            }
            catch
            {
                return default(T);
            }
        }

        public static async Task<List<Transaction>> Post(string url, List<Transaction> entity)
        {
            url = BaseUrl + url;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("APIKey", APIKeyToCheck);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (var content = new StringContent(JsonConvert.SerializeObject(entity), UTF8Encoding.UTF8, "application/json"))
                {
                    using (var response = await client.PostAsync(url, content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var objsJsonString = await response.Content.ReadAsStringAsync();
                            var jsonContent = JsonConvert.DeserializeObject<List<Transaction>>(objsJsonString);
                            return jsonContent;
                        }
                        else
                        {
                            return default(List<Transaction>);
                        }
                    }
                }
            }
        }

        #endregion

    }
}
