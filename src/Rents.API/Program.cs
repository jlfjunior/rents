using Microsoft.EntityFrameworkCore;
using Rents.API.Domain;
using Rents.API.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
    return await context.Vehicles.ToListAsync();
})
.WithName("Get Vehicles")
.WithOpenApi();

app.MapPost("/vehicles", async (string model, int year, string plate, RentsContext context) =>
{
    var vehicle = new Vehicle
    {
        Id = Guid.NewGuid(),
        Model = model,
        Year = year,
        Plate = plate
    };

    context.Vehicles.Add(vehicle);
    await context.SaveChangesAsync();

    return vehicle;
})
.WithName("Add Vehicles")
.WithOpenApi();

app.MapPost("/vehicles/{id}", async (Guid id, string plate, RentsContext context) =>
{
    var vehicle = context.Vehicles.First(x => x.Id == id);

    vehicle.Plate = plate;
    
    await context.SaveChangesAsync();

    return vehicle;
})
.WithName("Update Vehicle Plate")
.WithOpenApi();

app.MapDelete("/vehicles/{id}", async (Guid id, RentsContext context) =>
{
    var vehicle = context.Vehicles.First(x => x.Id == id);

    context.Vehicles.Remove(vehicle);
    
    await context.SaveChangesAsync();

    return vehicle;
})
.WithName("Delete Vehicle")
.WithOpenApi();

app.MapGet("/drivers", async (RentsContext context) =>
{
    return await context.Drivers.ToListAsync();
})
.WithName("Get Drivers")
.WithOpenApi();

app.MapPost("/drivers", async (string cnpj, string name, RentsContext context) =>
{
    var driver = new Driver
    {
        Id = Guid.NewGuid(),
        Cnpj = cnpj,
        Name = name
    };

    context.Drivers.Add(driver);

    await context.SaveChangesAsync();

    return driver;
})
.WithName("Add Driver")
.WithOpenApi();

app.MapGet("/locations", async (RentsContext context) =>
{
    return await context.Locations.ToListAsync();
})
.WithName("Get Locations")
.WithOpenApi();

app.MapPost("/locations", async (Guid driverId, Guid vehicleId, int plan, RentsContext context) =>
{
    var location = new Location
    {
        Id = Guid.NewGuid(),
        Plan = (Plan)plan,
        DriverId = driverId,
        VehicleId = vehicleId,
        CreatedAt = DateTime.Now,
        StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
        ExpectedDeliveryDate = DateOnly.FromDateTime(DateTime.Now).AddDays(1 + plan)
    };

    context.Locations.Add(location);

    await context.SaveChangesAsync();
    
    return location;
})
.WithName("Add Location")
.WithOpenApi();

app.MapPost("/locations/{id}/finish", async (Guid id, DateOnly date, RentsContext context) =>
{
    var location = context.Locations.First(x => x.Id == id);

    location.FinishDate = date;

    await context.SaveChangesAsync();

    return location;
})
.WithName("Finish Location")
.WithOpenApi();

app.Run();
