using System;
using System.Configuration;

namespace PaludariumController.Helpers
{
    class Temp
    {
        public static float Temperature { get; private set; }
        public static bool HasFans { get { return ConfigurationManager.AppSettings["HasFans"] == "True" ? true : false; } }
 
        public static void FanCommand(string state)
        {
            if (!ComPort.Port.IsOpen) return;
            switch (state)
            {
                case "on": { ComPort.Port.Write("c5"); break; }
                case "off": { ComPort.Port.Write("c6"); break; }

                default:
                    throw new Exception("Unexpected Case");
            }
        }

        public static bool GetTemp(out string response, out float temp)
        {
            
            temp = 0;
            response = "";
            try
            {

                if (ComPort.Port.IsOpen)
                {
                    LogHelper.Log("Trying to get temperature", "info");
                   ComPort.Port.Write("c4");

                    response = ComPort.Port.ReadLine();
                    LogHelper.Log($"Response: {response}", "info");
                }

                if (response.Length < 12)
                {
                    return false;
                }

                if (response.Contains("Temperature"))
                {
                    float.TryParse(response.Substring(12).Replace('.', ','), out temp);
                    if (temp > 0)
                    {
                        LogHelper.Log($"The Temperature is {temp}", "info");
                        Temperature = temp;
                        return true;
                    }
                    return false;
                }

                return false;

            }
            catch (Exception e)
            {
                LogHelper.Log($"{e}", "error");
                LogHelper.Log($"{e.Message}", "error");
                LogHelper.Log($"{e.StackTrace}", "error");
                LogHelper.Log($"{e.InnerException}", "error");
                return false;
            }
        }

        public static bool GetTemp()
        {
            return GetTemp(out _, out _);
        }

    }
}
