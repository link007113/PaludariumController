using PaludariumController.Core.Interfaces;
using PaludariumController.Core.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.IO;
using System.Text.Json;
using PaludariumController.Core.Utils;
using CoordinateSharp;

namespace PaludariumController.Core.Services
{
    public class LightService : ILightsService
    {
        private readonly IDevice device;
        public Light Light { get; set; }
        public static string InstanceName { get; set; }

        private string FilePath { get {return $"files/{InstanceName.ToLower()}_light.json"; } } 
        public LightService(IDevice device)
        {
            this.device = device;
        }

        public LightRequest SetLights(Light light, bool doFade)
        {
            var result = device.SetLights(light, doFade);
            result.Name = $"{InstanceName} Light";
            this.Light = light;
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            File.WriteAllText(FileUtil.GetFilePath(FilePath), JsonSerializer.Serialize(light, options));

            return result;

        }
        public Light GetLight()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            if (File.Exists(FileUtil.GetFilePath(FilePath)))
            {
                Light light = JsonSerializer.Deserialize<Light>(File.ReadAllText(FileUtil.GetFilePath(FilePath)), options);
                if (light == null)
                {
                    return GetCurrentLight();
                }
                else
                {
                    return light;
                }
            }
            else { return GetCurrentLight(); }
        }

        public Light GetCurrentLight()
        {
            Coordinate coordinate = new Coordinate(52.20122169520473, 5.995868479602127, DateTime.Now);

            if (coordinate.CelestialInfo.IsSunUp)
            {
                return GetLightPerHour(DateTime.Now.Hour);
            }
            else { return new Light(Color.FromArgb(5, 0, 25)); }
        }

        public Light GetLightPerHour(int hour)
        {
            switch (hour)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                    return new Light(Color.Black);
                case 5:
                    return new Light(Color.FromArgb(52, 57, 99));
                case 6:
                    return new Light(Color.FromArgb(120, 131, 161));
                case 7:
                    return new Light(Color.FromArgb(189, 113, 53));
                case 8:
                    return new Light(Color.FromArgb(243, 211, 162));
                case 9:
                    return new Light(Color.FromArgb(245, 224, 207));
                case 17:
                    return new Light(Color.FromArgb(233, 148, 91));
                case 18:
                    return new Light(Color.FromArgb(228, 124, 3));
                case 19:
                    return new Light(Color.FromArgb(197, 146, 145));
                case 20:
                    return new Light(Color.FromArgb(164, 145, 190));
                case 21:
                    return new Light(Color.FromArgb(111, 53, 75));
                case 22:
                    return new Light(Color.FromArgb(31, 18, 48));
                case 23:
                    return new Light(Color.FromArgb(5, 0, 25));
                default:
                    return new Light(Color.Snow);
            }
        }

        public void SetInstanceName(string instanceName) 
        {
            InstanceName = instanceName;
        }
    }
}

