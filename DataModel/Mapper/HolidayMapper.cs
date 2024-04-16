namespace DataModel.Mapper;

using DataModel.Model;
using Domain.Model;
using Domain.Factory;
using System.Linq;
using System;

public class HolidayMapper
{
    private IHolidayFactory _holidayFactory;
    private HolidayPeriodMapper _holidayPeriodMapper;

    public HolidayMapper(
        IHolidayFactory holidayFactory,
        HolidayPeriodMapper holidayPeriodMapper)
    {
        _holidayFactory = holidayFactory;
        _holidayPeriodMapper = holidayPeriodMapper;
    }


    public Holiday ToDomain(HolidayDataModel holidayDM)
    {
        long id = holidayDM.Id;
        long colabId = holidayDM.colaboratorId;

        Holiday holidayDomain = _holidayFactory.NewHoliday(id,colabId);
        if(holidayDM.holidayPeriods!=null){
            foreach (var holidayPeriods in holidayDM.holidayPeriods)
            {
                IHolidayPeriodFactory _holidayPeriodFactory = new HolidayPeriodFactory();
                _holidayPeriodFactory.NewHolidayPeriod(holidayPeriods.StartDate, holidayPeriods.EndDate);
                holidayDomain.AddHolidayPeriod(_holidayPeriodFactory, holidayPeriods.StartDate, holidayPeriods.EndDate);
            }
        }
        return holidayDomain;
    }
 
    public IEnumerable<Holiday> ToDomain(IEnumerable<HolidayDataModel> holidaysDM)
    {
        List<Holiday> holidaysDomain = new List<Holiday>();

        foreach(HolidayDataModel holidayDataModel in holidaysDM)
        {
            Holiday holidayDomain = ToDomain(holidayDataModel);

            holidaysDomain.Add(holidayDomain);
        }

        return holidaysDomain.AsEnumerable();
    }

    

    public HolidayDataModel ToDataModel(Holiday holiday)
    {
        var holidayDataModel = new HolidayDataModel
        {
            Id = holiday.Id,
            colaboratorId = holiday.GetColaborator(),
            holidayPeriods = holiday.GetHolidayPeriods().Select(hp => _holidayPeriodMapper.ToDataModel(hp)).ToList()
        };

        return holidayDataModel;
    }

    public bool AddHolidayPeriod(HolidayDataModel holidayDataModel, Holiday holidayDomain)
    {
        List<HolidayPeriodDataModel> lista = new List<HolidayPeriodDataModel>();

        var holidayPeriods = holidayDomain.GetHolidayPeriods();

        foreach(var holidayPeriod in holidayPeriods){
            var holidayPeriodDataModel = _holidayPeriodMapper.ToDataModel(holidayPeriod);
            lista.Add(holidayPeriodDataModel);
        }

        holidayDataModel.holidayPeriods = lista;

        return true;
    }
}