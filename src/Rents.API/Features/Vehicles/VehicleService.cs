
using Rents.API.Domain;
using Rents.API.Infrastructure;

namespace Rents.API.Features.Vehicles;

public interface IVehicleService
{
    Task AddAsync(string model, int year, string plate);
    Task UpdateAsync(Guid id, string plate);
    Task DeleteAsync(Guid id);
}

public class VehicleService : IVehicleService
{
    readonly RentsContext _context;

    public VehicleService(RentsContext context) 
        => _context = context;

    public async Task AddAsync(string model, int year, string plate)
    {
        var vehicle = new Vehicle
        {
            Id = Guid.NewGuid(),
            Model = model,
            Year = year,
            Plate = plate
        };
        
        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var vehicle = _context.Vehicles.First(x => x.Id == id);

        _context.Vehicles.Remove(vehicle);
    
        await _context.SaveChangesAsync();    
    }

    public async Task UpdateAsync(Guid id, string plate)
    {
        var vehicle = _context.Vehicles.First(x => x.Id == id);

        vehicle.Plate = plate;
    
        await _context.SaveChangesAsync();
    }
}