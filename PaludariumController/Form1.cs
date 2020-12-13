using System;
using System.Diagnostics;
using System.Drawing;

using System.Threading.Tasks;
using System.Timers;
using PaludariumController.Helpers;
using NLog;

using System.Windows.Forms;

namespace PaludariumController
{
    public partial class Form1 : Form
    {

        private static readonly System.Timers.Timer Timer = new System.Timers.Timer();
        private static bool hassFanState = true;
        private static bool firstCheckAfterStartup = true;
        private static bool controllerFanState = true;
        private static bool oldFanState = true;
        private static int hour;
     Hass.HassAvailable;
     Mqtt.MqttAvailable;
        private static int serverAvailableRetryCount = 0;

        public Form1()
        {
            InitializeComponent();
            LogHelper.Log("Program is starting", "info");
            Hass.InitHass();
            while (ComPort.Port == null) { ComPort.SetComPort(); }
            while (Mqtt.MqttClient == null) { Mqtt.SetMqttClient(); }
            Timer.Interval = 10000; // 10 seconds
            Timer.Elapsed += OnTimer;
            Timer.Start();
            LogHelper.Log("Timer Started", "info");
            OnTimer(null, null);
        }


        

    }
}
