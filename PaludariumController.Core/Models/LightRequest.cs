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
        public string LightState
        {
            get
            {
                if (int.Parse(this.Light.Red) == 0 && int.Parse(this.Light.Blue) == 0 && int.Parse(this.Light.Green) == 0)
                {
                    return "off";
                }
                else
                {
                    return "on";
                }

            }
            set { }
        }
    }
}
