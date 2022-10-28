using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MongoExample.Models
{
    public class FuelInfo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? FuelInfoId { get; set; }

        public string StationId { get; set; } = null;

        public string Type { get; set; } = null;

        public bool Status { get; set; }

        [BsonDateTimeOptions(Representation = BsonType.Document)]
        public DateTime ArrivalTime { get; set; } 

     
    }
}
