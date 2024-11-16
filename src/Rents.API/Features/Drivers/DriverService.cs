using Rents.API.Infrastructure;

namespace Rents.API.Features.Drivers;

public interface IDriverService
{
    Task AddAsync(string cnpj, string name);
}

public class DriverService : IDriverService
{
    readonly RentsContext _context;

    public DriverService(RentsContext context) 
        => _context = context; 
    
    public async Task AddAsync(string cnpj, string name)
    {
        var driver = new Driver
        {
            Id = Guid.NewGuid(),
            Cnpj = cnpj,
            Name = name
        };
        _context.Drivers.Add(driver);

        await _context.SaveChangesAsync();
    }
}