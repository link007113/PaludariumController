using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using PaludariumController.Core.Interfaces;
using PaludariumController.Core.Models;

namespace PaludariumController.InfraStructure.Devices
{
   public class ComDevice : IDevice
    {
        private readonly SerialPort serialPort;

        public ComDevice(SerialPort serialPort)
        {
            this.serialPort = serialPort;
        }

        public TemperatureRequest GetTemp()
        {
            TemperatureRequest result = new TemperatureRequest();
            try
            {
                if (serialPort.IsOpen)
                {
                    // LogHelper.Log("Trying to get temperature", "info");
                    serialPort.Write("c4");

                    result.Response = serialPort.ReadLine();
                    // LogHelper.Log($"Response: {response}", "info");
                }

                if (result.Response.Length < 12)
                {
                    result.Succes = false;
                }

                if (result.Response.Contains("Temperature"))
                {
                    float temp;
                    float.TryParse(result.Response.Substring(12).Replace('.', ','), out temp);
                    if (temp > 0)
                    {
                        // LogHelper.Log($"The Temperature is {temp}", "info");
                        result.Temperature = temp;
                    }
                }

                return result;
            }
            catch (Exception ex) { return null; }
        }

        public void WriteToPort(SerialPort port, string @byte)
        {
            if (!serialPort.IsOpen)
            {
                OpenPort();
            }

            serialPort.Write(@byte);
            serialPort.Write("\n");           
        }

        public void OpenPort()
        {
            if (serialPort == null)
            {
                if (!serialPort.IsOpen)
                {
                    try
                    {
                        serialPort.Open();
                 
                    }
                    catch (Exception ex)
                    {                       
                        //// LogHelper.Log(ex.Message, "error");
                    }
                }
            }
            else
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();         
                }
            }
           
        }

        public LightRequest SetLights(Light light)
        {

            LightRequest result = new LightRequest() { Light = light };
            if (serialPort.IsOpen)
            {
                // LogHelper.Log($"Following colors will be send: \nRed: {Light.Red}\nGreen: {Light.Green}\nBlue: {Light.Blue}\n", "info");
                serialPort.Write($"c3r{light.Green}g{light.Red}b{light.Blue}");
                result.Succes = true;
                return result;

            }
            else { result.Succes = false;
                return null;
            } 

        }
    }
}
