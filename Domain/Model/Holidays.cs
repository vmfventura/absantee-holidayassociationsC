using Domain.Factory;

namespace Domain.Model;

public class Holidays : IHolidays
{
    public long Id { get; set; }
    public IHolidayFactory _holidayFactory;
    public List<IHoliday> _holidayList = new List<IHoliday>();

    protected Holidays()
    {
    }

    public Holidays(IHolidayFactory hFactory)
    {
        if (hFactory is not null)
        {
            _holidayFactory = hFactory;
        }
        else
        {
            throw new ArgumentException("Holiday Factory cannot be null");
        }
    }

    public IHoliday addHoliday(long holidayId, long colaboratorId)
    {
        IHoliday holiday = _holidayFactory.NewHoliday(holidayId, colaboratorId);
        _holidayList.Add(holiday);
        return holiday;
    }

    public List<IHoliday> getListHolidayFilterByColaborator(long colaboratorId, DateOnly startDate, DateOnly endDate)
    {
        IEnumerable<IHoliday> holidayList =
            _holidayList.Where(h => h.HasColaboratorAndHolidayPeriodsDuring(colaboratorId, startDate, endDate));

        if (!holidayList.Any())
        {
            throw new ArgumentException("No holiday found for this colaborator");
        }

        return holidayList.ToList();
    }

    public int GetNumberOfHolidaysDaysForColaboratorDuringPeriod(long colaboratorId, DateOnly startDate,
        DateOnly endDate)
    {
        List<IHoliday> holidayList = getListHolidayFilterByColaborator(colaboratorId, startDate, endDate);
        int totalDaysOff = holidayList
            .Sum(holiday => holiday.GetNumberOfHolidayPeriodsDays());

        return totalDaysOff;
    }

    public int GetNumberOfDaysByColaborator(long colaboratorId)
    {
        return _holidayList.Where(h => h.HasColaborador(colaboratorId))
            .Sum(h => h.GetNumberOfHolidayPeriodsDays());
    }


    // public List<IHolidays> getListHolidayFilterByColaborator(colaborator, startDate, endDate) {

    //     return _holidayList.Where(h => h.getListHolidayFilterByColaborator(colaborator, startDate, endDate).Any()).ToList();
    // }
}