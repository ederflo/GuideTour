using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideTourData.Models
{
    public class Tour
    {
        [BsonElement("id")]
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public string Id { get; set; }

        [BsonElement("guideId")]
        public string GuideId { get; set; }

        [BsonElement("guideName")]
        public string GuideName { get; set; }

        [BsonElement("guideTeam")]
        public string GuideTeam { get; set; }

        [BsonElement("startedTour")]
        public DateTime? StartedTour { get; set; }

        [BsonElement("endedTour")]
        public DateTime? EndedTour { get; set; }

        [BsonElement("visitorName")]
        public string VisitorName { get; set; }

        [BsonElement("canceld")]
        [BsonDefaultValue("false")]
        public bool Canceld { get; set; } = false;
    }
}
