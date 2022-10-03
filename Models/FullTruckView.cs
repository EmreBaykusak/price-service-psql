namespace PriceConsoleWithPostgreSQL.Models;

public class FullTruckView
{
    public int Id { get; set; }
    public int DriversId { get; set; }
    public string DriverName { get; set; }
    public double TotalWeight { get; set; }
    public int DriverAge { get; set; }
    
}