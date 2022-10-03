using System.Data;
using AutoMapper.QueryableExtensions;
using PriceConsoleWithPostgreSQL;
using PriceConsoleWithPostgreSQL.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Npgsql;
using PriceConsoleWithPostgreSQL.DTOs.Create;
using PriceConsoleWithPostgreSQL.DTOs.Update;
using PriceConsoleWithPostgreSQL.Mappers;
using PriceConsoleWithPostgreSQL.Service;

Initializer.Build();

var truckLoadContext = new TruckLoadContext();
var priceService = new PriceService(truckLoadContext, ObjectMapper.Mapper);

var fullTruckLoad = new CreateFullTruckLoadDto
{
     OriginCountry = "de", DestinationCountry = "tr", TruckId = 2,
     TotalWeight = 2000, TransportationType = "FTL"
};


var fullTruckLoad1 = new CreateFullTruckLoadDto
{
     OriginCountry = "de", DestinationCountry = "it", TruckId = 2,
     TotalWeight = 1500, TransportationType = "FTL"
};

var fullTruckLoad2 = new CreateFullTruckLoadDto
{
     OriginCountry = "it", DestinationCountry = "de", TruckId = 2,
     TotalWeight = 1500, TransportationType = "FTL"
};

var fullTruckLoad3 = new CreateFullTruckLoadDto
{
     OriginCountry = "it", DestinationCountry = "de", TruckId = 2,
     TotalWeight = 1750, TransportationType = "FTL",
     FullTruckLoadFeature = new FullTruckLoadFeature() { ContainerType = "LCL" }
};

var fullTruckLoad4 = new CreateFullTruckLoadDto
{
     OriginCountry = "tr", DestinationCountry = "de", TruckId = 2,
     TotalWeight = 1750, TransportationType = "FTL",
     FullTruckLoadFeature = new FullTruckLoadFeature { ContainerType = "LCL" }, Drivers = new List<Driver>
     {
         new() { Name = "Ahmet", Age = 27, Phone = "05391238213" }
     }
};

var fullTruckLoad5 = new UpdateFullTruckLoadDto()
{
    OriginCountry = "it", DestinationCountry = "de", TruckId = 2,
    TotalWeight = 1500, TransportationType = "FTL"
};

await priceService.AddFullTruckLoad(fullTruckLoad);
await priceService.AddFullTruckLoad(fullTruckLoad1);
await priceService.AddFullTruckLoad(fullTruckLoad2);
await priceService.AddFullTruckLoad(fullTruckLoad3);
await priceService.AddFullTruckLoad(fullTruckLoad4);
await priceService.UpdateFullTruckLoad(4, fullTruckLoad5);
await priceService.DeleteFullTruckLoad(295);

var lessThanTruckLoad = new CreateLessThanFullTruckLoadDto() {OriginCountry = "de", DestinationCountry = "tr", TransportationType = "LTL", TruckId = 2,
         TotalWeight = 1500, Height = 100, Width = 120,Length = 80};

var lessThanTruckLoad1 = new CreateLessThanFullTruckLoadDto() {OriginCountry = "it", DestinationCountry = "tr", TransportationType = "LTL", TruckId = 2,
     TotalWeight = 1500, Height = 100, Width = 115,Length = 85};

var lessThanTruckLoad2 = new CreateLessThanFullTruckLoadDto() {OriginCountry = "it", DestinationCountry = "tr", TransportationType = "LTL", TruckId = 2,
     TotalWeight = 1500, Height = 100, Width = 115,Length = 85};

var lessThanTruckLoad3 = new CreateLessThanFullTruckLoadDto() {OriginCountry = "it", DestinationCountry = "tr", TransportationType = "LTL", TruckId = 2,
     TotalWeight = 1500,  Height = 100, Width = 115,Length = 85, Drivers = new List<Driver>()
     {
         new () {Name = "Muhammed", Age = 25, Phone = "05335849282"}
     }};

var lessThanTruckLoad4 = new UpdateLessThanTruckLoadDto() {OriginCountry = "it", DestinationCountry = "tr", TransportationType = "LTL", TruckId = 2,
    TotalWeight = 1500, Height = 100, Width = 115,Length = 85};


await priceService.AddLessThanTruckLoad(lessThanTruckLoad);
await priceService.AddLessThanTruckLoad(lessThanTruckLoad1);        
await priceService.AddLessThanTruckLoad(lessThanTruckLoad2);
await priceService.AddLessThanTruckLoad(lessThanTruckLoad3);
await priceService.UpdateLessThanTruckLoad(2, lessThanTruckLoad4);
await priceService.DeleteLessThanTruckLoad(23);

string FormatPhone(string phone)
{
    return phone.Substring(1, phone.Length - 1);
}

var connection = new NpgsqlConnection(Initializer.Configuration.GetConnectionString("SqlCon"));

await using var _context = new TruckLoadContext(connection);
// Client vs Server Evaluation
// EF Core için
// Server => Veritabanına gönderilecek olan sql cümleciğidir. Local function barındırmaz. (Server local function'u tanımlayamaz.)
// Client => Makinenin memory'sinden gelen data üzerinden sorgulama yaptığı kısımdır. Local function barındırır.
_context.Drivers.Add(new Driver { Age = 25, Name = "Ahmet", Phone = "05389213932" });
_context.Drivers.Add(new Driver { Age = 25, Name = "Mehmet", Phone = "05359213932" });

// _context.SaveChanges();
    
var person = _context.Drivers
    .ToList()
    .Select(x => new { personName = x.Name, personPhone = FormatPhone(x.Phone) });

// 2'li Inner Join Query
var innerJoinResult = _context.FullTruckLoads
    .Join(_context.Drivers, x => x.Id, y => y.Id, (f, d) => d)
    .ToList();

var innerJoinResult2 = (from f in _context.FullTruckLoads
     join d in _context.Drivers on f.Id equals d.Id
     select d).ToList();
    
// Left Join Query
var leftJoinResult = (from f in _context.FullTruckLoads
    join ff in _context.FullTruckLoadFeatures on f.Id equals ff.Id into fflist
    from ff in fflist.DefaultIfEmpty()
    select new {f, ff}).ToList();
    
// Right Join Query
var rightJoinResult = (from ff in _context.FullTruckLoadFeatures
    join f in _context.FullTruckLoads on ff.Id equals f.Id into flist
    from f in flist.DefaultIfEmpty()
    select new {f, ff}).ToList();
    
// Outer Join Query
var left = await (from f in _context.FullTruckLoads
    join ff in _context.FullTruckLoadFeatures on f.Id equals ff.Id into fflist
    from ff in fflist.DefaultIfEmpty()
    select new
    {
        Id = (int?)f.Id,
        OriginCountry = f.OriginCountry,
        ContainerType = (string?)ff.ContainerType
    }).ToListAsync();
    
var right = await (from ff in _context.FullTruckLoadFeatures
    join f in _context.FullTruckLoads on ff.Id equals f.Id into flist
    from f in flist.DefaultIfEmpty()
    select new
    { 
        Id = (int?)f.Id,
        OriginCountry = (string?)f.OriginCountry,
        ContainerType = ff.ContainerType
    }).ToListAsync();
    
var outerJoinResult = left.Union(right);
    
// Raw Sql Queries
var fullTruckLoads = await _context.FullTruckLoads
    .FromSqlRaw("select * from \"FullTruckLoads\"")
    .ToListAsync();
    
var fullTruckLoadFirst = await _context.FullTruckLoads
    .FromSqlRaw("select * from \"FullTruckLoads\"")
    .FirstAsync();
    
const int id = 5;
    
var fullTruckLoads2 = await _context.FullTruckLoads
    .FromSqlRaw("select * from \"FullTruckLoads\" where \"Id\"={0}", id)
    .ToListAsync();
    
const int totalWeight = 1750;
    
var fullTruckLoads3 = await _context.FullTruckLoads
    .FromSqlInterpolated($"select * from \"FullTruckLoads\" where \"TotalWeight\"<{totalWeight}")
    .ToListAsync();

// Raw Sql Queries Mapped to Custom Models
var fullTruckLoadWithFeature = await _context.FullTruckLoadWithFeatures
    .FromSqlRaw(@"select f.""Id"",""OriginCountry"",""TruckId"",""ContainerType"",""TotalWeight""
                  from ""FullTruckLoads"" f 
                  inner join ""FullTruckLoadFeatures"" ff on f.""Id"" = ff.""FullTruckLoad_Id""")
    .ToListAsync();
    
var fullTruckLoadWithEssential = await _context.FullTruckLoadsEssentials
    .FromSqlRaw("select \"OriginCountry\",\"DestinationCountry\" " +
                "from \"FullTruckLoads\"")
    .ToListAsync();
    
// ToSqlQuery Method

var fullTruckLoadEssential = await _context.FullTruckLoadsEssentials.ToListAsync();
    
// ToView Method

var fullTruckView = await _context.FullTruckViews.Where(x => x.DriverAge == 27).ToListAsync();

// Pagination 
    
static List<FullTruckLoad> GetFullTruckLoads(int page, int pageSize)
{
    using var _context = new TruckLoadContext();
    return _context.FullTruckLoads
        .OrderByDescending(load => load.Id)
        .Skip((page - 1) * pageSize)
        .Take(3)
        .ToList();
}

GetFullTruckLoads(5,3).ForEach(x =>
{
    Console.WriteLine($"{x.Id}, {x.OriginCountry}, {x.DestinationCountry}, {x.Drivers}");
});

// Global Query Filters

var ftls = _context.FullTruckLoads.ToList();

await using var _context2 = new TruckLoadContext(1750);
var fullTrucksEqualOrLowerThan1750 = _context2.FullTruckLoads.ToList();
var fullTrucksNotDeleted = _context.FullTruckLoads.ToList();

// Query Tags

var fullTrucksWithFeature = _context.FullTruckLoads
    .TagWith("Truckloads with containers")
    .Include(x => x.FullTruckLoadFeature)
    .Where(x=> x.PriceValue==0)
    .ToList();
    
// Tracking/No Tracking

var truckLoad = _context.FullTruckLoads.First(x => x.Id == 10);
truckLoad.OriginCountry = "tr";
_context.Entry(truckLoad).State = EntityState.Modified;
_context.Update(truckLoad);
_context.SaveChanges();
    
// Store Procedure/Function

// Stored Function for Normal Model

var fullTruckLoadsSf = await _context.FullTruckLoads
    .FromSqlRaw("select * from fx()")
    .ToListAsync();

// Stored Function for Custom Model

var fullTruckLoadsWithFeaturesSf = await _context.FullTruckLoadWithFeatures
    .FromSqlRaw("select * from gx()")
    .ToListAsync();
    
// Stored Function Custom Model with Parameters

const double totalWeight2 = 2000;

var fullTruckLoadsWithFeaturesTotalWeightSf = await _context.FullTruckLoadWithFeatures
    .FromSqlInterpolated($@"select * from sf_get_fullTruckLoadWithFeaturesTotalWeight({totalWeight2})")
    .ToListAsync();

// Stored Function Call From Method Declared In Context

var fullTruckLoadWithFeatureUsingMethodDeclaration = await _context
    .GetFullTruckLoadFeaturesWithParameter(1950)
    .ToListAsync();

// Projections

// Entity

var fullTruckLoadsToList = await _context.FullTruckLoads
    .ToListAsync();
    
// Anonymous

var fullTruckLoadsAnonymousModel = await _context.FullTruckLoads
    .Include(load => load.FullTruckLoadFeature)
    .Select(x => new
{
    TruckLoadType = x.TransportationType,
    TruckLoadPrice = x.PriceValue,
    TruckLoadContainerType = x.FullTruckLoadFeature
})
    .Where(x=> x.TruckLoadPrice > 0)
    .ToListAsync();

// Anonymous Without Include

var fullTruckLoadsAnonymousModelWithoutInclude = await _context.FullTruckLoads
    .Select(x => new
    {
        TruckLoadType = x.TransportationType,
        TruckLoadPrice = x.PriceValue,
        TruckLoadContainerType = x.FullTruckLoadFeature
    })
    .Where(x=> x.TruckLoadPrice > 0)
    .ToListAsync();
    
// DTO/View Model Without AutoMapper

var fullTruckLoadsDtoModelWithoutAutoMapper = await _context.FullTruckLoads
    .Select(x => new CreateFullTruckLoadDto()
    {
        TransportationType = x.TransportationType,
        OriginCountry = x.OriginCountry,
        DestinationCountry = x.DestinationCountry,
        TotalWeight = x.TotalWeight

    }).ToListAsync();
    
// DTO/View Model With AutoMapper
    
var fullTruckLoadsDtoModelWithAutoMapper = ObjectMapper.Mapper.Map<List<CreateFullTruckLoadDto>>(fullTruckLoadsDtoModelWithoutAutoMapper);
    
// DTO/View Model With AutoMapper ProjectTo Method

var fullTruckLoadsDtoModelWithAutoMapperProjectTo = await _context.FullTruckLoads
    .ProjectTo<CreateFullTruckLoadDto>(ObjectMapper.Mapper.ConfigurationProvider)
    .ToListAsync();
    
// Transactions

// Transactions with Multiple DbContextInstance

await using var transaction = _context.Database.BeginTransaction();
{
    await using (var dbContext2 = new TruckLoadContext(connection))
    {
        dbContext2.Database
            .UseTransaction(transaction.GetDbTransaction());
        dbContext2.FullTruckLoads
            .OrderBy(x => x.Id)
            .LastOrDefault().TruckId = 1;
        dbContext2.SaveChanges();
    }
    var fullTruckLoadObject3 = new FullTruckLoad
    {
        TransportationType = "FTL",
        OriginCountry = "tr",
        DestinationCountry = "de",
        TruckId = 2,
        TotalWeight = 2000,
        Drivers = new List<Driver>
        {
            new () 
                { 
                    Age = 25, 
                    Name = "Ahmet", 
                    Phone = "05324821383"
                }
        },
        FullTruckLoadFeature =  new FullTruckLoadFeature
        {
            ContainerType = "LCL",
        }
    };
    
    _context.FullTruckLoads.Add(fullTruckLoadObject3);
    _context.SaveChanges();

    var fullTruckLoadFirst2 = _context.FullTruckLoads.First();
    
    fullTruckLoadFirst2.DestinationCountry = "it";
    _context.SaveChanges();
    
    transaction.Commit();
}

// Isolation Levels - Update From Client - Read From Server - Read Uncommitted/Committed

await using var transaction2 = _context.Database.BeginTransaction();
{
    var fullTruckLoadsFirst3 = _context.FullTruckLoads.First();

    fullTruckLoadsFirst3.TotalWeight = 2000;
    _context.SaveChanges();
    
    transaction2.Commit();
}

// Isolation Levels - Read From Client - Update From Server - Repeatable Read - Snapshot - Serializable

await using var transaction3 = _context.Database.BeginTransaction(IsolationLevel.Serializable);
var fullTruckLoadsToList2 = _context.FullTruckLoads.ToList();
    
transaction3.Commit();

Console.WriteLine("");