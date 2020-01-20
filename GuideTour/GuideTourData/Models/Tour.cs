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
    public class Tour : Entity
    {
        [BsonElement("guideId")]
        public string GuideId { get; set; }

        [BsonElement("teacherId")]
        public string TeacherId { get; set; }

        [BsonElement("startedTour")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? StartedTour { get; set; }

        [BsonElement("endedTour")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? EndedTour { get; set; }

        [BsonElement("visitorName")]
        public string VisitorName { get; set; }

        [BsonElement("canceld")]
        [BsonDefaultValue("false")]
        public bool Canceld { get; set; } = false;

        public Tour () : base("Tour") { }
    }
}
