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
                const string API_KEY = "8dbc9848003844ae0f70";
                string URL = $"https://free.currconv.com/api/v7/convert?q=VND_USD&compact=ultra&apiKey={API_KEY}";
                HttpResponseMessage httpResponse = await httpClient.GetAsync(URL);
                var content = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine(content);
                    return (int)priceUSD;
                }

                var convertCurrencyResponse = JsonConvert.DeserializeObject<ConvertCurrencyResponse>(content);
                var exchangeRate = convertCurrencyResponse.VND_USD;
                priceUSD = Math.Round(priceVND * exchangeRate);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return (int)priceUSD;
        }
    }

    class ConvertCurrencyResponse
    {
        public double VND_USD { get; set; }
    }
}
