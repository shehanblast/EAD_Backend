using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MongoExample.Models

{
    public class Queue
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? QueueId { get; set; }

        public string StationId { get; set; } = null;

        public string UserId { get; set; } = null;

        public string VeihicleType { get; set; } = null;

        [BsonDateTimeOptions(Representation = BsonType.Document)]
        public DateTime ArrivalTime { get; set; }

        [BsonDateTimeOptions(Representation = BsonType.Document)]
        public DateTime DepartureTime { get; set; } 
    }
}
