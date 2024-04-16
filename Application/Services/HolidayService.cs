namespace Application.Services;

using Domain.Model;
using Application.DTO;

using Microsoft.EntityFrameworkCore;
using DataModel.Repository;
using Domain.IRepository;
using Domain.Factory;
using DataModel.Model;
using Gateway;

public class HolidayService {

    private readonly AbsanteeContext _context;
    private readonly IHolidayRepository _holidayRepository;
    
    private readonly IHolidayPeriodFactory _holidayPeriodFactory;
    private readonly HolidayGateway _holidayAmqpGateway;


    
    public HolidayService(IHolidayRepository holidayRepository, IHolidayPeriodFactory holidayPeriodFactory, HolidayGateway holidayAmqpGateway) {
        _holidayRepository = holidayRepository;
        _holidayPeriodFactory = holidayPeriodFactory;
        _holidayAmqpGateway=holidayAmqpGateway;

    }

    // public async Task<IEnumerable<HolidayDTO>> GetAll()
    // {    
    //     IEnumerable<Holiday> holidays = await _holidayRepository.GetHolidaysAsync();
    //
    //     IEnumerable<HolidayDTO> holidaysDTO = HolidayDTO.ToDTO(holidays);
    //
    //     return holidaysDTO;
    // }

    // public async Task<HolidayDTO> GetHolidayById(long id)
    // {    
    //     Holiday holiday = await _holidayRepository.GetHolidayByIdAsync(id);
    //
    //     HolidayDTO holidayDTO = HolidayDTO.ToDTO(holiday);
    //
    //     return holidayDTO;
    // }

    public async Task<HolidayDTO> Add(HolidayDTO holidayDto, List<string> errorMessages)
    {
        Holiday holiday = HolidayDTO.ToDomain(holidayDto);

        holiday = await _holidayRepository.AddHoliday(holiday);

        HolidayDTO holidayDTO = HolidayDTO.ToDTO(holiday);

        return holidayDTO;
    }

}