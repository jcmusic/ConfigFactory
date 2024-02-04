using ConfigFactory.Enums;
using ConfigFactory.Models;
using Omnichain.Core.Providers;
using System.ComponentModel;
using System.Reflection;

namespace ConfigFactory.Factories
{
    public abstract class AbstractCustomConfigFactory<T>
    {
        private readonly ICustomConfigProvider _customConfigProvider;
        private readonly CustomConfigCategoryType _configCategoryType;
        protected AbstractCustomConfigFactory(ICustomConfigProvider customConfigProvider
            , CustomConfigCategoryType configCategoryType)
        {
            _customConfigProvider = customConfigProvider;
            _configCategoryType = configCategoryType;
        }

        #region Public Methods
        public async Task<T> GetCustomConfigAsync(UserDto user)
        {
            var configValues = await _customConfigProvider.GetCustomConfigAsync(user, _configCategoryType) ?? new List<CustomConfigDto>();
            var type = typeof(T);
            var newConfig = Activator.CreateInstance<T>();

            if (configValues.Count == 0)
                return newConfig;

            SetConfigValues(configValues, type, newConfig);

            return await CustomConfiguration(user, configValues, newConfig);
        }
        #endregion
        /// <summary>
        /// Override this method to implement custom config values. e.g; fetching a value from a DB table. 
        /// </summary>
        #region Virtual Methods
        private protected virtual ValueTask<T> CustomConfiguration(UserDto user, 
            List<CustomConfigDto> configList, T config) => ValueTask.FromResult(config);
        #endregion

        #region Private Methods
        private static void SetConfigValues(IEnumerable<CustomConfigDto> configValues, Type type, T newConfig)
        {
            foreach (var config in configValues)
            {
                var property = type.GetProperty(config.Key);
                if(property == null) continue;;

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
                throw new Exception($"{config.Key} is not a valid property on Type={typeof(T).Name}");
        }

        #endregion
    }
}
