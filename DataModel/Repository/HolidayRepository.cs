namespace DataModel.Repository;

using Microsoft.EntityFrameworkCore;

using DataModel.Model;
using DataModel.Mapper;

using Domain.Model;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class HolidayRepository : GenericRepository<Holiday>, IHolidayRepository
{    
    HolidayMapper _holidayMapper;
    public HolidayRepository(AbsanteeContext context, HolidayMapper mapper) : base(context!)
    {
        _holidayMapper = mapper;
    }

    // public async Task<IEnumerable<Holiday>> GetHolidaysAsync()
    // {
    //     try {
    //         IEnumerable<HolidayDataModel> holidaysDataModel = await _context.Set<HolidayDataModel>()
    //                 .Include(c => c.holidayPeriods)
    //                 .ToListAsync();
    //
    //         IEnumerable<Holiday> holidays = _holidayMapper.ToDomain(holidaysDataModel);
    //
    //         return holidays;
    //     }
    //     catch
    //     {
    //         throw;
    //     }
    // }

    // public async Task<Holiday> GetHolidayByIdAsync(long id)
    // {
    //     try {
    //         HolidayDataModel holidayDataModel = await _context.Set<HolidayDataModel>()
    //                 .FirstAsync(c => c.Id==id);
    //
    //         Holiday holiday = _holidayMapper.ToDomain(holidayDataModel);
    //
    //         return holiday;
    //     }
    //     catch
    //     {
    //         throw;
    //     }
    // }

    public async Task<Holiday> AddHolidayPeriod(Holiday holiday, List<string> errorMessages)
    {
        try {
            HolidayDataModel holidayDataModel = await _context.Set<HolidayDataModel>()
                    .Include(c => c.colaboratorId)
                    .Include(c => c.holidayPeriods)
                    .FirstAsync(c => c.colaboratorId==holiday.GetColaborator());

            _holidayMapper.AddHolidayPeriod(holidayDataModel, holiday);

            _context.Entry(holidayDataModel).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return holiday;
        }
        catch (DbUpdateConcurrencyException)
        {
            // if (!await HolidayExists(holiday.Id))
            // {
            //     errorMessages.Add("Not found");
            //     
            //     return null;
            // }
            // else
            // {
            //     throw;
            // }

            return null;
        }
        catch
        {
            throw;
        }
    }


    public async Task<Holiday> AddHoliday(Holiday holiday)
    {
        try {
            HolidayDataModel holidayDataModel = _holidayMapper.ToDataModel(holiday);

            EntityEntry<HolidayDataModel> holidayDataModelEntityEntry = _context.Set<HolidayDataModel>().Add(holidayDataModel);
            
            await _context.SaveChangesAsync();

            HolidayDataModel holidayDataModelSaved = holidayDataModelEntityEntry.Entity;

            Holiday holidaySaved = _holidayMapper.ToDomain(holidayDataModelSaved);

            return holidaySaved;    
        }
        catch
        {
            throw;
        }
    }
    // public async Task<bool> HolidayExists(long id)
    // {
    //     return await _context.Set<HolidayDataModel>().AnyAsync(e => e.Id == id);
    // }
}