﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Text.Json;
using System.Threading;
using PaludariumController.Core.Interfaces;
using PaludariumController.Core.Models;
using PaludariumController.Core.Utils;

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
                if (!serialPort.IsOpen)
                {
                    OpenPort();
                }

                // LogHelper.Log("Trying to get temperature", "info");
                serialPort.Write("c4");

                result.Response = serialPort.ReadLine();
                // LogHelper.Log($"Response: {response}", "info");


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

        public void ColorCommand(string red, string blue, string green)
        {
            serialPort.Write($"c3r{green}g{red}b{blue}");
        }


        public LightRequest SetLights(Light light, bool doFade)
        {
            LightRequest result = new LightRequest() { Light = light };            
            try
            {
                if (!serialPort.IsOpen)
                {
                    OpenPort();
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                Light oldLight = JsonSerializer.Deserialize<Light>(File.ReadAllText(FileUtil.GetFilePath("Light.json")), options);

                if (doFade && oldLight != light && result.LightState == "on")
                {

                    result.Fade = doFade;
                    int discreteUnits = 25;
                    Color fadeTo = Light.GetColor(light);
                    Color baseClr = Light.GetColor(oldLight);
                    float correctionFactor = 0.0f;
                    float corFactorStep = 1.0f / discreteUnits;

                    for (int i = 0; i < discreteUnits; i++)
                    {
                        correctionFactor += corFactorStep;
                        float red = (fadeTo.R - baseClr.R) * correctionFactor + baseClr.R;
                        float green = (fadeTo.G - baseClr.G) * correctionFactor + baseClr.G;
                        float blue = (fadeTo.B - baseClr.B) * correctionFactor + baseClr.B;
                        int redInt = (int)red;
                        int greenInt = (int)green;
                        int blueInt = (int)blue;
                        ColorCommand(redInt.ToString().PadLeft(3, '0'), blueInt.ToString().PadLeft(3, '0'), greenInt.ToString().PadLeft(3, '0'));
                        Thread.Sleep(500);
                    }
                    
                    result.Succes = true;
                }
                else
                {
                    ColorCommand(light.Red.PadLeft(3, '0'), light.Blue.PadLeft(3, '0'), light.Green.PadLeft(3, '0'));
                    
                    result.Succes = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Response = ex.Message;
                result.Succes = false;
                return result;
            }
           
           
        }
    }
}

