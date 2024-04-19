namespace Domain.Model;

using Domain.Factory;

public interface IHoliday
{
    public HolidayPeriod AddHolidayPeriod(IHolidayPeriodFactory hpFactory, DateOnly startDate, DateOnly endDate);

    public List<HolidayPeriod> GetHolidayPeriodsDuring(DateOnly startDate, DateOnly endDate);

    public List<HolidayPeriod> GetHolidayPeriods();
    
    public bool HasColaboratorAndHolidayPeriodsDuring(long colabId, DateOnly startDate, DateOnly endDate);

    public int GetHolidaysDaysWithMoreThanXDaysOff(int intDaysOff);

    public int GetNumberOfHolidayPeriodsDays();
    public int GetNumberOfHolidayPeriodsDays(DateOnly startDate, DateOnly endDate);

    public bool HasColaborador(long colabId);

}