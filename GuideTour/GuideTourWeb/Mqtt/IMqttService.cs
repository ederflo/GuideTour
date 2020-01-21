using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace GuideTourWeb.Mqtt
{
    public interface IMqttService
    {
        void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e);
    }
}
