using System.ComponentModel;
using System.Reflection;

namespace ConfigFactory.CustomConfig
{
    public abstract class AbstractCustomConfigFactory<TOutput>
    {
        #region Fields/Ctor

        private readonly ICustomConfigProvider _customConfigProvider;
        private readonly CustomConfigCategoryType _configCategoryType;
        protected AbstractCustomConfigFactory(ICustomConfigProvider customConfigProvider
            , CustomConfigCategoryType configCategoryType)
        {
            _customConfigProvider = customConfigProvider;
            _configCategoryType = configCategoryType;
        }

        #endregion

        #region Public Methods
        public async Task<TOutput> GetCustomConfigAsync(UserDto user)
        {
            var configValues = await _customConfigProvider.GetCustomConfigAsync(user, _configCategoryType) ?? new List<CustomConfigDto>();
            var concreteType = typeof(TOutput);
            var newConfig = Activator.CreateInstance<TOutput>();

            if (configValues.Count == 0)
                return newConfig;

            SetConfigValues(configValues, concreteType, newConfig);

            return await CustomConfiguration(user, configValues, newConfig);
        }

        #endregion

        #region Virtual Methods

        private protected virtual ValueTask<TOutput> CustomConfiguration(UserDto user,
            List<CustomConfigDto> configList, TOutput config) => ValueTask.FromResult(config);

        #endregion

        #region Private Methods
        private static void SetConfigValues(IEnumerable<CustomConfigDto> configValues, Type concreteType, TOutput newConfig)
        {
            foreach (var config in configValues)
            {
                var property = concreteType.GetProperty(config.Key);

                /*  Config key (Name) in the db table must match the property name in the config class (case-insensitive)
                 *  Behavior here is up to you to either ignore/log/throw when a config key db entry is miss-spelled.  */

                //if (property == null) continue;  

                IsValidPropertyOrThrow(property, config);

                object value = null;
                try
                {
                    var converter = TypeDescriptor.GetConverter(property.PropertyType);

                    value = converter.ConvertFromString(config.Value);
                }
                catch
                {
                    throw new Exception(
                        $"Unable to convert ClientConfig value={config.Value} to Type={property!.PropertyType}");
                }

                property.SetValue(newConfig, value, null);
            }
        }

        private static void IsValidPropertyOrThrow(PropertyInfo property, CustomConfigDto config)
        {
            if (property == null)
                throw new Exception($"{config.Key} is not a valid property on Type={typeof(TOutput).Name}");
        }

        #endregion
    }
}
