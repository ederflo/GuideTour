using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuideTourData.Models
{
    public class Teacher : Entity
    {

        public Teacher() : base("Teacher") { }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("pinCode")]
        public int PinCode { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("lastAction")]
        public DateTime? LastAction { get; set; }
    }
}
