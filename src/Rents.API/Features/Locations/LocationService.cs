using Rents.API.Domain;
using Rents.API.Infrastructure;

namespace Rents.API.Features.Locations;

public interface ILocationService
{
    Task AddAsync(Guid driverId, Guid vehicleId, int plan);
    Task FinishAsync(Guid id, DateOnly date);
}

public class LocationService : ILocationService
{
    readonly RentsContext _context;

    public LocationService(RentsContext context)
        => _context = context;

    public async Task AddAsync(Guid driverId, Guid vehicleId, int plan)
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
        _context.Locations.Add(location);

        await _context.SaveChangesAsync();
    }

    public async Task FinishAsync(Guid id, DateOnly date)
    {
        var location = _context.Locations.First(x => x.Id == id);

        location.FinishDate = date;

        await _context.SaveChangesAsync();
    }
}