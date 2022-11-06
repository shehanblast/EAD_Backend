namespace MongoExample.Models;

public class MongoDBSettings {
    public string ConnectionURI {get;set;} = null!;
    public string DatabaseName {get;set;} = null!;
    public string FuelStationCollectionName { get; set; } = null!;
    public string QueueCollectionname { get; set; } = null!;
    public string FuelInfoCollectionName { get; set; } = null!;
}