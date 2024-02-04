using ConfigFactory.Configs;
using ConfigFactory.Enums;
using ConfigFactory.Models;
using Omnichain.Core.Providers;

namespace ConfigFactory.Factories
{
    public interface IInventoryConfigFactory
    {
        Task<InventoryConfiguration> GetCustomConfigAsync(UserDto user);
    }

    public class InventoryConfigFactory 
        : AbstractCustomConfigFactory<InventoryConfiguration>, IInventoryConfigFactory
    {
        public InventoryConfigFactory(ICustomConfigProvider customConfigService) 
            : base(customConfigService, CustomConfigCategoryType.Inventory)
        {
        }
    }
}