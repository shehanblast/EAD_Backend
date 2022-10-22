using MongoExample.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoExample.Services;

public class MongoDBService
{
    private readonly IMongoCollection<Playlist> _playlistCollection;
    private readonly IMongoCollection<FuelStation> _fuelStationCollection;
    //private readonly IMongoCollection<FuelType> _fuelTypeCollection;
    private readonly IMongoCollection<Queue> _queueCollection;
    private readonly IMongoCollection<FuelDetails> _fuelDetailsCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _playlistCollection = database.GetCollection<Playlist>(mongoDBSettings.Value.Collectionname);
        _fuelStationCollection = database.GetCollection<FuelStation>(mongoDBSettings.Value.CollectionnameStation);
        //_fuelTypeCollection = database.GetCollection<FuelType>(mongoDBSettings.Value.CollectionnameFuelType);
        _queueCollection = database.GetCollection<Queue>(mongoDBSettings.Value.CollectionnameQueue);
        _fuelDetailsCollection = database.GetCollection<FuelDetails>(mongoDBSettings.Value.CollectionnameDetails);
    }

    // Playsit controllers ----------------------------------
    public async Task CreateAsync(Playlist playlist)
    {
        await _playlistCollection.InsertOneAsync(playlist);
        return;
    }

    public async Task<List<Playlist>> GetAsync()
    {
        return await _playlistCollection.Find(new BsonDocument()).ToListAsync();

    }

    public async Task AddTOPlaylistAsync(string id, string movieId)
    {
        FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Eq("Id", id);
        UpdateDefinition<Playlist> update = Builders<Playlist>.Update.AddToSet<string>("items", movieId);
        await _playlistCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task DeleteAsync(string id)
    {
        FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Eq("Id", id);
        await _playlistCollection.DeleteOneAsync(filter);
        return;
    }

    public async Task<Playlist> GetAsyncOne(string id)
    {
        FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Eq("Id", id);
        return await _playlistCollection.Find(filter).Limit(1).SingleAsync();

    }

    public async Task<List<Playlist>> GetAsyncOne(string id,string un)
    {
        FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Where(p => p.username == un && p.Id == id);
        return await _playlistCollection.Find(filter).ToListAsync();

    }

    // ----------------------------------

    // Fuel station controllers ----------------------------------
    public async Task CreateStationAsync(FuelStation fuelStation)
    {
        await _fuelStationCollection.InsertOneAsync(fuelStation);
        return;
    }

    public async Task<List<FuelStation>> GetStationAsync()
    {
        return await _fuelStationCollection.Find(new BsonDocument()).ToListAsync();

    }

    public async Task<FuelStation> GetAsyncOneStation(string id)
    {
        FilterDefinition<FuelStation> filter = Builders<FuelStation>.Filter.Eq("StationId", id);
        return await _fuelStationCollection.Find(filter).Limit(1).SingleAsync();

    }

    public async Task ChangeFuelStattionAsync(string id, FuelStation fuelStation)
    {
        FilterDefinition<FuelStation> filter = Builders<FuelStation>.Filter.Eq("StationId", id);
        UpdateDefinition<FuelStation> update = Builders<FuelStation>.Update.Set(p => p.Name, fuelStation.Name)
            .Set(p => p.Address, fuelStation.Address)
            .Set(p => p.OwnerId, fuelStation.OwnerId)
            .Set(p => p.StationNo, fuelStation.StationNo)
            .Set(p => p.City, fuelStation.City);
        await _fuelStationCollection.UpdateOneAsync(filter, update);
        return;
    }

    //get fuel stations according to city
    public async Task<List<FuelStation>> GetAsyncStationsCity(string city)
    {
        FilterDefinition<FuelStation> filter = Builders<FuelStation>.Filter.Eq("City", city);
        return await _fuelStationCollection.Find(filter).ToListAsync();

    }

    // ----------------------------------

    // Fuel type controllers ----------------------------------
    //public async Task CreateTypeAsync(FuelType fuelType)
    //{
    //    await _fuelTypeCollection.InsertOneAsync(fuelType);
    //    return;
    //}

    //public async Task<List<FuelType>> GetTypeAsync()
    //{
    //    return await _fuelTypeCollection.Find(new BsonDocument()).ToListAsync();

    //}

    // ----------------------------------

    // Queue controllers ----------------------------------
    public async Task CreateQueueAsync(Queue queue)
    {
        await _queueCollection.InsertOneAsync(queue);
        return;
    }

    public async Task<List<Queue>> GetQueueAsync()
    {
        return await _queueCollection.Find(new BsonDocument()).ToListAsync();

    }

    public async Task ChangeDepartureTimeAsync(string id, DateTime time)
    {
        FilterDefinition<Queue> filter = Builders<Queue>.Filter.Eq("QueueId", id);
        UpdateDefinition<Queue> update = Builders<Queue>.Update.Set(p => p.DepartureTime, time);
        await _queueCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task<Queue> GetAsyncOneQueue(string id)
    {
        FilterDefinition<Queue> filter = Builders<Queue>.Filter.Eq("QueueId", id);
        return await _queueCollection.Find(filter).Limit(1).SingleAsync();

    }

    // ----------------------------------

    // Fuel details controllers ----------------------------------
    public async Task CreateFuelAsync(FuelDetails fuelDetails)
    {
        await _fuelDetailsCollection.InsertOneAsync(fuelDetails);
        return;
    }

    public async Task<List<FuelDetails>> GetFuelAsync()
    {
        return await _fuelDetailsCollection.Find(new BsonDocument()).ToListAsync();

    }

    public async Task ChangeFuelDetailsAsync(string id, FuelDetails fuelDetails)
    {
        FilterDefinition<FuelDetails> filter = Builders<FuelDetails>.Filter.Eq("FdId", id);
        UpdateDefinition<FuelDetails> update = Builders<FuelDetails>.Update.Set(p => p.StationId, fuelDetails.StationId)
            .Set(p => p.FuelName, fuelDetails.FuelName)
            .Set(p => p.FuelArrivalTime, fuelDetails.FuelArrivalTime)
            .Set(p => p.FuelFinish, fuelDetails.FuelFinish);
        await _fuelDetailsCollection.UpdateOneAsync(filter, update);
        return;
    }


    public async Task<FuelDetails> GetAsyncOneFuel(string id)
    {
        FilterDefinition<FuelDetails> filter = Builders<FuelDetails>.Filter.Eq("FdId", id);
        return await _fuelDetailsCollection.Find(filter).Limit(1).SingleAsync();

    }

    //get fuel details according to stationId
    public async Task<List<FuelDetails>> GetAsyncStations(string id)
    {
        FilterDefinition<FuelDetails> filter = Builders<FuelDetails>.Filter.Eq("StationId", id);
        return await _fuelDetailsCollection.Find(filter).ToListAsync();

    }

    //get fuel details according to station id and fuel id
    public async Task<List<FuelDetails>> GetAsyncStationFuel(string sId, string fName)
    {
        FilterDefinition<FuelDetails> filter = Builders<FuelDetails>.Filter.Where(p => p.StationId == sId && p.FuelName == fName);
        return await _fuelDetailsCollection.Find(filter).ToListAsync();

    }

    // ----------------------------------


}