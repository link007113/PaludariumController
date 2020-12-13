using PaludariumController.Core.Interfaces;
using PaludariumController.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaludariumController.InfraStructure.Devices
{
   public class MockDevice : IDevice
    {
        public TemperatureRequest GetTemp()
        {
            Random random = new Random();
            return new TemperatureRequest { Temperature = random.Next(15,23), Response = "This Response Comes From the mock", Succes = true };
        }

        public LightRequest SetLights(Light light)
        {          
            return new LightRequest { Light = light, Response = "This Response Comes From the mock", Succes = true };
        }
    }
}
