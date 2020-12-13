using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PaludariumController.Helpers
{
    class Network
    {
        public static bool PingHost(string hostUri, int portNumber)
        {
            try
            {
                using (var client = new TcpClient(hostUri, portNumber))
                    return true;
            }
            catch (SocketException ex)
            {
                LogHelper.Log($"Error pinging host:'" + hostUri + ":" + portNumber.ToString() + "'", "info");
                return false;
            }
        }
       
    }
}
