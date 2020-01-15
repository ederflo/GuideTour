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
    public class Team
    {
        [BsonElement("id")]
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public String Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("guides")]
        public List<Guide> Guides { get; set; } = new List<Guide>();
    }
}
