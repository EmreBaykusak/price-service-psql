using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace PriceConsoleWithPostgreSQL.DataAccessLayer;

public abstract class TruckLoad : IPrice
{
    private int id;
    private string transportationType;
    private string originCountry;
    private string destinationCountry;
    private int truckId;
    private double totalWeight;
    private double distance;
    private double pricePerKm;
    private double priceValue;
    
    public int Id
    {
        get
        {
            return id;
        }
        set
        {
            this.id = value;
        }
    }
    public string TransportationType
    {
        get { return transportationType; }
        set
        {
            if (value is "FTL" or "LTL")
            {
                this.transportationType = value;
            }
            else
            {
                throw new ArgumentException("Please enter a valid transportation type \"FTL\" or \"LTL\"", String.Empty);
            }
        }
    }
    public string OriginCountry
    {
        get { return originCountry; }
        set
        {
            if (value == "tr" || value == "de" || value == "it")
            {
                this.originCountry = value;
            }
            else if (value == "")
            {
                throw new ArgumentNullException(String.Empty, "Please enter the origin country");
                ;
            }
            else
            {
                throw new ArgumentException("Please enter a valid country  \"tr\", \"de\" or \"it\"", String.Empty);
            }

        }
    }
    public string DestinationCountry
    {
        get { return destinationCountry; }
        set
        {
            if (value == "tr" || value == "de" || value == "it")
            {
                this.destinationCountry = value;
            }
            else if (value == "")
            {
                throw new ArgumentNullException(String.Empty, "Please enter the origin country");
                ;
            }
            else
            {
                throw new ArgumentException("Please enter a valid country  \"tr\", \"de\" or \"it\"", String.Empty);
            }

        }
    }
    public int TruckId
    {
        get { return truckId; }
        set
        {
            switch (value)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    this.truckId = value;
                    break;
                default:
                    throw new ArgumentException("Please enter a valid truck type \"1\", \"2\", \"3\" or \"4\"");
            }
        }
    } 
    public double TotalWeight
    {
        get { return totalWeight; }
        set
        {
            if (value < 500)
            {
                throw new ArgumentOutOfRangeException(String.Empty, "Please enter above 500 kg");
            }
            else
            {
                if (this.truckId == 1)
                {
                    if (value <= 1000)
                    {
                        this.totalWeight = value;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException(String.Empty, "max weight limit is 1000 for this truck");
                    }
                }

                else if (this.truckId == 2)
                {
                    if (value <= 2000)
                    {
                        this.totalWeight = value;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException(String.Empty, "max weight limit is 2000 for this truck");
                    }
                }

                else if (this.truckId == 3)
                {
                    if (value <= 5000)
                    {
                        this.totalWeight = value;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException(String.Empty, "max weight limit is 5000 for this truck");
                    }
                }

                else if (this.truckId == 4)
                {
                    if (value <= 24000)
                    {
                        this.totalWeight = value;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException(String.Empty, "max weight limit is 24000 for this truck");
                    }
                }
                else
                {
                    throw new ArgumentException("Please enter a valid truck type \"1\", \"2\", \"3\" or \"4\"");
                }
            }
        }
    }
    public double Distance
    {
        get
        {
            switch (originCountry)
            {
                case "tr" when destinationCountry == "de":
                case "de" when destinationCountry == "tr":
                    return 2490;
                case "tr" when destinationCountry == "it":
                case "it" when destinationCountry == "tr":
                    return 1940;
                case "de" when destinationCountry == "it":
                case "it" when destinationCountry == "de":
                    return 1027;
                default:
                    return 0;
            }
        }
    }
    public double PricePerKm
    {
        get
        {
            return transportationType switch
            {
                "FTL" => GetPricePerKmAsFTL(),
                "LTL" => GetPricePerKmAsLTL(),
                _ => 0
            };
        }
    }

    public abstract double PriceValue { get;  }

    private double GetPricePerKmAsFTL()
    {
        switch (truckId)
        {
            case 1:
                return GetPricePerKmAsFTLAndTruckTypeOne();
            case 2:
                return GetPricePerKmAsFTLAndTruckTypeTwo();
            case 3:
                return GetPricePerKmAsFTlAndTruckTypeThree();
            case 4:
                return GetPricePerKmAsFTLAndTruckTypeFour();
            default:
                return 0;
        }
    }
    private double GetPricePerKmAsFTLAndTruckTypeOne()
    {
        switch (originCountry)
        {
            case "tr" when destinationCountry == "de":
            case "de" when destinationCountry == "tr":
                return 0.2;
            case "tr" when destinationCountry == "it":
            case "it" when destinationCountry == "tr":
                return 0.15;
            case "de" when destinationCountry == "it":
            case "it" when destinationCountry == "de":
                return 0.12;
        }

        return 0.0;
    }
    private double GetPricePerKmAsFTLAndTruckTypeTwo()
    {
        switch (originCountry)
        {
            case "tr" when destinationCountry == "de":
            case "de" when destinationCountry == "tr":
                return 0.19;
            case "tr" when destinationCountry == "it":
            case "it" when destinationCountry == "tr":
                return 0.14;
            case "de" when destinationCountry == "it":
            case "it" when destinationCountry == "de":
                return 0.11;
            default:
                return 0;
        }
    }
    private double GetPricePerKmAsFTlAndTruckTypeThree()
    {
        switch (originCountry)
        {
            case "tr" when destinationCountry == "de":
            case "de" when destinationCountry == "tr":
                return 0.15;
            case "tr" when destinationCountry == "it":
            case "it" when destinationCountry == "tr":
                return 0.12;
            case "de" when destinationCountry == "it":
            case "it" when destinationCountry == "de":
                return 0.10;
            default:
                return 0;
        }
    }
    private double GetPricePerKmAsFTLAndTruckTypeFour()
    {
        switch (originCountry)
        {
            case "tr" when destinationCountry == "de":
            case "de" when destinationCountry == "tr":
                return 0.12;
            case "tr" when destinationCountry == "it":
            case "it" when destinationCountry == "tr":
                return 0.10;
            case "de" when destinationCountry == "it":
            case "it" when destinationCountry == "de":
                return 0.08;
            default:
                return 0;
        }
    }
    private double GetPricePerKmAsLTL()
    {
        if ((originCountry == "tr" && destinationCountry == "de")
            || (originCountry == "de" && destinationCountry == "tr"))
        {
            if (totalWeight >= 500 && totalWeight <= 4000)
            {
                return 0.24;
            }

            if (totalWeight >= 4001 && totalWeight <= 12000)
            {
                return 0.18;
            }

            if (totalWeight >= 120001 && totalWeight <= 24000)
            {
                return 0.12;
            }

            return 0;
        }

        if ((originCountry == "tr" && destinationCountry == "it")
            || (originCountry == "it" && destinationCountry == "tr"))
        {
            if (totalWeight >= 500 && totalWeight <= 4000)
            {
                return 0.20;
            }

            if (totalWeight >= 4001 && totalWeight <= 12000)
            {
                return 0.16;
            }

            if (totalWeight >= 120001 && totalWeight <= 24000)
            {
                return 0.14;
            }

            return 0;
        }
        if ((originCountry == "de" && destinationCountry == "it")
            || (originCountry == "it" && destinationCountry == "de"))
        {
            if (totalWeight >= 500 && totalWeight <= 4000)
            {
                return 0.16;
            }

            if (totalWeight >= 4001 && totalWeight <= 12000)
            {
                return 0.14;
            }

            if (totalWeight >= 120001 && totalWeight <= 24000)
            {
                return 0.10;
            }
        }

        return 0;
        
        }
}