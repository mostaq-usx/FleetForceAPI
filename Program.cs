using FleetForceAPI.Models;
using FleetForceAPI.Repository;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add connection string
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));

// Register mongoDb configuration as a singleton object
builder.Services.AddSingleton<IMongoDatabase>(options =>{
    var settings = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();
    var client = new MongoClient(settings.ConnectionString);
    return client.GetDatabase(settings.DatabaseName);
});

// Register driver repository
builder.Services.AddSingleton<IDriverRepository, DriverRepository>();

// Register truck repository
builder.Services.AddSingleton<ITruckRepository, TruckRepository>();

// Register load repository
builder.Services.AddSingleton<ILoadRepository, LoadRepository>();

// Register autoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
