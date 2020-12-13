using HADotNet.Core;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System;

namespace PaludariumController.Helpers
{
    class Hass
    {
        public static string HassApiKey => "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJjMzllZGVmNGJhZmQ0NmY3OWY5NjhhZjE3NzhkMGQxYyIsImlhdCI6MTU5MTQyODg0MCwiZXhwIjoxOTA2Nzg4ODQwfQ.tfOHpLOAtNVNzBGROmbJTgI-aCbJy1usQORV0P_WSBQ";
        public static bool HassAvailable { get { return Network.PingHost("192.168.1.2", 8123); } }
        public static void InitHass()
        {
            ClientFactory.Initialize("http://192.168.1.2:8123/", HassApiKey);
        }
        public static async Task<bool> PostHttpServiceRequest(string entityId, string domain, string service)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var bearerValue = $"Bearer {HassApiKey}";

                    using (HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("POST"), $"http://192.168.1.2:8123/api/services/{domain}/{service}"))
                    {
                        request.Headers.TryAddWithoutValidation("Authorization", bearerValue);

                        request.Content = new StringContent("{\"entity_id\":\"" + domain + "." + entityId + "\"}");
                        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                        var response = await httpClient.SendAsync(request);
                        if (!response.IsSuccessStatusCode)
                        { LogHelper.Log($"Request: {response.RequestMessage} Result: {response}", "error"); }
                        else
                        {
                            LogHelper.Log($"Request: {response.RequestMessage} Result: {response.StatusCode}", "info");
                        }

                        return response.IsSuccessStatusCode;
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.Log($"{e}", "error");
                LogHelper.Log($"{e.Message}", "error");
                LogHelper.Log($"{e.StackTrace}", "error");
                LogHelper.Log($"{e.InnerException}", "error");

                return false;
            }
        }
        public static async Task<bool> PostHttpServiceRequestWithState(string entityId, string domain, string service, string state)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var bearerValue = $"Bearer {HassApiKey}";

                    using (HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("POST"), $"http://192.168.1.2:8123/api/services/{domain}/{service}"))
                    {
                        request.Headers.TryAddWithoutValidation("Authorization", bearerValue);

                        request.Content = new StringContent("{\"entity_id\":\"" + domain + "." + entityId + "\", \"value\":\"" + state + "\"}");
                        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                        var response = await httpClient.SendAsync(request);
                        if (!response.IsSuccessStatusCode)
                        { LogHelper.Log($"Request: {response.RequestMessage} Result: {response}", "error"); }
                        else
                        {
                            LogHelper.Log($"Request: {response.RequestMessage} Result: {response.StatusCode}", "info");
                        }

                        return response.IsSuccessStatusCode;
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.Log($"{e}", "error");
                LogHelper.Log($"{e.Message}", "error");
                LogHelper.Log($"{e.StackTrace}", "error");
                LogHelper.Log($"{e.InnerException}", "error");

                return false;
            }
        }
        public static async Task<JsonTypes.EntityState> GetHttpSensorRequest(string entityId)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var bearerValue = $"Bearer {HassApiKey}";
                    JsonTypes.EntityState state;
                    using (HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("GET"), $"http://192.168.1.2:8123/api/states/{entityId}"))
                    {
                        request.Headers.TryAddWithoutValidation("Authorization", bearerValue);
                        var response = await httpClient.SendAsync(request);
                        if (!response.IsSuccessStatusCode)
                        {
                            LogHelper.Log($"Request: {response.RequestMessage} \n Result: {response}", "error");
                            return null;
                        }
                        else
                        {
                            LogHelper.Log($"Request: {response.RequestMessage} Result: {response.StatusCode}", "info");
                            state = JsonConvert.DeserializeObject<JsonTypes.EntityState>(await response.Content.ReadAsStringAsync());
                            return state;
                        }

                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.Log($"{e}", "error");
                LogHelper.Log($"{e.Message}", "error");
                LogHelper.Log($"{e.StackTrace}", "error");
                LogHelper.Log($"{e.InnerException}", "error");
                return null;
            }
        }
        public static async void PostHttpSensorRequest(string entityId, string state, string deviceClass, string uom, string friendlyName)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var bearerValue = $"Bearer {HassApiKey}";

                    using (HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("POST"), $"http://192.168.1.2:8123/api/states/{entityId}"))
                    {
                        request.Headers.TryAddWithoutValidation("Authorization", bearerValue);

                        request.Content = new StringContent("{\"state\": \"" + state + "\", \"attributes\": {\"unit_of_measurement\": \"" + uom + "\",\"device_class\": \"" + deviceClass + "\", \"friendly_name\": \"" + friendlyName + "\"}}");
                        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                        var response = await httpClient.SendAsync(request);
                        if (!response.IsSuccessStatusCode)
                        { LogHelper.Log($"Request: {response.RequestMessage} Result: {response}", "error"); }
                        else
                        {
                            { LogHelper.Log($"Request: {response.RequestMessage} Result: {response.StatusCode}", "info"); }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.Log($"{e}", "error");
                LogHelper.Log($"{e.Message}", "error");
                LogHelper.Log($"{e.StackTrace}", "error");
                LogHelper.Log($"{e.InnerException}", "error");
            }
        }
    }
}
