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
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace PaludariumController.Core.Utils
{
    class MqttUtil
    {
        public static MqttClient MqttClient { get; private set; }

        public static bool MqttAvailable { get { return NetworkUtil.PingHost("192.168.1.2", 1883); } }
        public static void CreateMqttDevice(string domain, string entityId, string payload)
        {
            try            {
               
                var topic = $"homeassistant/{domain}/{entityId}/config";
                Console.WriteLine($"Following topic is used for creating {domain}.{entityId}: {topic}", "info");
                ushort msgId = MqttClient.Publish(topic, // topic
                               Encoding.UTF8.GetBytes(payload), // message body
                               MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, // QoS level
                               true); // retained
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString(), "error");
            }
        }

        public static void SetMqttClient()
        {
            try
            {
                if (MqttClient == null)
                {
                    MqttClient = new MqttClient("192.168.1.2");
                    byte code = MqttClient.Connect(Guid.NewGuid().ToString(), "hass", "3Erfoom1992");
                    Console.WriteLine($"MQTT Client is {MqttClient.IsConnected}: {MqttClient}", "info");
                    //MqttClient.MqttMsgPublishReceived += mqttClient_recievedMessage;

                    MqttClient.Subscribe(new String[] { "homeassistant/light/axolotllight/set" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message, "error"); ;
            }
        }
        public static void UpdateMqttDevice(string domain, string entityId, string state)
        {
            try
            {
                //MqttClient.MqttMsgPublished += mqttClient_MqttMsgPublished;
                var topic = $"homeassistant/{domain}/{entityId}/state";
                Console.WriteLine($"Following topic is used for updating {domain}.{entityId}: {topic} to state: {state}", "info");
                ushort msgId = MqttClient.Publish(topic, // topic
                               Encoding.UTF8.GetBytes(state), // message body
                               MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, // QoS level
                               true); // retained
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString(), "error");
            }
        }
        public static void UpdateMqttLight(string entityId, string state, int red, int green, int blue)
        {
            Color col = Color.FromArgb(red, green, blue);
            if (col == null)
            { col = Color.Black; }

            try
            {
                if (entityId.Contains("light."))
                {
                    entityId = entityId.Replace("light.", "");
                }

                string configpayload = $@" {{
                                           ""~"": ""homeassistant/light/{entityId}"",
                                           ""name"": ""{entityId}"",
                                           ""unique_id"": ""{entityId}"",
                                           ""cmd_t"": ""~/set"",
                                           ""stat_t"": ""~/state"",
                                           ""schema"": ""json"",
                                           ""brightness"": true
                                           ""rgb"": true
                                         }}";

                CreateMqttDevice("light", entityId, configpayload);

                Console.WriteLine($"Sending light info to MQTT", "info");
                LightRequest lightState = new LightRequest();
                Light light = new Light(col);                

                lightState.Light = light;               
                lightState.LightState = state.ToUpper();
                var lightJson = JsonConvert.SerializeObject(lightState);

                Console.WriteLine($"{lightJson}", "info");


                //MqttClient.MqttMsgPublished += mqttClient_MqttMsgPublished;
                var topic = $"homeassistant/light/{entityId}/state";
                Console.WriteLine($"Following topic is used for updating {entityId}: {topic} to state: {lightJson}", "info");
                ushort msgId = MqttClient.Publish(topic, // topic
                               Encoding.UTF8.GetBytes(lightJson), // message body
                               MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, // QoS level
                               true); // retained
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}", "error");
                Console.WriteLine($"{e.Message}", "error");
                Console.WriteLine($"{e.StackTrace}", "error");
                Console.WriteLine($"{e.InnerException}", "error");
            }
        }       
        //private static void mqttClient_recievedMessage(object sender, MqttMsgPublishEventArgs e)
        //{
        //    // Handle message received
        //    var message = System.Text.Encoding.Default.GetString(e.Message);
        //    Console.WriteLine($"Message received: {message} in topic {e.Topic}", "info");

        //    try
        //    {
        //        if (Service1.manualMode)
        //        {
        //            if (e.Topic.Contains("axolotllight"))
        //            {
        //                JsonTypes.LightState lightState = new JsonTypes.LightState();
        //                Color sentColor = Color.Snow;
        //                lightState = JsonConvert.DeserializeObject<JsonTypes.LightState>(message);
        //                if (lightState.state == "ON")
        //                {
        //                    if (lightState.color != null)
        //                    {
        //                        sentColor = Color.FromArgb(lightState.color.r, lightState.color.g, lightState.color.b);

        //                    }
        //                }
        //                else
        //                {
        //                    sentColor = Color.Black;
        //                }
        //                Service1.ColorFade(Service1.oldColor, sentColor, true, 3);
        //                UpdateMqttLight("axolotllight", lightState.state, sentColor.R, sentColor.G, sentColor.B);

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"{ex}", "error");
        //        Console.WriteLine($"{ex.Message}", "error");
        //        Console.WriteLine($"{ex.StackTrace}", "error");
        //        Console.WriteLine($"{ex.InnerException}", "error");
        //    }


        //}
    }
}
