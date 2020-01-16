using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuideTourData.Models
{
    public class Entity
    { 
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        public Entity (string type)
        {
            Type = type;
        }
    }
}
