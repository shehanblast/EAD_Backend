using MongoExample.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoExample.Services;

public class FuelStationService
{
    private readonly IMongoCollection<FuelStation> _fuelStationCollection;

    public FuelStationService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _fuelStationCollection = database.GetCollection<FuelStation>(mongoDBSettings.Value.FuelStationCollectionName);
       
    }

    /* REST API url for creating a fuel station document */
    public async Task InsertFuelStation(FuelStation fuelStation)
    {
        await _fuelStationCollection.InsertOneAsync(fuelStation);
        return;
    }

    /* REST API url for fetching all fuel station information */
    public async Task<List<FuelStation>> FetchAllFuelStations()
    {
        return await _fuelStationCollection.Find(new BsonDocument()).ToListAsync();

    }

    /* REST API url for fetching a single fuel station document */
    public async Task<FuelStation> FetchOneFuelStation(string id)
    {
        FilterDefinition<FuelStation> filter = Builders<FuelStation>.Filter.Eq("StationId", id);
        return await _fuelStationCollection.Find(filter).Limit(1).SingleAsync();

    }

    /* REST API url for updating a fuel station document */
    public async Task EditFuelStation(string id, FuelStation fuelStation)
    {
        FilterDefinition<FuelStation> filter = Builders<FuelStation>.Filter.Eq("StationId", id);
        UpdateDefinition<FuelStation> update = Builders<FuelStation>.Update.Set(p => p.StationName, fuelStation.StationName)
            .Set(p => p.Location, fuelStation.Location)
            .Set(p => p.OwnerId, fuelStation.OwnerId)
            .Set(p => p.StationNo, fuelStation.StationNo)
            .Set(p => p.Town, fuelStation.Town);
        await _fuelStationCollection.UpdateOneAsync(filter, update);
        return;
    }

    /* REST API url for fetching a fuel station document according to city */
    public async Task<List<FuelStation>> FetchFuelStationAccordingToCity(string town)
    {
        FilterDefinition<FuelStation> filter = Builders<FuelStation>.Filter.Eq("Town", town);
        return await _fuelStationCollection.Find(filter).ToListAsync();

    }

    /* REST API url for fetching a fuel station document according to user ID */
    public async Task<List<FuelStation>> FetchFuelStationAccordingToOwnerId(string ownerId)
    {
        FilterDefinition<FuelStation> filter = Builders<FuelStation>.Filter.Eq("OwnerId", ownerId);
        return await _fuelStationCollection.Find(filter).ToListAsync();

    }

    /* REST API url for fetching a fuel station id document according to station name */
    public async Task<FuelStation> FetchFuelStationIdAccordingToStationname(string name)
    {
        FilterDefinition<FuelStation> filter = Builders<FuelStation>.Filter.Eq("StationName", name);
        return await _fuelStationCollection.Find(filter).Limit(1).SingleAsync();

    }
}

