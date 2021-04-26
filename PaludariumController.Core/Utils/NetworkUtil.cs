using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace PaludariumController.Core.Utils
{
   public class NetworkUtil
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
                Console.WriteLine($"Error pinging host:'" + hostUri + ":" + portNumber.ToString() + "'", "info");
                return false;
            }
        }
    }
}
