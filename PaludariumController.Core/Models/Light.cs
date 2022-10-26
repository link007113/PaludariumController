using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Text.Json.Serialization;
using PaludariumController.Core.Attributes.Validators.Light;

namespace PaludariumController.Core.Models
{
    public class Light
    {
        [Range(0, 255)]
        public int Red { get; set; }
        [Range(0, 255)]
        public int Blue { get; set; }
        [Range(0, 255)]
        public int Green { get; set; }

        public int Brightness { get { return Red + Green + Blue / 3; } set { } }
        public Light()
        {
            this.Red = 255;
            this.Blue = 255;
            this.Green = 255;
        }
        public Light(int red, int blue, int green)
        {
            this.Red = red;
            this.Blue = blue;
            this.Green = green;
        }
        
        public Light(Color color)
        {
            this.Red = color.R;
            this.Blue = color.B;
            this.Green = color.G;
        }


        public static Color GetColor(Light light)
        {
            return Color.FromArgb(light.Red, light.Green, light.Blue);
        }
    }
}
