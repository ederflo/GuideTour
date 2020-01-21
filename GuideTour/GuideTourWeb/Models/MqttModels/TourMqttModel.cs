using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideTourWeb.Models.MqttModels
{
    public enum TourMqttState
    {
        Started,
        StartedAck,
        Ended,
        EndedAck
    };

    public class TourMqttModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("team")]
        public string Team { get; set; }

        [JsonProperty("guide")]
        public string Guide { get; set; }

        [JsonProperty("nameOfGuest")]
        public string NameOfGuests { get; set; }

        [JsonProperty("states")]
        public TourMqttState State { get; set; }

        [JsonProperty("atStation")]
        public string AtStation { get; set; }
    }
}
