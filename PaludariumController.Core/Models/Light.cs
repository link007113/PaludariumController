using System.Drawing;
using PaludariumController.Core.Attributes.Validators.Light;

namespace PaludariumController.Core.Models
{
    public class Light
    {
        [LightValue]
        public string Red { get; set; }
        [LightValue]
        public string Blue { get; set; }
        [LightValue]
        public string Green { get; set; }
        public Light()
        {
            this.Red = "255";
            this.Blue = "255";
            this.Green = "255";
        }
        public Light(string red, string blue, string green)
        {
            this.Red = red;
            this.Blue = blue;
            this.Green = green;
        }

        public Light(Color color)
        {
            this.Red = color.R.ToString();
            this.Blue = color.B.ToString();
            this.Green = color.G.ToString();
        }
    }
}
