using System;
using System.Collections.Generic;
using System.Text;

namespace PaludariumController.Core.Models
{
   public class LightRequest
    {
        public Light Light { get; set; }
        public bool Fade { get; set; }
        public string Response { get; set; }
        public bool Succes { get; set; }

    }
}
