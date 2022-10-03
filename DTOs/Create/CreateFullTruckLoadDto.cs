using PriceConsoleWithPostgreSQL.DataAccessLayer;

namespace PriceConsoleWithPostgreSQL.DTOs.Create;

public class CreateFullTruckLoadDto
{
    public string TransportationType { get; set; }
    public string OriginCountry { get; set; }
    public int TruckId { get; set; }
    public string DestinationCountry { get; set; }
    public double TotalWeight { get; set; }
    
    public FullTruckLoadFeature FullTruckLoadFeature { get; set; }
    
    public List<Driver> Drivers { get; set; }
}