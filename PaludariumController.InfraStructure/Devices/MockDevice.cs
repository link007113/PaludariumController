using PaludariumController.Core.Interfaces;
using PaludariumController.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaludariumController.InfraStructure.Devices
{
   public class MockDevice : IDevice
    {
        public Task<TemperatureRequest> GetTempAsync()
        {
            Random random = new Random();
            return Task.FromResult(new TemperatureRequest { Temperature = random.Next(15,23), Response = Response(), Succes = true });
        }

        public LightRequest SetLights(Light light, bool doFade)
        {          
            return new LightRequest { Light = light, Response = Response() + $"doFade = {doFade}", Succes = true };
        }

        private static string Response()
        {
            return "This response comes from the mock";
        }

        public CoolingRequest SetFan(bool state)
        {
            return new CoolingRequest { State = state ? "on" : "off", Response = Response(), Succes = true } ;
        }
    }
}
