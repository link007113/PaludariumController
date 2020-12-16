using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaludariumController.Core.Interfaces;
using PaludariumController.Core.Models;
using Microsoft.AspNetCore.Mvc;
using PaludariumController.Core.Services;

namespace PaludariumController.WebApi.Controllers
{
    [Route("api/[controller]")] //http://yoursite/api/Photos => controller == Photos
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

        [HttpGet("CurrentLight")]
        public LightRequest SetLightToCurrent(bool doFade)
        {
            return lightsService.SetLights(lightsService.GetCurrentLight(DateTime.Now.Hour), doFade);
        }

        [HttpPost]
        public LightRequest SetLights(Light light, bool doFade)
        {
            return lightsService.SetLights(light, doFade);
        }
    }
}
