using PriceConsoleWithPostgreSQL.DataAccessLayer;

namespace PriceConsoleWithPostgreSQL.DTOs.Create;

public class CreateLessThanFullTruckLoadDto
{
    public string TransportationType { get; set; }
    public string OriginCountry { get; set; }
    public string DestinationCountry { get; set; }
    public int TruckId { get; set; }
    public double TotalWeight { get; set; }
    public double Height { get; set; }
    public double Width { get; set; }
    public double Length { get; set; }
    public List<Driver> Drivers { get; set; }

}