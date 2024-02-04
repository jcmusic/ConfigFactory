using ConfigFactory.Factories;
using ConfigFactory.Models;
using Microsoft.Extensions.Logging;

namespace ConfigFactory.Services;

public class InventoryService : IInventoryService
{
    private readonly ILogger<InventoryService> _logger;

    private readonly IInventoryConfigFactory _inventoryConfigFactory;

    public InventoryService(ILogger<InventoryService> logger, IInventoryConfigFactory inventoryConfigFactory)
    {
        _logger = logger;
        _inventoryConfigFactory = inventoryConfigFactory;
    }

    public async Task<IConfiguration> DoSomethingAsync(UserDto user)
    {
        _logger.Log(LogLevel.Information, "Logging from InventoryService.");
        Console.WriteLine("InventoryService method DoSomething() was called.");
        var config = await _inventoryConfigFactory.GetCustomConfigAsync(user);

        Console.WriteLine("InventoryService method DoSomething() was called.");

        //var type = config.GetType();
        //Console.WriteLine($"Type: {type.Name}");
        //foreach (var item in type.GetProperties())
        //{
        //    Console.WriteLine($"property: {item.Name}   Value: {item.GetValue(type).ToString()}");
        //}

        return config as IConfiguration;
    }
}