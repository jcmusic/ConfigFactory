using ConfigFactory.DAL;
using ConfigFactory.Factories;
using ConfigFactory.Services;
using Microsoft.EntityFrameworkCore;
using Omnichain.Core.Providers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB Context
builder.Services.AddDbContext<UsersContext>(
    DbContextOptions => DbContextOptions.UseSqlite(
        "Data Source=Users.db",
        b => b.MigrationsAssembly("ConfigFactory")));

builder.Services.AddScoped<ICustomConfigProvider, CustomConfigProvider>();
builder.Services.AddScoped<IInventoryConfigFactory, InventoryConfigFactory>();
builder.Services.AddScoped<IInventoryService, InventoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
