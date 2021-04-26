using PaludariumController.Core.Models;
using System.Drawing;

namespace PaludariumController.Core.Interfaces
{
    public interface ILightsService
    {
        public Light GetCurrentLight();
        public LightRequest SetLights(Light light, bool doFade);
        public Light GetLight();
        public Light GetLightPerHour(int hour);

        public void SetInstanceName(string instanceName);

    }
}
