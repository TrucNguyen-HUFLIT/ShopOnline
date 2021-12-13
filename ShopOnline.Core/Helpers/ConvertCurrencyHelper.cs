using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShopOnline.Core.Helpers
{
    public static class ConvertCurrencyHelper
    {
        private static DateTime LastUpdated { get; set; }
        private static double ExchangeRate { get; set; }

        public static async Task<int> ConvertVNDToUSD(double priceVND)
        {
            await ConvertVNDToUSD();
            return (int)Math.Round(priceVND / ExchangeRate);
        }

        private static async Task ConvertVNDToUSD()
        {
            if (LastUpdated.Date != DateTime.Now.Date && ExchangeRate == default)
            {
                try
                {
                    HttpClient httpClient = new();
                    const string API_KEY = "af8cf5a0-5b29-11ec-a3b6-e13601d8bd47";
                    string URL = $"https://freecurrencyapi.net/api/v2/latest?apikey={API_KEY}";
                    HttpResponseMessage httpResponse = await httpClient.GetAsync(URL);
                    var content = await httpResponse.Content.ReadAsStringAsync();

                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        Console.WriteLine(content);
                        ExchangeRate = 22500;
                    }
                    else
                    {
                        var result = JsonConvert.DeserializeObject<ResultResponse>(content);
                        ExchangeRate = result.Data.VND; //base on USD
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    ExchangeRate = 22500;
                }
            }
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
