using Newtonsoft;
using Newtonsoft.Json;
using System;

using System.Drawing;

using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace PaludariumController.Helpers
{
    class Mqtt
    {
        public static MqttClient MqttClient { get; private set; }

        public static bool MqttAvailable { get { return Network.PingHost("192.168.1.2", 1883); } }
        public static void CreateMqttDevice(string domain, string entityId, string payload)
        {
            try
            {
                MqttClient.MqttMsgPublished += mqttClient_MqttMsgPublished;
                var topic = $"homeassistant/{domain}/{entityId}/config";
                LogHelper.Log($"Following topic is used for creating {domain}.{entityId}: {topic}", "info");
                ushort msgId = MqttClient.Publish(topic, // topic
                               Encoding.UTF8.GetBytes(payload), // message body
                               MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, // QoS level
                               true); // retained
            }
            catch (Exception e)
            {
                LogHelper.Log(e.ToString(), "error");
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
                    LogHelper.Log($"MQTT Client is {MqttClient.IsConnected}: {MqttClient}", "info");
                    MqttClient.MqttMsgPublishReceived += mqttClient_recievedMessage;

                    MqttClient.Subscribe(new String[] { "homeassistant/light/axolotllight/set" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                }

            }
            catch (Exception e)
            {
                LogHelper.Log(e.Message, "error"); ;
            }
        }
        public static void UpdateMqttDevice(string domain, string entityId, string state)
        {
            try
            {
                MqttClient.MqttMsgPublished += mqttClient_MqttMsgPublished;
                var topic = $"homeassistant/{domain}/{entityId}/state";
                LogHelper.Log($"Following topic is used for updating {domain}.{entityId}: {topic} to state: {state}", "info");
                ushort msgId = MqttClient.Publish(topic, // topic
                               Encoding.UTF8.GetBytes(state), // message body
                               MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, // QoS level
                               true); // retained
            }
            catch (Exception e)
            {
                LogHelper.Log(e.ToString(), "error");
            }
        }
        public static void UpdateMqttLight(string entityId, string state, int red, int green, int blue)
        {
            Color col = Color.FromArgb(red, green, blue);

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

                LogHelper.Log($"Sending light info to MQTT", "info");
                var lightState = new JsonTypes.LightState();
                JsonTypes.LightColor lightColor = new JsonTypes.LightColor();
                if (col == null)
                { col = Color.Black; }

                lightColor.r = red;
                lightColor.g = green;
                lightColor.b = blue;
                lightState.color = lightColor;
                lightState.brightness = (red + green + blue) / 3;
                lightState.state = state.ToUpper();
                var lightJson = JsonConvert.SerializeObject(lightState);

                LogHelper.Log($"{lightJson}", "info");


                MqttClient.MqttMsgPublished += mqttClient_MqttMsgPublished;
                var topic = $"homeassistant/light/{entityId}/state";
                LogHelper.Log($"Following topic is used for updating {entityId}: {topic} to state: {lightJson}", "info");
                ushort msgId = MqttClient.Publish(topic, // topic
                               Encoding.UTF8.GetBytes(lightJson), // message body
                               MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, // QoS level
                               true); // retained
            }
            catch (Exception e)
            {
                LogHelper.Log($"{e}", "error");
                LogHelper.Log($"{e.Message}", "error");
                LogHelper.Log($"{e.StackTrace}", "error");
                LogHelper.Log($"{e.InnerException}", "error");
            }
        }

        private static void mqttClient_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            //  LogHelper.Log($"MessageId = {e.MessageId} Published = {e.IsPublished}", "info");
        }
        private static void mqttClient_recievedMessage(object sender, MqttMsgPublishEventArgs e)
        {
            // Handle message received
            var message = System.Text.Encoding.Default.GetString(e.Message);
            LogHelper.Log($"Message received: {message} in topic {e.Topic}", "info");

            try
            {
                if (Service1.manualMode)
                {
                    if (e.Topic.Contains("axolotllight"))
                    {
                        JsonTypes.LightState lightState = new JsonTypes.LightState();
                        Color sentColor = Color.Snow;
                        lightState = JsonConvert.DeserializeObject<JsonTypes.LightState>(message);
                        if (lightState.state == "ON")
                        {
                            if (lightState.color != null)
                            {
                                sentColor = Color.FromArgb(lightState.color.r, lightState.color.g, lightState.color.b);

                            }
                        }
                        else
                        {
                            sentColor = Color.Black;
                        }
                        Service1.ColorFade(Service1.oldColor, sentColor, true, 3);
                        UpdateMqttLight("axolotllight", lightState.state, sentColor.R, sentColor.G, sentColor.B);

                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log($"{ex}", "error");
                LogHelper.Log($"{ex.Message}", "error");
                LogHelper.Log($"{ex.StackTrace}", "error");
                LogHelper.Log($"{ex.InnerException}", "error");
            }


        }
    }
}
