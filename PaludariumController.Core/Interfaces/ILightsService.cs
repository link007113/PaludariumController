using PaludariumController.Core.Models;
using System.Drawing;

namespace PaludariumController.Core.Interfaces
{
    public interface ILightsService
    {
        public Light GetCurrentLight(int hour);
        public LightRequest SetLights(Light light, bool doFade);
        public Light GetLight();
    }
}
