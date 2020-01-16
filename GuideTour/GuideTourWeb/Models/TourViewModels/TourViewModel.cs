using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideTourWeb.Models.TourViewModels
{
    public class TourViewModel
    {
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("guideId")]
        public string GuideId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("teamId")]
        public string TeamId { get; set; }

        [BsonElement("team")]
        public string Team { get; set; }

        [BsonElement("startedTime")]
        public DateTime StartedTime { get; set; }

        [BsonElement("endedTime")]
        public DateTime EndedTime { get; set; }

        [BsonElement("visitorName")]
        public string VisitorName { get; set; }
    }
}
