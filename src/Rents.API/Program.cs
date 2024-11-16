using Microsoft.EntityFrameworkCore;
using Rents.API.Features.Drivers;
using Rents.API.Features.Locations;
using Rents.API.Features.Vehicles;
using Rents.API.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<ILocationService, LocationService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RentsContext>(options => options.UseInMemoryDatabase("InMemoryDb"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/vehicles", async (RentsContext context) =>
{
    var vehicles = await context.Vehicles.ToListAsync();
    
    return Results.Ok(vehicles);
})
.WithName("Get Vehicles")
.WithOpenApi();

app.MapPost("/vehicles", async (string model, int year, string plate, IVehicleService service) =>
{
    await service.AddAsync(model, year, plate);

    return Results.Ok();
})
.WithName("Add Vehicles")
.WithOpenApi();

app.MapPost("/vehicles/{id}", async (Guid id, string plate, IVehicleService service) =>
{
    await service.UpdateAsync(id, plate);

    return Results.Ok(id);
})
.WithName("Update Vehicle Plate")
.WithOpenApi();

app.MapDelete("/vehicles/{id}", async (Guid id, IVehicleService service) =>
{
    await service.DeleteAsync(id);

    return Results.Ok(id);
})
.WithName("Delete Vehicle")
.WithOpenApi();

app.MapGet("/drivers", async (RentsContext context) =>
{
    var drivers = await context.Drivers.ToListAsync();

    return Results.Ok(drivers);
})
.WithName("Get Drivers")
.WithOpenApi();

app.MapPost("/drivers", async (string cnpj, string name, IDriverService service) =>
{
    await service.AddAsync(cnpj, name);

    return Results.Ok();
})
.WithName("Add Driver")
.WithOpenApi();

app.MapGet("/locations", async (RentsContext context) =>
{
    var locations = await context.Locations.ToListAsync();

    return Results.Ok(locations);
})
.WithName("Get Locations")
.WithOpenApi();

app.MapPost("/locations", async (Guid driverId, Guid vehicleId, int plan, ILocationService service) =>
{
    await service.AddAsync(driverId, vehicleId, plan);

    return Results.Ok();
})
.WithName("Add Location")
.WithOpenApi();

app.MapPost("/locations/{id}/finish", async (Guid id, DateOnly date, ILocationService service) =>
{
    await service.FinishAsync(id, date);
    
    return Results.Ok(id);
})
.WithName("Finish Location")
.WithOpenApi();

app.Run();
