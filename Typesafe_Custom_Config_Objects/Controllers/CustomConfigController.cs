using ConfigFactory.Factories;
using ConfigFactory.Models;
using ConfigFactory.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ConfigFactory.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomConfigController : ControllerBase
{
    private readonly ILogger<CustomConfigController> _logger;
    private readonly IInventoryConfigFactory _inventoryConfigFactory;

    public CustomConfigController(ILogger<CustomConfigController> logger, IInventoryConfigFactory inventoryConfigFactory)
    {
        _logger = logger;
        _inventoryConfigFactory = inventoryConfigFactory;
    }

    public IInventoryConfigFactory InventoryConfigFactory { get; }

    [HttpGet(Name = "GetCustomConfig")]
    public async Task<ActionResult> GetAsync()
    {
        // var httpUser = HttpContext.User;
        UserDto user = new UserDto
        {
            UserId = 1
        };

        var customConfig = await _inventoryConfigFactory.GetCustomConfigAsync(user);

        // Write out type & properties to the output window
        var type =  customConfig.GetType();
        Console.WriteLine($"Type: {type.Name}");
        Console.WriteLine($"Type: {type.BaseType?.Name ?? "None"}");
        foreach (var item in type.GetProperties())
        {
            Console.WriteLine($"property: {item.Name}   Value: {item.GetValue(customConfig)}");
        }
        //

        return Ok(JsonSerializer.Serialize(
            customConfig, new JsonSerializerOptions() { WriteIndented = true }));
    }
}
