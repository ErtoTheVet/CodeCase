using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Configuration.Models
{
    public class Parameters
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } //id kısmını mongodb otomatik olarak oluşturacak

        public string Name { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public bool IsActive { get; set; }

        public string ApplicationName { get; set; }
    }
}
