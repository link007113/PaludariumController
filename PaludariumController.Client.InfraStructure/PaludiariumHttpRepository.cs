﻿using PaludariumController.Core.Interfaces;
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
        private int portNumber= 5001;
        public PaludiariumHttpRepository(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public async Task<TemperatureRequest> GetTempAsync()
        {
           TemperatureRequest result;
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:{portNumber}/api/temperature");
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

        public CoolingRequest SetFan(bool state)
        {
            CoolingRequest result;
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:{portNumber}/api/fan");
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
                result = await JsonSerializer.DeserializeAsync<CoolingRequest>(responseStream, options);
            }
            else
            {
                result = new CoolingRequest();
            }
            return result;
        }

        public LightRequest SetLights(Light light, bool doFade = false)
        {
            var result = new LightRequest { Light = light };
            var client = clientFactory.CreateClient();

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var todoItemJson = new StringContent(JsonSerializer.Serialize(light, options), Encoding.UTF8, "application/json");
            string url;
            if (doFade)
                url = $"https://localhost:{portNumber}/api/light?doFade=true";
            else
                url = $"https://localhost:{portNumber}/api/light";


            using var response = client.PostAsync(url, todoItemJson);

            //httpResponse.EnsureSuccessStatusCode();


            result.Response = response.Result.Content.ToString();
            
            return result;
        }
    }
}
