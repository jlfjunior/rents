using Rents.API.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var vehicles = new List<Vehicle>();
var drivers = new List<Driver>();

app.MapGet("/vehicles", () =>
{
    return vehicles;
})
.WithName("Get Vehicles")
.WithOpenApi();

app.MapPost("/vehicles", (string model, int year, string plate) =>
{
    var vehicle = new Vehicle
    {
        Id = Guid.NewGuid(),
        Model = model,
        Year = year,
        Plate = plate
    };

    vehicles.Add(vehicle);

    return vehicle;
})
.WithName("Add Vehicles")
.WithOpenApi();

app.MapPost("/vehicles/{id}", (Guid id, string plate) =>
{
    var vehicle = vehicles.First(x => x.Id == id);

    vehicle.Plate = plate;
    
    return vehicle;
})
.WithName("Update Vehicle Plate")
.WithOpenApi();

app.MapDelete("/vehicles/{id}", (Guid id) =>
{
    var vehicle = vehicles.First(x => x.Id == id);

    vehicles.Remove(vehicle);
    
    return vehicle;
})
.WithName("Delete Vehicle")
.WithOpenApi();

app.MapGet("/drivers", () =>
{
    return drivers;
})
.WithName("Get Drivers")
.WithOpenApi();

app.MapPost("/drivers", (string cnpj, string name) =>
{
    var driver = new Driver
    {
        Id = Guid.NewGuid(),
        Cnpj = cnpj,
        Name = name
    };

    return driver;
})
.WithName("Add Driver")
.WithOpenApi();

app.Run();
