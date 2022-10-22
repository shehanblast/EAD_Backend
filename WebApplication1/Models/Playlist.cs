using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MongoExample.Models;

public class Playlist{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set;}

    public string username {get; set;} = null;

    [BsonElement("items")]
    [JsonPropertyName("item")]
    public List<string> moviesIds {get; set;} = null;
}