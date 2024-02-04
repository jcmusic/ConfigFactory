namespace ConfigFactory.CustomConfig
{
    public class InventoryConfiguration
    {
        public bool SerialNumbersAreRequired { get; set; }
        public string? PO_Prefix { get; set; }
        public int? FrozenPeriodWeeks { get; set; }
        public ForecastCadenceType ForecastCadence { get; set; }
    }
}