namespace ConfigFactory.CustomConfig
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