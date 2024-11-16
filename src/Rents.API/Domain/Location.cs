namespace Rents.API.Domain;

public class Location
{
    public Guid Id { get; set; }
    public Plan Plan { get; set; }
    public Guid DriverId { get; set; }
    public Guid VehicleId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly ExpectedDeliveryDate { get; set; }
    public DateOnly FinishDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal PenaltyAmount { get; set; }
    public decimal Amount { get; set; }
}

public enum Plan
{
    SevenDays = 7,
    FifteenDays = 15,
    ThirtyDays = 30,
    FortyFiveDays = 45,
    FiftyDays = 50
}