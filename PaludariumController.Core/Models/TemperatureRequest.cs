using System;
using System.Collections.Generic;
using System.Text;

namespace PaludariumController.Core.Models
{
    public class TemperatureRequest
    {
        public float Temperature { get; set; }
        public string Response { get; set; }
        public bool Succes { get; set; }

    }
}
