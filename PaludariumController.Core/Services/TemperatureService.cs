using PaludariumController.Core.Interfaces;
using PaludariumController.Core.Models;
using PaludariumController.Core.Utils;
using System;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PaludariumController.Core.Services
{
    public class TemperatureService : ITemperatureService

    {
        private readonly IDevice device;
        public static string InstanceName { get; set; }

        private string FilePath = $"files/{InstanceName}_cooling.json";
        public TemperatureService(IDevice device)
        {
            this.device = device;
        }

        public Task<TemperatureRequest> GetTempAsync()
        {
            return device.GetTempAsync();
        }

        public CoolingRequest GetCooling() 
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            if (File.Exists(FileUtil.GetFilePath(FilePath)))
            {
                CoolingRequest cooling = JsonSerializer.Deserialize<CoolingRequest>(File.ReadAllText(FileUtil.GetFilePath(FilePath)), options);
                if (cooling == null)
                {
                    return new CoolingRequest() { State = "Unknown", Response = "Unknown", Succes = true };
                }
                else
                {
                    return cooling;
                }
            }
            else 
            {
                return new CoolingRequest() { State = "Unknown", Response = "Unknown", Succes = true };
            }                

        }

        public CoolingRequest SetCooling(bool state)
        {
            var result = device.SetFan(state);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            File.WriteAllText(FileUtil.GetFilePath(FilePath), JsonSerializer.Serialize(result, options));

            return result;
        }
        public void SetInstanceName(string instanceName)
        {
            InstanceName = instanceName;
        }
    }
}
