namespace PriceConsoleWithPostgreSQL.Models;

public class FullTruckLoadWithFeature
{
    public int Id { get; set; }
    public string OriginCountry { get; set; }
    public double TotalWeight { get; set; }
    public int TruckId { get; set; }
    public string? ContainerType { get; set; }
}