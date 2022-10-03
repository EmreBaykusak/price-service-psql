namespace PriceConsoleWithPostgreSQL.DTOs.Get;

public class GetFullTruckLoadDto
{
    public int Id { get; set; }
    public string TransportationType { get; set; }
    public string OriginCountry { get; set; }
    public string DestinationCountry { get; set; }
    public double TotalWeight { get; set; }
    public double Distance { get; set; }
    public double PriceValue { get; set; }
}