using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaludariumController.Helpers
{
    class Lights
    {
        public static void ColorCommand(string red, string blue, string green)
        {
            if (ComPort.Port.IsOpen)
            {
                LogHelper.Log($"Following colors will be send: \nRed: {red}\nGreen: {green}\nBlue: {blue}\n", "info");
                ComPort.Port.Write($"c3r{green}g{red}b{blue}");

                if (MqttAvailable)
                {
                    if (red == "000" && blue == "000" && green == "000")
                    {
                        UpdateMqttLight("light.axolotllight", "off", int.Parse(red), int.Parse(green), int.Parse(blue));
                    }
                    else
                    {
                        UpdateMqttLight("light.axolotllight", "on", int.Parse(red), int.Parse(green), int.Parse(blue));
                    }
                }

            }
        }
    }
}
