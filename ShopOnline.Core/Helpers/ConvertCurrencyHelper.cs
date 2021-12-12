using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShopOnline.Core.Helpers
{
    public static class ConvertCurrencyHelper
    {
        public static async Task<int> ConvertVNDToUSD(double priceVND)
        {
            double priceUSD = 0;
            try
            {
                HttpClient httpClient = new HttpClient();
                const string API_KEY = "af8cf5a0-5b29-11ec-a3b6-e13601d8bd47";
                string URL = $"https://freecurrencyapi.net/api/v2/latest?apikey={API_KEY}";
                HttpResponseMessage httpResponse = await httpClient.GetAsync(URL);
                var content = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine(content);
                    return (int)priceUSD;
                }

                var result = JsonConvert.DeserializeObject<ResultResponse>(content);
                var exchangeRate = result.Data.VND; //base on USD

                priceUSD = Math.Round(priceVND / exchangeRate);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return (int)priceUSD;
        }
    }

    class ResultResponse
    {
        public ConvertCurrencyResponse Data { get; set; }
    }

    class ConvertCurrencyResponse
    {
        public double VND { get; set; }
    }
}
