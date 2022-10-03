using System.ComponentModel.DataAnnotations.Schema;

namespace PriceConsoleWithPostgreSQL.DataAccessLayer;

public class LessThanTruckLoad : TruckLoad, ILtlPrice
{
    public List<Driver> Drivers { get; set; }
    private double width; 
    private double height; 
    private double length;
    public double Width 
    { 
        get
        {
            return width;
        } 
        set
        {
            this.width = value;
        } 
    }

    public double Height 
    { 
        get
        {
            return height;
        } 
        set
        {
            this.height = value;
        } 
    }

    public double Length 
    {
        get
        {
            return length;
        } 
        set
        {
            this.length = value;
        } 
    }
    
    public override double PriceValue
    {
        get => LtlPrice();
       
    }

    public double LtlPrice()
    {
        width /= 100;
        height /= 100;
        length /= 100;

        var cubicMeter = width * height * length * 333;
        var weightCalculation = base.TotalWeight * base.PricePerKm;

        return cubicMeter < weightCalculation ? weightCalculation : cubicMeter;
    }
}