using System;
using Microsoft.AspNetCore.Mvc;
using MongoExample.Services;
using MongoExample.Models;


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
        await _mongoDBService.ChangeDepartureTimeAsync(id, time);
        return NoContent();
    }

    [HttpGet("GetOne/{id}")]
    public async Task<Queue> GetOne(string id)
    {
        return await _mongoDBService.GetAsyncOneQueue(id);
    }
}

