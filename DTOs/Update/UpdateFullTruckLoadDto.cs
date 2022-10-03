namespace PriceConsoleWithPostgreSQL.DTOs.Update;

public class UpdateFullTruckLoadDto
{
    public string TransportationType { get; set; }
    public string OriginCountry { get; set; }
    public int TruckId { get; set; }
    public string DestinationCountry { get; set; }
    public double TotalWeight { get; set; }
}