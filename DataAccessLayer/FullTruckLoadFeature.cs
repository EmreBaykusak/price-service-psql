using System.ComponentModel.DataAnnotations.Schema;

namespace PriceConsoleWithPostgreSQL.DataAccessLayer;

public class FullTruckLoadFeature
{
    public int Id { get; set; }
    public int FullTruckLoad_Id { get; set; }
    public string ContainerType { get; set; }
    public FullTruckLoad FullTruckLoad { get; set; }
}