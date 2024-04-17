namespace Domain.Model;

public interface IHolidays
{
    IHoliday addHoliday(long holidayId, long colaboratorId);
    // public List<IHoliday> GetListHolidayMoreDays(int numberOfDays);
    List<IHoliday> getListHolidayFilterByColaborator(long colaboratorId, DateOnly startDate, DateOnly endDate);
    int GetNumberOfHolidaysDaysForColaboratorDuringPeriod(long colaboratorId, DateOnly startDate, DateOnly endDate);

    int GetNumberOfDaysByColaborator(long colaboratorId);
}