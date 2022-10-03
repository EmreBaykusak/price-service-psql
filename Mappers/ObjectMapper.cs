using AutoMapper;
using PriceConsoleWithPostgreSQL.DataAccessLayer;
using PriceConsoleWithPostgreSQL.DTOs.Create;
using PriceConsoleWithPostgreSQL.DTOs.Get;
using PriceConsoleWithPostgreSQL.DTOs.Update;

namespace PriceConsoleWithPostgreSQL.Mappers;

internal class ObjectMapper
{
    private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg =>
            cfg.AddProfile<CustomMapper>());
        return config.CreateMapper();
    });

    public static IMapper Mapper => lazy.Value;
}

internal class CustomMapper : Profile
{
    public CustomMapper()
    {
        CreateMap<CreateFullTruckLoadDto, FullTruckLoad>().ReverseMap();
        CreateMap<CreateLessThanFullTruckLoadDto, LessThanTruckLoad>();
        CreateMap<FullTruckLoad, GetFullTruckLoadDto>();
        CreateMap<LessThanTruckLoad, GetLessThanTruckLoadDto >();
        CreateMap<UpdateFullTruckLoadDto, FullTruckLoad>();
        CreateMap<UpdateLessThanTruckLoadDto, LessThanTruckLoad>();
    }
}

