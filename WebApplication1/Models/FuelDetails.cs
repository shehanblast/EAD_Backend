using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MongoExample.Models
{
    public class FuelDetails
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? FdId { get; set; }

        public string StationId { get; set; } = null;

        public string FuelName { get; set; } = null;

        [BsonDateTimeOptions(Representation = BsonType.Document)]
        public DateTime FuelArrivalTime { get; set; } 

        public bool FuelFinish { get; set; } 
    }
}
