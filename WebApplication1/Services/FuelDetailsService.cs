using MongoExample.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoExample.Services;

public class FuelDetailsService
{

    private readonly IMongoCollection<FuelInfo> _fuelDetailsCollection;

    public FuelDetailsService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _fuelDetailsCollection = database.GetCollection<FuelInfo>(mongoDBSettings.Value.FuelInfoCollectionName);
    }

    /* REST API url for creating a fuel info document */
    public async Task InsertFuelInfo(FuelInfo fuelDetails)
    {
        await _fuelDetailsCollection.InsertOneAsync(fuelDetails);
        return;
    }

    /* REST API url for fetching all fuel information */
    public async Task<List<FuelInfo>> FetchAllFuelInfo()
    {
        return await _fuelDetailsCollection.Find(new BsonDocument()).ToListAsync();

    }

    /* REST API url for updating fuel info */
    public async Task EditFuelInfo(string id, FuelInfo fuelInfo)
    {
        FilterDefinition<FuelInfo> filter = Builders<FuelInfo>.Filter.Eq("FuelInfoId", id);
        UpdateDefinition<FuelInfo> update = Builders<FuelInfo>.Update.Set(p => p.StationId, fuelInfo.StationId)
            .Set(p => p.Type, fuelInfo.Type)
            .Set(p => p.ArrivalTime, fuelInfo.ArrivalTime)
            .Set(p => p.Status, fuelInfo.Status);
        await _fuelDetailsCollection.UpdateOneAsync(filter, update);
        return;
    }

    /* REST API url for fetching a single fuel info document */
    public async Task<FuelInfo> FetchOneFuelInfo(string id)
    {
        FilterDefinition<FuelInfo> filter = Builders<FuelInfo>.Filter.Eq("FuelInfoId", id);
        return await _fuelDetailsCollection.Find(filter).Limit(1).SingleAsync();

    }

    /* REST API url for fetching a fuel info document according to station ID */
    public async Task<List<FuelInfo>> FetchFuelInfoAccordingToStation(string id)
    {
        FilterDefinition<FuelInfo> filter = Builders<FuelInfo>.Filter.Eq("StationId", id);
        return await _fuelDetailsCollection.Find(filter).ToListAsync();

    }

    /* REST API url for fetching a fuel info document according to station ID and fuel ID */
    public async Task<List<FuelInfo>> FetchFuelInfoAccordingToStationAndFuel(string sId, string fName)
    {
        FilterDefinition<FuelInfo> filter = Builders<FuelInfo>.Filter.Where(p => p.StationId == sId && p.Type == fName);
        return await _fuelDetailsCollection.Find(filter).ToListAsync();

    }

    public async Task UpdateArrivalTime(string id, FuelInfo fuelInfo)
    {
        FilterDefinition<FuelInfo> filter = Builders<FuelInfo>.Filter.Eq("FuelInfoId", id);
        UpdateDefinition<FuelInfo> update = Builders<FuelInfo>.Update.Set(p => p.ArrivalTime, fuelInfo.ArrivalTime)
            .Set(p => p.Status, fuelInfo.Status);
        await _fuelDetailsCollection.UpdateOneAsync(filter, update);
        return;
    }
}

