using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideTourWeb.Models.MqttModels
{
    public class TourMqttModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("ifguideappid")]
        public string IfGuideAppId { get; set; }

        [JsonProperty("team")]
        public string Team { get; set; }

        [JsonProperty("guidename")]
        public string GuideName { get; set; }

        [JsonProperty("guestname")]
        public string GuestName { get; set; }
    }
}
