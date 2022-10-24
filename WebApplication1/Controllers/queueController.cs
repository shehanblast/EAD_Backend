using System;
using Microsoft.AspNetCore.Mvc;
using MongoExample.Services;
using MongoExample.Models;
using MongoDB.Bson;
using System.Linq;

namespace MongoExample.Controllers;
[Controller]
[Route("api/queue/[controller]")]
public class QueueController : Controller
{
    private readonly MongoDBService _mongoDBService;

    public QueueController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
    public async Task<List<Queue>> Get()
    {
        return await _mongoDBService.GetQueueAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Queue queue)
    {
        await _mongoDBService.CreateQueueAsync(queue);
        return CreatedAtAction(nameof(Get), new { id = queue.QueueId }, queue);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ChangeDepartureTime(string id, [FromBody] DateTime time)
    {
        List<Queue> queueList = new List<Queue>();
        queueList = await _mongoDBService.GetAsyncOneQueueUser(id);

        foreach (Queue item in queueList)

        {
            if (item.DepartureTime == new DateTime())
            {
                await _mongoDBService.ChangeDepartureTimeAsync(item.QueueId, time);
                break;

            }

        }

        return NoContent();
    }

    [HttpGet("GetOne/{id}")]
    public async Task<Queue> GetOne(string id)
    {
        return await _mongoDBService.GetAsyncOneQueue(id);
    }

    //get queue length according to stationId
    [HttpGet("GetOneQueueLength/{id}")]
    public async Task<int> GetQueueLength(string id)
    {
        List<Queue> queueList = new List<Queue>();
        queueList = await _mongoDBService.GetAsyncQueueLength(id);

        int count = queueList.Count();

        return count;
    }

    //get queue average time according to stationId
    [HttpGet("GetOneQueueAverage/{id}")]
    public async Task<TimeSpan> GetQueueAverage(string id)
    {
        List<Queue> queueList = new List<Queue>();
        queueList = await _mongoDBService.GetAsyncQueueLength(id);

        int count = 0;

        TimeSpan avg = new TimeSpan();

        TimeSpan time = new TimeSpan();
  
   
        foreach (Queue item in queueList)

        {
            if (item.DepartureTime  != new DateTime())
            {
                count++;
                time += item.DepartureTime.TimeOfDay.Subtract(item.ArrivalTime.TimeOfDay);
                
              
            }

        }

        avg = time / count;

        return avg;
    }

}

