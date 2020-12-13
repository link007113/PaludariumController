using PaludariumController.Core.Interfaces;
using System;
using PaludariumController.Core.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PaludariumController.Client.InfraStructure
{
    public class PaludiariumHttpRepository : IDevice
    {
        private readonly IHttpClientFactory clientFactory;

        public PaludiariumHttpRepository(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public async Task<TemperatureRequest> GetTempAsync()
        {
           TemperatureRequest result;
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:5001/api/temperature");
            request.Headers.Add("Accept", "application/json");
            //request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };
                result = await JsonSerializer.DeserializeAsync<TemperatureRequest>(responseStream, options);
            }
            else
            {
                result = new TemperatureRequest();
            }
            return result;
        }

        public LightRequest SetLights(Light light, bool doFade = false)
        {
            throw new NotImplementedException();
        }
    }
}
