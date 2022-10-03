using Microsoft.EntityFrameworkCore;

namespace PriceConsoleWithPostgreSQL.DataAccessLayer;

public class Driver
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Phone { get; set; }
    public List<FullTruckLoad> FullTruckLoads { get; set; }
    public List<LessThanTruckLoad> LessThanTruckLoads { get; set; }
}