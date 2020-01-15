using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuideTourData.Models
{
    public class Teacher
    {
        [BsonElement("id")]
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public string Id { get; set; }

        [BsonElement("pinCode")]
        public int PinCode { get; set; }

        [BsonElement("lastAction")]
        public DateTime? LastAction { get; set; }
    }
}
