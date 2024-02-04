namespace ConfigFactory.Configs
{
    public interface IConfiguration
    {
    }

    public class InventoryConfiguration : IConfiguration
    {
        public bool SerialNumbersAreRequired { get; set; }
        public string? PO_Prefix { get; set; }
        public int? FrozenPeriodWeeks { get; set; }
        public int? ForecastCadence { get; set; }
    }
}