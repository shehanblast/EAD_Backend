using System;
using Microsoft.AspNetCore.Mvc;
using MongoExample.Services;
using MongoExample.Models;


namespace MongoExample.Controllers;

[Controller]
[Route("api/station/[controller]")]
public class FuelStationController : Controller
{
    private readonly MongoDBService _mongoDBService;

    public FuelStationController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
    public async Task<List<FuelStation>> Get()
    {
        return await _mongoDBService.GetStationAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] FuelStation fuelStation)
    {
        await _mongoDBService.CreateStationAsync(fuelStation);
        return CreatedAtAction(nameof(Get), new { id = fuelStation.StationId }, fuelStation);
    }

    [HttpGet("GetOne/{id}")]
    public async Task<FuelStation> GetOne(string id)
    {
        return await _mongoDBService.GetAsyncOneStation(id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ChangeFuelStation(string id, [FromBody] FuelStation fuelStation)
    {
        await _mongoDBService.ChangeFuelStattionAsync(id, fuelStation);
        return NoContent();
    }

    //get fuel stations according to city
    [HttpGet("GetStationCity")]
    public async Task<List<FuelStation>> GetStationAll(string city)
    {
        return await _mongoDBService.GetAsyncStationsCity(city);
    }


}


