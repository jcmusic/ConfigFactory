using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConfigFactory.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ConfigName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ConfigValue = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ConfigCategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConfigs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UserConfigs",
                columns: new[] { "Id", "ConfigCategoryId", "ConfigName", "ConfigValue", "UserId" },
                values: new object[] { 1, 1, "PO_Prefix", "TR-", 1 });

            migrationBuilder.InsertData(
                table: "UserConfigs",
                columns: new[] { "Id", "ConfigCategoryId", "ConfigName", "ConfigValue", "UserId" },
                values: new object[] { 2, 1, "SerialNumbersAreRequired", "true", 1 });

            migrationBuilder.InsertData(
                table: "UserConfigs",
                columns: new[] { "Id", "ConfigCategoryId", "ConfigName", "ConfigValue", "UserId" },
                values: new object[] { 3, 1, "ForecastCadence", "1", 1 });

            migrationBuilder.InsertData(
                table: "UserConfigs",
                columns: new[] { "Id", "ConfigCategoryId", "ConfigName", "ConfigValue", "UserId" },
                values: new object[] { 4, 1, "FrozenPeriodWeeks", "2", 1 });

            migrationBuilder.InsertData(
                table: "UserConfigs",
                columns: new[] { "Id", "ConfigCategoryId", "ConfigName", "ConfigValue", "UserId" },
                values: new object[] { 5, 1, "PO_Prefix", "US", 2 });

            migrationBuilder.InsertData(
                table: "UserConfigs",
                columns: new[] { "Id", "ConfigCategoryId", "ConfigName", "ConfigValue", "UserId" },
                values: new object[] { 6, 1, "SerialNumbersAreRequired", "false", 2 });

            migrationBuilder.InsertData(
                table: "UserConfigs",
                columns: new[] { "Id", "ConfigCategoryId", "ConfigName", "ConfigValue", "UserId" },
                values: new object[] { 7, 1, "ForecastCadence", "2", 2 });

            migrationBuilder.InsertData(
                table: "UserConfigs",
                columns: new[] { "Id", "ConfigCategoryId", "ConfigName", "ConfigValue", "UserId" },
                values: new object[] { 8, 1, "FrozenPeriodWeeks", "1", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserConfigs");
        }
    }
}
