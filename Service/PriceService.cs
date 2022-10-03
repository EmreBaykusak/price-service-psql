using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PriceConsoleWithPostgreSQL.DataAccessLayer;
using PriceConsoleWithPostgreSQL.DTOs.Create;
using PriceConsoleWithPostgreSQL.DTOs.Get;
using PriceConsoleWithPostgreSQL.DTOs.Update;
using PriceConsoleWithPostgreSQL.Mappers;

namespace PriceConsoleWithPostgreSQL.Service;

public class PriceService
{
    private readonly TruckLoadContext _context;
    private readonly IMapper _mapper;
    
    public PriceService(TruckLoadContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task AddFullTruckLoad(CreateFullTruckLoadDto fullTruckLoad)
    {
        
        var obj = _mapper.Map<FullTruckLoad>(fullTruckLoad);
        
        await _context.FullTruckLoads.AddAsync(obj);
        await _context.SaveChangesAsync();
    }
    public async Task AddLessThanTruckLoad(CreateLessThanFullTruckLoadDto lessThanTruckLoad)
    {
        var obj = _mapper.Map<LessThanTruckLoad>(lessThanTruckLoad);
        await _context.LessThanTruckLoads.AddAsync(obj);
        await _context.SaveChangesAsync();
    }

    public async Task<GetFullTruckLoadDto> GetFullTruckLoad(int id)
    {
        var fullTruckLoads = await _context.FullTruckLoads
            .AsNoTracking()
            .Where(x => x.Id == id)
            .ToListAsync();
        
        var obj = _mapper.Map<GetFullTruckLoadDto>(fullTruckLoads);
        return obj;
    }
    
    public async Task<GetLessThanTruckLoadDto> GetLessThanTruckLoad(int id)
    {
         var lessThanTruckLoads = await _context.LessThanTruckLoads
             .AsNoTracking()
             .Where(x => x.Id == id)
             .ToListAsync();

         var obj = _mapper.Map<GetLessThanTruckLoadDto>(lessThanTruckLoads);
         return obj;
    }
    
    public async Task<List<GetFullTruckLoadDto>> GetAllFullTruckLoad()
    {
        
        var fullTruckLoads = await _context.FullTruckLoads
            .AsNoTracking()
            .ToListAsync();
        var obj = _mapper.Map<List<GetFullTruckLoadDto>>(fullTruckLoads);
        return obj;
    }
    
    public async Task<List<GetLessThanTruckLoadDto>> GetAllLessThanTruckLoad()
    {
        var lessThanTruckLoads = await _context.LessThanTruckLoads
            .AsNoTracking()
            .ToListAsync();

        var obj = _mapper.Map<List<GetLessThanTruckLoadDto>>(lessThanTruckLoads);
        return obj;
    }

    public async Task UpdateFullTruckLoad(int id, UpdateFullTruckLoadDto fullTruckLoad)
    {
        var existingFullTruckLoad = await _context.FullTruckLoads.FindAsync(id);

        var obj = _mapper.Map<FullTruckLoad>(fullTruckLoad);
        if (existingFullTruckLoad is null)
        {
            throw new DbUpdateException();
        }

        existingFullTruckLoad.TransportationType = obj.TransportationType;
        existingFullTruckLoad.OriginCountry = obj.OriginCountry;
        existingFullTruckLoad.DestinationCountry = obj.DestinationCountry;
        existingFullTruckLoad.TruckId = obj.TruckId;
        existingFullTruckLoad.TotalWeight = obj.TotalWeight;
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e) when (!FullTruckLoadExists(id))
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task UpdateLessThanTruckLoad(int id, UpdateLessThanTruckLoadDto lessThanTruckLoad)
    {
        var existingLessThanTruckLoad = await _context.LessThanTruckLoads.FindAsync(id);
        var obj = _mapper.Map<UpdateLessThanTruckLoadDto>(lessThanTruckLoad);
      
        if (existingLessThanTruckLoad is null)
        {
            throw new DbUpdateException();
        }

        existingLessThanTruckLoad.TransportationType = obj.TransportationType;
        existingLessThanTruckLoad.OriginCountry = obj.OriginCountry;
        existingLessThanTruckLoad.DestinationCountry = obj.DestinationCountry;
        existingLessThanTruckLoad.TruckId = obj.TruckId;
        existingLessThanTruckLoad.TotalWeight = obj.TotalWeight;
        existingLessThanTruckLoad.Height = obj.Height;
        existingLessThanTruckLoad.Width = obj.Width;
        existingLessThanTruckLoad.Length = obj.Length;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e) when (!LessThanTruckLoadExists(id))
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public async Task DeleteFullTruckLoad(int id)
    {
        var fullTruckLoad = await _context.FullTruckLoads.FindAsync(id);

        if (fullTruckLoad is null)
        {
            throw new NullReferenceException();
        }

        _context.FullTruckLoads.Remove(fullTruckLoad);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteLessThanTruckLoad(int id)
    {
        var lessThanTruckLoad = await _context.LessThanTruckLoads.FindAsync(id);

        if (lessThanTruckLoad is null)
        {
            throw new NullReferenceException();
        }

        _context.LessThanTruckLoads.Remove(lessThanTruckLoad);
        await _context.SaveChangesAsync();
    }
    
    private bool FullTruckLoadExists(long id) {
        return _context.FullTruckLoads.Any(e => e.Id == id);
    }
    
    private bool LessThanTruckLoadExists(long id) {
        return _context.LessThanTruckLoads.Any(e => e.Id == id);
    }
    
    
    


}