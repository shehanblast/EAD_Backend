using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MongoExample.Models
{
    public class FuelStation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? StationId { get; set; }

        public string Name { get; set; } = null;

        public string Address { get; set; } = null;

        public string OwnerId { get; set; } = null;

        public string StationNo { get; set; } = null;

        public string City { get; set; } = null;


    }
}
