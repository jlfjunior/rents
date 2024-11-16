namespace Rents.API.Domain;

public class Vehicle
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public string Model { get; set; }
    public string Plate { get; set; }
}