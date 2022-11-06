using System;
using Microsoft.AspNetCore.Mvc;
using MongoExample.Services;
using MongoExample.Models;
using MongoDB.Bson;
using System.Linq;
using System.Globalization;
using System.Text.Json.Nodes;

namespace MongoExample.Controllers;
[Controller]
[Route("api/queue/[controller]")]
public class vehicleQueueController : Controller
{
    private readonly QueueService _queueService;

    /* Constructer that adds the service to this controller */
    public vehicleQueueController(QueueService mongoDBService)
    {
        _queueService = mongoDBService;
    }

    /* REST API url for fetching all queue information */
    [HttpGet]
    public async Task<List<VehicleQueue>> Get()
    {
        return await _queueService.FetchQueueAll();
    }

    /* REST API url for creating a queue document */
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] VehicleQueue vehicleQueue)
    {
        await _queueService.CreateQueueAsync(vehicleQueue);
        return CreatedAtAction(nameof(Get), new { id = vehicleQueue.VehicleQueueId }, vehicleQueue);
    }

    /* REST API url for updating a queue document - not used */
    //[HttpPatch("{id}")]
    //public async Task<JsonObject> ChangeDepartureTime(string id, [FromBody] c time)
    //{
    //    List<VehicleQueue> vehicleList = new List<VehicleQueue>();
    //    vehicleList = await _queueService.FetchQueueAccordingToUser(id);

    //    foreach (VehicleQueue vehicle in vehicleList)

    //    {
    //        if (vehicle.QueueDepartureTime == new DateTime())
    //        {
    //            await _queueService.UpdateDepartureTime(vehicle.VehicleQueueId, time);
    //            break;

    //        }

    //    }

    //    JsonObject keyValuePairs = new JsonObject();

    //    return keyValuePairs;
    //}

    /* REST API url for updating a queue document */
    [HttpPatch("{id}")]
    public async Task<JsonObject> UpdateQueue(string id, [FromBody] VehicleQueue vehicleQueue)
    {
    
        await _queueService.UpdateQueueDepartureTime(id, vehicleQueue);

        JsonObject keyValuePairs = new JsonObject();

        return keyValuePairs;
    }

    /* REST API url for fetching a single queue document */
    [HttpGet("GetOne/{id}")]
    public async Task<VehicleQueue> GetOne(string id)
    {
        return await _queueService.FetchOneQueue(id);
    }

    /* REST API url for fetching the  queue length according to station ID */
    [HttpGet("GetOneQueueLength/{id}")]
    public async Task<int> GetQueueLength(string id)
    {
        List<VehicleQueue> queueList = new List<VehicleQueue>();
        queueList = await _queueService.FetchQueueTotal(id);

        int count = queueList.Count();

        return count;
    }

    /* REST API url for fetching queue average according to station ID */
    [HttpGet("GetOneQueueAverage/{id}")]
    public async Task<TimeSpan> GetQueueAverage(string id)
    {
        List<VehicleQueue> queueList = new List<VehicleQueue>();
        queueList = await _queueService.FetchQueueTotal(id);

        int count = 0;

        TimeSpan average = new TimeSpan();
        TimeSpan totalTimeSpent = new TimeSpan();
   
        foreach (VehicleQueue item in queueList)
        {
            if (item.QueueDepartureTime  != new DateTime())
            {
                count++;
                totalTimeSpent += item.QueueDepartureTime.TimeOfDay - item.QueueArrivalTime.TimeOfDay;          
            }
        }

        if(count == 0)
        {
            count = 1;
        }

        average = totalTimeSpent / count;

        return average;
    }


    /* REST API url for fetching a fuel info document according to station ID */
    [HttpGet("FetchQueueFromUser/{id}")]
    public async Task<List<VehicleQueue>> FetchQueueAccordingToUser(string id)
    {
        return await _queueService.FetchQueueAccordingToUser(id);
    }

    [HttpGet("FetchQueueFromStation/{id}")]
    public async Task<List<VehicleQueue>> FetchQueueAccordingToStation(string id)
    {
        return await _queueService.FetchQueueTotal(id);
    }


    [HttpGet("FetchQueueArrivalTime/{id}")]
    public async Task<string> FetchQueueTimeSpent(string id)
    {
        List<VehicleQueue> queueList = new List<VehicleQueue>();
        queueList = await _queueService.FetchQueueByQueueId(id);

        DateTime time = new DateTime();

        foreach (VehicleQueue item in queueList)
        {
            time = item.QueueArrivalTime;

        }

        int hours = time.TimeOfDay.Hours;
        int minutes = time.TimeOfDay.Minutes;
        int seconds = time.TimeOfDay.Seconds;

        string returnTime = hours + ":" + minutes + ":" + seconds;

        return returnTime;
    }

    //get queue length according to Departure null - not used
    [HttpGet("GetOneQueueCustomer/{id}")]
    public async Task<TimeSpan> GetQueueCustomer(string id)
    {
        List<VehicleQueue> queueList = new List<VehicleQueue>();
        queueList = await _queueService.FetchQueueCustomer(id);


        TimeSpan time = new TimeSpan();

        foreach (VehicleQueue item in queueList)


        {
            if (item.QueueDepartureTime == new DateTime())
            {

                return item.QueueArrivalTime.TimeOfDay;

            }

        }

        return new DateTime().TimeOfDay;

    }

    //check departure time exisits
    [HttpGet("CheckDepartureTime")]
    public async Task<VehicleQueue> CheckDepartureTime(string id)
    {
        List<VehicleQueue> queueList = new List<VehicleQueue>();
        queueList = await _queueService.CheckAsyncDepartureTime(id);


        VehicleQueue queue = new VehicleQueue();


        foreach (VehicleQueue item in queueList)

        {
            if (item.QueueDepartureTime == new DateTime())
            {

                queue = item;
            }


        }

        return queue;


    }

}

