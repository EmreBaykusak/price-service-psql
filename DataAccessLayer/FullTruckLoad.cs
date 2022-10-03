using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace PriceConsoleWithPostgreSQL.DataAccessLayer;

public class FullTruckLoad : TruckLoad, IFtlPrice
{

    public FullTruckLoad()
    {
    }
    
    public FullTruckLoadFeature FullTruckLoadFeature { get; set; }
    public List<Driver> Drivers { get; set; }

    public bool IsDeleted { get; set; }

    public override double PriceValue
    {
        
        get
        {
            return FtlPrice();
        }
        
    }

    
    public double FtlPrice() 
    {
        var price = (this.Distance * this.PricePerKm);
        var TurkeyToGermany = base.OriginCountry == "tr" && base.DestinationCountry == "de";
        var TurkeyToItaly  = base.OriginCountry == "tr" && base.DestinationCountry == "it";
        var ItalyToTurkey = base.OriginCountry == "it" && base.DestinationCountry == "tr";
        var GermanyToTurkey = base.OriginCountry == "de" && base.DestinationCountry== "tr";
        var GermanyToItaly = base.OriginCountry == "de" && base.DestinationCountry == "it";
        var ItalyToGermany  = base.OriginCountry == "it" && base.DestinationCountry == "de";

        if  (((TurkeyToGermany) || (GermanyToTurkey)) && (price < 300))
        {
            return  300;
        }
    
        if  (((TurkeyToItaly) || (ItalyToTurkey)) && (price < 200))
        {
            return  200;
        }

        if  (((GermanyToItaly) || (ItalyToGermany)) && (price < 150))
        {
            return  150;
        }

        return price;  
    }

    
}