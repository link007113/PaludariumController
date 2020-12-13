using PaludariumController.Core.Interfaces;
using PaludariumController.Core.Models;
using System;
using System.IO.Ports;
using System.Text;

namespace PaludariumController.Core.Services
{
 public   class TemperatureService : ITemperatureService

    {
        private readonly IDevice device;

        public TemperatureService(IDevice device)
        {
            this.device = device;
        }
        public TemperatureRequest GetTemp()
        {
          return device.GetTemp();
        }
    }
}
