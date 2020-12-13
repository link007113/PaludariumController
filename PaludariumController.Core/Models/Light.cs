using System.Drawing;
using System.Text.Json.Serialization;
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
            this.Red = red.PadLeft(3, '0');
            this.Blue = blue.PadLeft(3, '0');
            this.Green = green.PadLeft(3, '0');
        }

        public Light(int red, int blue, int green)
        {
            this.Red = red.ToString().PadLeft(3, '0');
            this.Blue = blue.ToString().PadLeft(3, '0');
            this.Green = green.ToString().PadLeft(3, '0');
        }
        public Light(Color color)
        {
            this.Red = color.R.ToString().PadLeft(3, '0');
            this.Blue = color.B.ToString().PadLeft(3, '0');
            this.Green = color.G.ToString().PadLeft(3, '0');
        }


        public static Color GetColor(Light light)
        {
            return Color.FromArgb(int.Parse(light.Red), int.Parse(light.Green), int.Parse(light.Blue));
        }
    }
}
