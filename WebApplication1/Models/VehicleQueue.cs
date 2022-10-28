using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MongoExample.Models

{
    public class VehicleQueue
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? VehicleQueueId { get; set; }

        public string StationId { get; set; } = null;

        public string CustomerId { get; set; } = null;

        public string VeihicleModel { get; set; } = null;

        [BsonDateTimeOptions(Representation = BsonType.Document)]
        public DateTime QueueArrivalTime { get; set; }

        [BsonDateTimeOptions(Representation = BsonType.Document)]
        public DateTime QueueDepartureTime { get; set; } 
    }
}
