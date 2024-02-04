using ConfigFactory.Models;

namespace ConfigFactory.Services
{
    public interface IInventoryService
    {
        Task<IConfiguration> DoSomethingAsync(UserDto user);
    }
}