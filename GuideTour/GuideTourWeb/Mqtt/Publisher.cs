using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;

namespace GuideTourWeb.Mqtt
{
    public class Publisher
    {
        public static readonly string BROKER_URL = "broker.hivemq.com";
        private MqttClient client;

        public Publisher()
        {

            //string clientId = "guideTour";
            //try
            //{
            //    client = new MqttClient(BROKER_URL, clientId);
            //}
            //catch (MqttException e)
            //{
            //    e.printStackTrace();
            //    System.exit(1);
            //}
        }
    }
}
