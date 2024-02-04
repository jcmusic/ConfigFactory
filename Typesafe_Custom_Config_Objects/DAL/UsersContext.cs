using ConfigFactory.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ConfigFactory.DAL
{
    public class UsersContext : DbContext
    {
        internal DbSet<UserConfig> UserConfigs { get; set; } = null!;

        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder = optionsBuilder
                .UseSqlite($"Data Source=Users.db",
                    options => { options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName); });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserConfig>()
                .HasData(
                    new UserConfig()
                    {
                        Id = 1,
                        UserId = 1,
                        ConfigName = "PO_Prefix",
                        ConfigValue = "TR-",
                        ConfigCategoryId = 1,
                    },
                    new UserConfig()
                    {
                        Id = 2,
                        UserId = 1,
                        ConfigName = "SerialNumbersAreRequired",
                        ConfigValue = "true",
                        ConfigCategoryId = 1,
                    },
                    new UserConfig()
                    {
                        Id = 3,
                        UserId = 1,
                        ConfigName = "ForecastCadence",
                        ConfigValue = "1",
                        ConfigCategoryId = 1,
                    },
                    new UserConfig()
                    {
                        Id = 4,
                        UserId = 1,
                        ConfigName = "FrozenPeriodWeeks",
                        ConfigValue = "2",
                        ConfigCategoryId = 1,
                    },
                    new UserConfig()
                    {
                        Id = 5,
                        UserId = 2,
                        ConfigName = "PO_Prefix",
                        ConfigValue = "US",
                        ConfigCategoryId = 1,
                    },
                    new UserConfig()
                    {
                        Id = 6,
                        UserId = 2,
                        ConfigName = "SerialNumbersAreRequired",
                        ConfigValue = "false",
                        ConfigCategoryId = 1,
                    },
                    new UserConfig()
                    {
                        Id = 7,
                        UserId = 2,
                        ConfigName = "ForecastCadence",
                        ConfigValue = "2",
                        ConfigCategoryId = 1,
                    },
                    new UserConfig()
                    {
                        Id = 8,
                        UserId = 2,
                        ConfigName = "FrozenPeriodWeeks",
                        ConfigValue = "1",
                        ConfigCategoryId = 1,
                    }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}