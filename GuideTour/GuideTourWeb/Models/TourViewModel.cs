using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideTourWeb.Models
{
    public class TourViewModel
    {
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("guideId")]
        public string GuideId { get; set; }

        [BsonElement("name")]
        public string GuideName { get; set; }

        [BsonElement("teamId")]
        public string TeamId { get; set; }

        [BsonElement("team")]
        public string Team { get; set; }

        [BsonElement("startedTour")]
        public DateTime? StartedTour { get; set; }

        [BsonElement("endedTour")]
        public DateTime? EndedTour { get; set; }

        [BsonElement("visitorName")]
        public string VisitorName { get; set; }
    }
}
