using Domain.Factory;
using Domain.Model;

namespace Application.DTO;

public class HolidayDTO
{
    public long Id { get; set; }
    public long _colabId{ get; set; }

    public IEnumerable<HolidayPeriodDTO> _holidayPeriods { get; set; }


    public IEnumerable<HolidayPeriodDTO> HolidayPeriods
    {
        get => _holidayPeriods;
        set => _holidayPeriods = value;
    }

    public HolidayDTO() {
    }

    public HolidayDTO(long colabId,long id)
    {
        Id = id;
        _colabId = colabId;
    }

    static public HolidayDTO ToDTO(Holiday holiday) {
        long idColab = holiday.GetColaborator();
        long id = holiday.Id;
        HolidayDTO holidayDTO = new HolidayDTO(idColab,id);

        return holidayDTO;
    }

    static public IEnumerable<HolidayDTO> ToDTO(IEnumerable<Holiday> holidays)
    {
        List<HolidayDTO> holidaysDTO = new List<HolidayDTO>();

        foreach( Holiday holiday in holidays ) {
            HolidayDTO holidayDTO = ToDTO(holiday);

            holidaysDTO.Add(holidayDTO);
        }

        return holidaysDTO;
    }

    static public Holiday ToDomain(HolidayDTO holidayDTO) 
    {
        if (holidayDTO == null) 
        {
            throw new ArgumentException("holidayDTO must not be null");
        }

        Holiday holiday = new Holiday(holidayDTO.Id,holidayDTO._colabId);
        
        return holiday;
    }

    static public Holiday UpdateToDomain(DateOnly startDate, DateOnly endDate,IHolidayPeriodFactory holidayPeriodFactory,Holiday holiday) {

		
        holiday.AddHolidayPeriod(holidayPeriodFactory,startDate,endDate);
		

        return holiday;

    }
}