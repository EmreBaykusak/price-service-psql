using System.Data.Common;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PriceConsoleWithPostgreSQL.Models;

namespace PriceConsoleWithPostgreSQL.DataAccessLayer;

public class TruckLoadContext : DbContext
{
    private readonly double TotalWeight;
    private readonly DbConnection connection;
    public TruckLoadContext()
    {
    }
    public TruckLoadContext(double totalWeight)
    {
        TotalWeight = totalWeight;
    }

    public TruckLoadContext(DbConnection connection)
    {
        this.connection = connection;
    }

    public DbSet<FullTruckLoad> FullTruckLoads { get; set; }
    public DbSet<LessThanTruckLoad> LessThanTruckLoads { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<FullTruckLoadFeature> FullTruckLoadFeatures { get; set; }
    public DbSet<FullTruckLoadWithFeature> FullTruckLoadWithFeatures { get; set; }
    public DbSet<FullTruckLoadsEssential> FullTruckLoadsEssentials { get; set; }
    public DbSet<FullTruckView> FullTruckViews { get; set; }

    public IQueryable<FullTruckLoadWithFeature> GetFullTruckLoadFeaturesWithParameter(int totalWeight) =>
        FromExpression(() => GetFullTruckLoadFeaturesWithParameter(totalWeight));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<FullTruckLoad>()
            .Property(x => x.Distance)
            .HasDefaultValueSql(@"alter table ""FullTruckLoads""
        alter column ""Distance"" set default get_ftl_distance();")
            .ValueGeneratedOnAddOrUpdate();

        modelBuilder
            .Entity<FullTruckLoad>()
            .Property(x => x.PricePerKm)
            .HasDefaultValueSql(@"alter table ""FullTruckLoads""
        alter column ""PricePerKm"" set default get_ftl_priceperkm();")
            .ValueGeneratedOnAddOrUpdate();
        
        modelBuilder
            .Entity<FullTruckLoad>()
            .Property(x => x.PriceValue)
            .HasDefaultValueSql(@"alter table ""FullTruckLoads""
        alter column ""PriceValue"" set default get_ftl_pricevalue();")
            .ValueGeneratedOnAddOrUpdate();
        
        modelBuilder
            .Entity<FullTruckLoad>()
            .HasOne(x => x.FullTruckLoadFeature)
            .WithOne(x => x.FullTruckLoad)
            .HasForeignKey<FullTruckLoadFeature>(x => x.FullTruckLoad_Id)
            .HasConstraintName("FK_FullTruckLoads_FullTruckLoadFeatures_FullTruckLoad_Id")
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder
            .Entity<FullTruckLoadsEssential>()
            .HasNoKey()
            .ToSqlQuery("select \"OriginCountry\", \"DestinationCountry\" from \"FullTruckLoads\"");

        modelBuilder
            .Entity<FullTruckView>()
            .HasNoKey()
            .ToView("FullTruckView");

        modelBuilder
            .Entity<FullTruckLoad>()
            .Property(x => x.IsDeleted)
            .HasDefaultValue(false);

        modelBuilder
            .HasDbFunction(typeof(TruckLoadContext)
                .GetMethod(nameof(GetFullTruckLoadFeaturesWithParameter), new[] { typeof(int) })!)
            .HasName("sf_get_fulltruckloadwithfeaturestotalweight");

        if (TotalWeight != default(int))
        {
            modelBuilder
                .Entity<FullTruckLoad>()
                .HasQueryFilter(x => !x.IsDeleted && x.TotalWeight <= TotalWeight);
        }
        else
        {
            modelBuilder
                .Entity<FullTruckLoad>()
                .HasQueryFilter(x => !x.IsDeleted);
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (connection == default(DbConnection))
        {
            Initializer.Build();
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information).UseNpgsql(Initializer.Configuration.GetConnectionString("SqlCon"));  
        }
        else
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information).UseNpgsql(connection);
        }
    }
}
