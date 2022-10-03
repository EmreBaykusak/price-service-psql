namespace PriceConsoleWithPostgreSQL.DTOs.Get;

public class GetLessThanTruckLoadDto
{
    public string TransportationType { get; set; }
    public string OriginCountry { get; set; }
    public string DestinationCountry { get; set; }
    public double TotalWeight { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public double Length { get; set; }
    public double Distance { get; set; }
    public double PriceValue { get; set; }
}