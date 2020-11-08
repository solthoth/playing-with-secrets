using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace playing_with_secrets.data
{
    public interface IWeatherByZipCodeRepository
    {
        Task<WeatherByZipCode> GetByZipCode(string zipcode);
    }

    public class WeatherByZipCodeRepository : IWeatherByZipCodeRepository
    {
        private readonly IRapidApiOptions context;
        public WeatherByZipCodeRepository(IRapidApiOptions options)
        {
            context = options;
        }

        public Task<WeatherByZipCode> GetByZipCode(string zipcode) =>
            ExecuteRequest<WeatherByZipCode>($"getweatherzipcode?zip={zipcode}");

        private async Task<T> ExecuteRequest<T>(string uri)
        {
            var request = CreateRapidApiRequest(uri);
            var client = new HttpClient();
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        private HttpRequestMessage CreateRapidApiRequest(string uri)
        {
            return new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{GetRequestUri()}/{uri}"),
                Headers = {
                        { "x-rapidapi-key", context.Key },
                        { "x-rapidapi-host", context.WeatherByZipCode },
                    },
            };
        }

        private string GetRequestUri() => context.HostTemplate.Replace("{host}", context.WeatherByZipCode);

    }
}
