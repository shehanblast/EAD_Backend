//using System;
//using Microsoft.AspNetCore.Mvc;
//using MongoExample.Services;
//using MongoExample.Models;


//namespace MongoExample.Controllers;

//[Controller]
//[Route("api/fuelType/[controller]")]
//public class FuelTypeController : Controller
//{
//    private readonly MongoDBService _mongoDBService;

//    public FuelTypeController(MongoDBService mongoDBService)
//    {
//        _mongoDBService = mongoDBService;
//    }

//    [HttpGet]
//    public async Task<List<FuelType>> Get()
//    {
//        return await _mongoDBService.GetTypeAsync();
//    }

//    [HttpPost]
//    public async Task<IActionResult> Post([FromBody] FuelType fuelType)
//    {
//        await _mongoDBService.CreateTypeAsync(fuelType);
//        return CreatedAtAction(nameof(Get), new { id = fuelType.FuelId }, fuelType);
//    }
//}

