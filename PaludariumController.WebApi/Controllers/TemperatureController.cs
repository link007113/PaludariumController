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

        [HttpGet]
        public TemperatureRequest GetTemp()
        {
            return temperatureService.GetTemp();
        }
    }
}
