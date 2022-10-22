using System;
using Microsoft.AspNetCore.Mvc;
using MongoExample.Services;
using MongoExample.Models;


namespace MongoExample.Controllers;

[Controller]
[Route("api/fuel/[controller]")]
public class FuelDetailsController : Controller
{
    private readonly MongoDBService _mongoDBService;

    public FuelDetailsController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
    public async Task<List<FuelDetails>> Get()
    {
        return await _mongoDBService.GetFuelAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] FuelDetails fuelDetails)
    {
        List<FuelDetails> fuelDetailsList = new List<FuelDetails>();
        fuelDetailsList = await _mongoDBService.GetAsyncStationFuel(fuelDetails.StationId, fuelDetails.FuelName);
     

       if (fuelDetailsList.Count() == 0)
        {
            await _mongoDBService.CreateFuelAsync(fuelDetails);
            return CreatedAtAction(nameof(Get), new { id = fuelDetails.FdId }, fuelDetails);
        }
        else
        {
            return NoContent();
        }
     

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ChangeFuelDetails(string id, [FromBody] FuelDetails fuelDetails)
    {
        await _mongoDBService.ChangeFuelDetailsAsync(id, fuelDetails);
        return NoContent();
    }

    [HttpGet("GetOne/{id}")]
    public async Task<FuelDetails> GetOne(string id)
    {
        return await _mongoDBService.GetAsyncOneFuel(id);
    }

    //get fuel details according to stationId
    [HttpGet("GetOneStation/{id}")]
    public async Task<List<FuelDetails>> GetStationAll(string id)
    {
        return await _mongoDBService.GetAsyncStations(id);
    }

    //get fuel details according to station id and fuel id
    [HttpGet("GetStationFuelAll")]
    public async Task<List<FuelDetails>> GetStationFuel(string sId, string fName)
    {
        return await _mongoDBService.GetAsyncStationFuel(sId, fName);
    }

}

