using System;
using Microsoft.AspNetCore.Mvc;
using MongoExample.Services;
using MongoExample.Models;
using System.Text.Json.Nodes;

namespace MongoExample.Controllers;

[Controller]
[Route("api/fuelInfo/[controller]")]
public class fuelInfoController : Controller
{
    private readonly FuelDetailsService _fuelDetailsService;

    /* Constructer that adds the service to this controller */
    public fuelInfoController(FuelDetailsService fuelDetailsService)
    {
        _fuelDetailsService = fuelDetailsService;
    }

    /* REST API url for fetching all fuel information */
    [HttpGet]
    public async Task<List<FuelInfo>> Get()
    {
        return await _fuelDetailsService.FetchAllFuelInfo();
    }

    /* REST API url for creating a fuel info document */
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] FuelInfo fuelInfo)
    {
        List<FuelInfo> fuelDetailsList = new List<FuelInfo>();
        fuelDetailsList = await _fuelDetailsService.FetchFuelInfoAccordingToStationAndFuel(fuelInfo.StationId, fuelInfo.Type);
     

       if (fuelDetailsList.Count() == 0)
        {
            await _fuelDetailsService.InsertFuelInfo(fuelInfo);
            return CreatedAtAction(nameof(Get), new { id = fuelInfo.FuelInfoId }, fuelInfo);
        }
        else
        {
            return NoContent();
        }
     

    }

    /* REST API url for updating fuel info */
    [HttpPut("{id}")]
    public async Task<IActionResult> EditFuelInfo(string id, [FromBody] FuelInfo fuelInfo)
    {
        await _fuelDetailsService.EditFuelInfo(id, fuelInfo);
        return NoContent();
    }

    /* REST API url for fetching a single fuel info document */
    [HttpGet("FetchOneFuelInfo/{id}")]
    public async Task<FuelInfo> FetchOneFuelInfo(string id)
    {
        return await _fuelDetailsService.FetchOneFuelInfo(id);
    }

    /* REST API url for fetching a fuel info document according to station ID */
    [HttpGet("FetchFuelInfoFromStation/{id}")]
    public async Task<List<FuelInfo>> FetchFuelInfoAccordingToStation(string id)
    {
        return await _fuelDetailsService.FetchFuelInfoAccordingToStation(id);
    }

    /* REST API url for fetching a fuel info document according to station ID and fuel ID */
    [HttpGet("FetchFuelInfoFromStationAndFuel")]
    public async Task<List<FuelInfo>> FetchFuelInfoAccordingToStationAndFuel(string sId, string fName)
    {
        return await _fuelDetailsService.FetchFuelInfoAccordingToStationAndFuel(sId, fName);
    }

    [HttpPatch("updateArrivalTime/{id}")]
    public async Task<JsonObject> UpdateArrivalTime(string id, [FromBody] FuelInfo fuelInfo)
    {

        await _fuelDetailsService.UpdateArrivalTime(id, fuelInfo);

        JsonObject keyValuePairs = new JsonObject();

        return keyValuePairs;
    }

}

