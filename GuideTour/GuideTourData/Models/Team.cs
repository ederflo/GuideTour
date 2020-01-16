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
    public class Team : Entity
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        public Team() : base("Team") { }
    }
}
