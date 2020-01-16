using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuideTourData.Models
{
    public class Teacher : Entity
    {
        [BsonElement("pinCode")]
        public int PinCode { get; set; }

        [BsonElement("lastAction")]
        public DateTime? LastAction { get; set; }
    
        public Teacher () : base("Teacher") { }
    }
}
