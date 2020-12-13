using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaludariumController.Helpers
{
    class ComPort
    {
        public static SerialPort Port { get; private set; }
        public static bool PortReady { get; private set; }
        public static string SensorPrefix { get { return ConfigurationManager.AppSettings["SensorPrefix"]; } }
        public static bool OpenPort(SerialPort port)
        {
            if (port == null)
            {
                if (!port.IsOpen)
                {
                    try
                    {
                        port.Open();
                        PortReady = true;
                    }
                    catch (Exception ex)
                    {
                        PortReady = false;
                        LogHelper.Log(ex.Message, "error");
                    }
                }
            }
            else
            {
                if (!port.IsOpen)
                {
                    port.Open();
                    PortReady = true;
                }
            }
            return PortReady;
        }

        public static bool SetComPort()
        {
            try
            {
                LogHelper.Log($"Found following port in config file {ConfigurationManager.AppSettings["ComPort"]}", "info");
                Port = new SerialPort(ConfigurationManager.AppSettings["ComPort"], 9600);
                OpenPort(Port);
                return Port.IsOpen;
            }
            catch (Exception e)
            {
                LogHelper.Log(e.Message, "error");
                return false;
            }
        }

        public static void WriteToPort(SerialPort port, string @byte)
        {
            if (!port.IsOpen)
            {
                port.Open();
            }

            port.Write(@byte);
            port.Write("\n");
            Thread.Sleep(50);
        }
    }
}
