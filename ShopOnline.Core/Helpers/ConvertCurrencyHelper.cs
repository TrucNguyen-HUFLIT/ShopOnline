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
                const string API_KEY = "30f0f3c9a3-14328b43be-r38fyu";
                string URL = $"https://api.fastforex.io/fetch-one?from=VND&to=USD&api_key={API_KEY}";
                HttpResponseMessage httpResponse = await httpClient.GetAsync(URL);
                var content = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine(content);
                    return (int)priceUSD;
                }

                var convertCurrencyResponse = JsonConvert.DeserializeObject<ConvertCurrencyResponse>(content);
                var exchangeRate = convertCurrencyResponse.Result.USD;
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
        public string Base { get; set; }
        public TypeExchangeRate Result { get; set; }
        public DateTime Updated { get; set; }
    }

    class TypeExchangeRate
    {
        public double USD { get; set; }
        public double VND { get; set; }
    }
}
