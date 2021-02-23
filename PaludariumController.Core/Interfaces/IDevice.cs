using PaludariumController.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaludariumController.Core.Interfaces
{
    public interface IDevice
    {        
        Task<TemperatureRequest> GetTempAsync();
        public LightRequest SetLights(Light light, bool doFade = false);
        public CoolingRequest SetFan(bool state);
    }
}
