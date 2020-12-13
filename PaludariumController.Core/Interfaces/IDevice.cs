using PaludariumController.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaludariumController.Core.Interfaces
{
    public interface IDevice
    {
        public TemperatureRequest GetTemp();
        public LightRequest SetLights(Light light, bool doFade = false);
    }
}
