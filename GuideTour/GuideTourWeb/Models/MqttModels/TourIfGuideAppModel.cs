using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideTourWeb.Models.MqttModels
{
    public class TourIfGuideAppModel
    {
        [BsonElement("ifguideappid")]
        public string IfGuideAppId { get; set; }

        [BsonElement("team")]
        public string Team { get; set; }

        [BsonElement("guidemail")]
        public string GuideMail { get; set; }
    }
}
