using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaludariumController.Core.Interfaces;
using PaludariumController.Core.Models;

namespace PaludariumController.WebApi.Controllers
{
    [Route("api/[controller]")] //http://yoursite/api/Photos => controller == Photos
    [ApiController]
    public class TemperatureController : ControllerBase
    {
        private readonly ITemperatureService temperatureService;

        public TemperatureController(ITemperatureService temperatureService)
        {
            this.temperatureService = temperatureService;
        }

        [HttpGet("Current")]
        public async Task<TemperatureRequest> GetTempAsync()
        {
            return await temperatureService.GetTempAsync();
        }

        [HttpGet("Cooling")]
        public CoolingRequest GetCooling()
        {
            return temperatureService.GetCooling();
        }
        [HttpGet("Cooling/{state}")]
        public CoolingRequest SetCooling(bool state)
        {
            return temperatureService.SetCooling(state);
        }
    }
}
