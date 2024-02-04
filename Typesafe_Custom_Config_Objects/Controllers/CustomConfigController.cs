using ConfigFactory.CustomConfig;
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
        // mocking user
        UserDto user = new UserDto
        {
            UserId = 1
        };

        /***** Set breakpoint here to examine this type-safe config object!  Note that you can even use enums! ****/

        /***   Instead of fetching rows, name-matching, null-checking, which is ugly, verbose, 
        *      and error-prone, use this typesafe object to get config values for your service/module
        *      
        *  This controller is requesting an IInventoryConfigFactory, which you would use in (inject into) a Inventory Service or BLL class 
        *      The values are coming from the database (UserConfig table) where the property name and value are stored as strings (key/value pairs)
        *      You can then create/instantiate the config onject by calling GetCustomConfigAsync() and passing the userId. 
        *      
        *      Use this class in Linq statements or to make decision branching in your code much cleaner, safer, & more readable.
        *      
        *      Change UserId to clientId or customerId for your own use case.
        *      
        *      The InventoryConfigFactory is just a sample.
        *      Create your own ConfigFactory for each services/BLL class in which you need it by deriving from the AbstractCustomConfigFactory and 
        *      a commensurate configuration model to populate (propertys should match the key name from you Key/value pairs.
        *      
        *      Enjoy!
        * ***/
        var customConfig = await _inventoryConfigFactory.GetCustomConfigAsync(user);

        /***** Write out type & properties to the output window ****/
        var type =  customConfig.GetType();
        Console.WriteLine($"Type: {type.Name}");
        Console.WriteLine($"Type: {type.BaseType?.Name ?? "None"}");
        foreach (var item in type.GetProperties())
        {
            Console.WriteLine($"property: {item.Name}   Value: {item.GetValue(customConfig)}");
        }
        /*****  ****/

        return Ok(JsonSerializer.Serialize(
            customConfig, new JsonSerializerOptions() { WriteIndented = true }));
    }
}
