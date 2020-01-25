using GuideTourData.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideTourWeb.Models
{
    public class TeamViewModel
    {
        [BsonElement("teamId")]
        public string TeamId { get; set; }

        [BsonElement("teamName")]
        public string TeamName { get; set; }

        [BsonElement("guides")]
        public List<Guide> Guides { get; set; } = new List<Guide>();
    }
}
