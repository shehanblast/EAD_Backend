using MongoExample.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoExample.Services;

public class QueueService
{
    private readonly IMongoCollection<VehicleQueue> _queueCollection;

    public QueueService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _queueCollection = database.GetCollection<VehicleQueue>(mongoDBSettings.Value.QueueCollectionname);
       
    }

    /* REST API url for creating a queue document */
    public async Task CreateQueueAsync(VehicleQueue vehicleQueue)
    {
        await _queueCollection.InsertOneAsync(vehicleQueue);
        return;
    }

    /* REST API url for fetching all queue information */
    public async Task<List<VehicleQueue>> FetchQueueAll()
    {
        return await _queueCollection.Find(new BsonDocument()).ToListAsync();

    }

    /* REST API url for updating a queue document */
    public async Task UpdateDepartureTime(string id, DateTime time)
    {
        FilterDefinition<VehicleQueue> filter = Builders<VehicleQueue>.Filter.Eq("VehicleQueueId", id);
        UpdateDefinition<VehicleQueue> update = Builders<VehicleQueue>.Update.Set(p => p.QueueDepartureTime, time);
        await _queueCollection.UpdateOneAsync(filter, update);
        return;
    }

    /* REST API url for fetching a single queue document */
    public async Task<VehicleQueue> FetchOneQueue(string id)
    {
        FilterDefinition<VehicleQueue> filter = Builders<VehicleQueue>.Filter.Eq("VehicleQueueId", id);
        return await _queueCollection.Find(filter).Limit(1).SingleAsync();

    }
    //get queue list according to user id
    public async Task<List<VehicleQueue>> FetchQueueAccordingToUser(string id)
    {
        FilterDefinition<VehicleQueue> filter = Builders<VehicleQueue>.Filter.Eq("CustomerId", id);
        return await _queueCollection.Find(filter).ToListAsync();

    }

    /* REST API url for fetching the  queue length according to station ID */
    public async Task<List<VehicleQueue>> FetchQueueTotal(string id)
    {
        FilterDefinition<VehicleQueue> filter = Builders<VehicleQueue>.Filter.Eq("StationId", id);
        return await _queueCollection.Find(filter).ToListAsync();

    }

    //get queue length according to Departure null - not used now
    public async Task<List<VehicleQueue>> FetchQueueCustomer(string id)
    {
        FilterDefinition<VehicleQueue> filter = Builders<VehicleQueue>.Filter.Eq("CustomerId", id);
        return await _queueCollection.Find(filter).ToListAsync();


    }

    //get queue length according to Departure null
    public async Task<List<VehicleQueue>> FetchQueueByQueueId(string id)
    {
        FilterDefinition<VehicleQueue> filter = Builders<VehicleQueue>.Filter.Eq("VehicleQueueId", id);
        return await _queueCollection.Find(filter).ToListAsync();


    }

    public async Task UpdateQueueDepartureTime(string id, VehicleQueue vehicleQueue)
    {
        FilterDefinition<VehicleQueue> filter = Builders<VehicleQueue>.Filter.Eq("VehicleQueueId", id);
        UpdateDefinition<VehicleQueue> update = Builders<VehicleQueue>.Update.Set(p => p.QueueDepartureTime, vehicleQueue.QueueDepartureTime);
        await _queueCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task<List<VehicleQueue>> CheckAsyncDepartureTime(string id)
    {
        FilterDefinition<VehicleQueue> filter = Builders<VehicleQueue>.Filter.Eq("CustomerId", id);
        return await _queueCollection.Find(filter).ToListAsync();


    }

}

