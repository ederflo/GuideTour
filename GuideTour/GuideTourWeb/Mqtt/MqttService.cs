using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace GuideTourWeb.Mqtt
{
    public class MqttService
    {
        public static readonly string StartUrl = "/TDOT/tours/started";
        public static readonly string StartAckUrl = "/TDOT/tours/startedAck";
        public static readonly string EndedUrl = "/TDOT/tours/ended";
        public static readonly string EndedAckUrl = "/TDOT/tours/endedAck";

        private static readonly string[] topics = { "/TDOT/tours/started" };
        private static readonly string brokerUrl = "broker.hivemq.com";
        private static readonly string clientId = "guideTour";
        private static MqttService instance = null;
        private static readonly object padlock = new object();

        public readonly MqttClient client;

        public static MqttService Instance {
            get {
                lock(padlock)
                {
                    if (instance == null)
                        Init();
                    return instance;
                }
            }

            set {
                instance = value;
            }
        }

        public MqttService()
        {
            try
            {
                client = new MqttClient(brokerUrl);
                client.MqttMsgPublishReceived += MqttMsgPublishReceived;
                client.Connect(clientId);
                Console.WriteLine(client.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public static void Init()
        {
            if (instance == null)
                instance = new MqttService();
            Instance.client.Subscribe(topics, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }

        public void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Console.WriteLine(e.Topic);
        }
    }
}
