using System;
using Microsoft.AspNetCore.Mvc;
using MongoExample.Services;
using MongoExample.Models;


namespace MongoExample.Controllers;

[Controller]
[Route("api/fuelStation/[controller]")]
public class FuelStationController : Controller
{
    private readonly FuelStationService _fuelStationService;

    /* Constructer that adds the service to this controller */
    public FuelStationController(FuelStationService mongoDBService)
    {
        _fuelStationService = mongoDBService;
    }

    /* REST API url for fetching all fuel station information */
    [HttpGet]
    public async Task<List<FuelStation>> Get()
    {
        return await _fuelStationService.FetchAllFuelStations();
    }

    /* REST API url for creating a fuel station document */
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] FuelStation fuelStation)
    {
        await _fuelStationService.InsertFuelStation(fuelStation);
        return CreatedAtAction(nameof(Get), new { id = fuelStation.StationId }, fuelStation);
    }

    /* REST API url for fetching a single fuel station document */
    [HttpGet("FetchOneStation/{id}")]
    public async Task<FuelStation> GetOne(string id)
    {
        return await _fuelStationService.FetchOneFuelStation(id);
    }

    /* REST API url for updating a fuel station document */
    [HttpPut("{id}")]
    public async Task<IActionResult> ChangeFuelStation(string id, [FromBody] FuelStation fuelStation)
    {
        await _fuelStationService.EditFuelStation(id, fuelStation);
        return NoContent();
    }

    /* REST API url for fetching a fuel station document according to city */
    [HttpGet("FetchStationAccordingtoCity")]
    public async Task<List<FuelStation>> GetStationAll(string city)
    {
        return await _fuelStationService.FetchFuelStationAccordingToCity(city);
    }

    /* REST API url for fetching a fuel station document according to user ID */
    [HttpGet("FetchStationAccordingtoOwnerId")]
    public async Task<List<FuelStation>> GetFuelDetailsAccordingToOwnerId(string ownerId)
    {
        return await _fuelStationService.FetchFuelStationAccordingToOwnerId(ownerId);
    }


}


