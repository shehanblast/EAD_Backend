namespace MongoExample.Models;

public class MongoDBSettings {
    public string ConnectionURI {get;set;} = null!;
    public string DatabaseName {get;set;} = null!;
    public string Collectionname {get;set;} = null!;
    public string CollectionnameStation { get; set; } = null!;
    public string CollectionnameFuelType { get; set; } = null!;
    public string CollectionnameQueue { get; set; } = null!;
    public string CollectionnameDetails { get; set; } = null!;
}