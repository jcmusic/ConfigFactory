using ConfigFactory.DAL;
using ConfigFactory.DAL.Entities;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace ConfigFactory.CustomConfig
{
    public interface ICustomConfigProvider
    {
        Task<List<CustomConfigDto>> GetCustomConfigAsync(UserDto user, CustomConfigCategoryType? clientConfigCategoryType);
        Task<string?> GetCustomConfigValueOrNullByKey(UserDto user, string clientConfigKey);
    }

    public class CustomConfigProvider : ICustomConfigProvider
    {
        private readonly UsersContext _usersDbContext;

        public CustomConfigProvider(UsersContext usersDbContext)
        {
            _usersDbContext = usersDbContext;
        }

        public async Task<List<CustomConfigDto>> GetCustomConfigAsync(UserDto user, CustomConfigCategoryType? customConfigCategoryType)
        {
            var builder = PredicateBuilder.New<UserConfig>(u => u.UserId == user.UserId);
            {
                if (customConfigCategoryType != null)
                    builder = builder.And(x => x.ConfigCategoryId == (int)customConfigCategoryType);
            }

            var clientConfigList = await _usersDbContext.UserConfigs.AsNoTracking()
                .Where(builder)
                .ToListAsync();

            var clientConfigDtoList = clientConfigList.Select(x => new CustomConfigDto
            {
                Key = x.ConfigName,
                Value = x.ConfigValue
            }).ToList();

            return clientConfigDtoList;
        }

        public async Task<string?> GetCustomConfigValueOrNullByKey(UserDto user, string customConfigKey)
        {
            string? value = await _usersDbContext.UserConfigs.AsNoTracking()
                .Where(x => x.UserId == user.UserId)
                .Where(x => x.ConfigName.ToLower() == customConfigKey.ToLower())
                .Select(x => x.ConfigValue)
                .FirstOrDefaultAsync();

            return value;
        }
    }
}
