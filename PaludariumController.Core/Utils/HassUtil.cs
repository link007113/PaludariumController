using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using PaludariumController.Core.Interfaces;
using PaludariumController.Core.Models;
using PaludariumController.Core.Utils;
using HADotNet.Core;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace PaludariumController.Core.Utils
{
    class HassUtil
    {
        private static string HassApiKey => "TBA";
        private static bool HassAvailable { get { return NetworkUtil.PingHost("192.168.1.2", 8123); } }
        public static string GetApiKey()
        { 
        return HassApiKey;
        }

    }
}
