using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaludariumController.Core.Interfaces;
using PaludariumController.Core.Models;
using Microsoft.AspNetCore.Mvc;
using PaludariumController.Core.Services;
using System.Drawing;

namespace PaludariumController.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LightController
    {
        private readonly ILightsService lightsService;

        public LightController(ILightsService lightsService)
        {
            this.lightsService = lightsService;
        }

        [HttpGet]
        public Light GetLight()
        {
            return lightsService.GetLight();
        }

        [HttpGet("SetLightsBy/CurrentLight")]
        public LightRequest SetLightToCurrent(bool doFade)
        {
            return lightsService.SetLights(lightsService.GetCurrentLight(), doFade);
        }
        [HttpGet("SetLightsBy/Hour/{id}")]
        public LightRequest SetLightsByHour(bool doFade, int id)
        {
            if (Enumerable.Range(0, 24).Contains(id))
            return lightsService.SetLights(lightsService.GetLightPerHour(id), doFade);
            else
                throw new ArgumentOutOfRangeException();
        }

        [HttpGet("SetLightsBy/RGB/")]
        public LightRequest SetLightsByRGB(int r, int g, int b, bool doFade )
        {
            Light light = new Light(r, b, g);
            return lightsService.SetLights(light, doFade);
        }

        [HttpGet("SetLightsBy/Colorname/{colorname}")]
        public LightRequest SetLightsByColorname(bool doFade, string colorname)
        {
            Light light = new Light(Color.FromName(colorname));
            return lightsService.SetLights(light, doFade);
        }

        [HttpPost]
        public LightRequest SetLights(Light light, bool doFade)
        {
            return lightsService.SetLights(light, doFade);
        }
    }
}
