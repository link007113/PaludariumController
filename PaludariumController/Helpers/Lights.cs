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
            }
        }
    }
}
