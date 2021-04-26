using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using PaludariumController.Core.Interfaces;
using PaludariumController.Core.Models;
using PaludariumController.Core.Utils;
using HADotNet.Core;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace PaludariumController.Core.Utils
{
    class HttpUtil
    {

        public static HttpClient HttpClient { get; private set; }
        public static async void PostHttpSensorRequest(string entityId, string state, string deviceClass, string uom, string friendlyName)
        {
            try
            {
                using (HttpClient)
                {
                    var bearerValue = $"Bearer {HassUtil.GetApiKey()}";

                    using (HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("POST"), $"http://192.168.1.2:8123/api/states/{entityId}"))
                    {
                        request.Headers.TryAddWithoutValidation("Authorization", bearerValue);

                        request.Content = new StringContent("{\"state\": \"" + state + "\", \"attributes\": {\"unit_of_measurement\": \"" + uom + "\",\"device_class\": \"" + deviceClass + "\", \"friendly_name\": \"" + friendlyName + "\"}}");
                        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                        var response = await HttpClient.SendAsync(request);
                        if (!response.IsSuccessStatusCode)
                        { Console.WriteLine($"Request: {response.RequestMessage} Result: {response}", "error"); }
                        else
                        {
                            { Console.WriteLine($"Request: {response.RequestMessage} Result: {response.StatusCode}", "info"); }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}", "error");
                Console.WriteLine($"{e.Message}", "error");
                Console.WriteLine($"{e.StackTrace}", "error");
                Console.WriteLine($"{e.InnerException}", "error");
            }
        }

        public static async Task<bool> PostHttpServiceRequest(string entityId, string domain, string service)
        {
            try
            {
                using (HttpClient)
                {
                    var bearerValue = $"Bearer {HassUtil.GetApiKey()}";

                    using (HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("POST"), $"http://192.168.1.2:8123/api/services/{domain}/{service}"))
                    {
                        request.Headers.TryAddWithoutValidation("Authorization", bearerValue);

                        request.Content = new StringContent("{\"entity_id\":\"" + domain + "." + entityId + "\"}");
                        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                        var response = await HttpClient.SendAsync(request);
                        if (!response.IsSuccessStatusCode)
                        { Console.WriteLine($"Request: {response.RequestMessage} Result: {response}", "error"); }
                        else
                        {
                            Console.WriteLine($"Request: {response.RequestMessage} Result: {response.StatusCode}", "info");
                        }

                        return response.IsSuccessStatusCode;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}", "error");
                Console.WriteLine($"{e.Message}", "error");
                Console.WriteLine($"{e.StackTrace}", "error");
                Console.WriteLine($"{e.InnerException}", "error");

                return false;
            }
        }
        public static async Task<bool> PostHttpServiceRequestWithState(string entityId, string domain, string service, string state)
        {
            try
            {
                using (HttpClient)
                {
                    var bearerValue = $"Bearer {HassUtil.GetApiKey()}";

                    using (HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("POST"), $"http://192.168.1.2:8123/api/services/{domain}/{service}"))
                    {
                        request.Headers.TryAddWithoutValidation("Authorization", bearerValue);

                        request.Content = new StringContent("{\"entity_id\":\"" + domain + "." + entityId + "\", \"value\":\"" + state + "\"}");
                        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                        var response = await HttpClient.SendAsync(request);
                        if (!response.IsSuccessStatusCode)
                        { Console.WriteLine($"Request: {response.RequestMessage} Result: {response}", "error"); }
                        else
                        {
                            Console.WriteLine($"Request: {response.RequestMessage} Result: {response.StatusCode}", "info");
                        }

                        return response.IsSuccessStatusCode;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}", "error");
                Console.WriteLine($"{e.Message}", "error");
                Console.WriteLine($"{e.StackTrace}", "error");
                Console.WriteLine($"{e.InnerException}", "error");

                return false;
            }
        }

        public static async Task<HassEntity> GetHttpSensorRequest(string entityId)
        {
            try
            {
                using (HttpClient)
                {
                    var bearerValue = $"Bearer {HassUtil.GetApiKey()}";
                    using (HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("GET"), $"http://192.168.1.2:8123/api/states/{entityId}"))
                    {
                        request.Headers.TryAddWithoutValidation("Authorization", bearerValue);
                        var response = await HttpClient.SendAsync(request);
                        if (!response.IsSuccessStatusCode)
                        {
                            Console.WriteLine($"Request: {response.RequestMessage} \n Result: {response}", "error");
                            return null;
                        }
                        else
                        {
                            Console.WriteLine($"Request: {response.RequestMessage} Result: {response.StatusCode}", "info");
                            HassEntity state = JsonConvert.DeserializeObject<HassEntity>(await response.Content.ReadAsStringAsync());
                            return state;
                        }

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}", "error");
                Console.WriteLine($"{e.Message}", "error");
                Console.WriteLine($"{e.StackTrace}", "error");
                Console.WriteLine($"{e.InnerException}", "error");
                return null;
            }
        }
    }
}
