namespace Domain.Model;

public class HolidayPeriod
{
    public long Id { get; set; }
    private DateOnly _startDate;
    private DateOnly _endDate;

    public DateOnly StartDate
    {
        get { return _startDate; }
    }

    public DateOnly EndDate
    {
        get { return _endDate; }
    }

    // EF Core requires a parameterless constructor for mapping purposes.
    private HolidayPeriod() { }

    // This constructor can be used by your code when creating new instances.
    public HolidayPeriod(DateOnly startDate, DateOnly endDate)
    {
        if (startDate >= endDate)
        {
            throw new ArgumentException("invalid arguments: start date >= end date.");
        }

        this._startDate = startDate;
        this._endDate = endDate;
    }



    public bool IsStartDateIsValid(DateOnly startDate, DateOnly endDate)
    {
        if( startDate >= endDate ) 
        {
            return false;
        }
        return true;
    }

    public int GetNumberOfDays()
    {
        int startDateDays = _startDate.DayNumber;
        int endDateDays = _endDate.DayNumber;
        return endDateDays - startDateDays;
    }
    
    public int GetNumberOfDays(DateOnly startDate, DateOnly endDate)
    {
        DateOnly startDateDay = (_startDate < startDate) ? startDate : _startDate;
        DateOnly endDateDay = (_endDate > endDate) ? endDate : _endDate;
        int startDateDays = startDateDay.DayNumber;
        int endDateDays = endDateDay.DayNumber;
        int discountDays = GetNumberOfDaysInWeekends(startDateDay, endDateDay.AddDays(1));
        return ((endDateDays - startDateDays)+1)-discountDays;
    }

    public int GetNumberOfDaysInWeekends(DateOnly startDate, DateOnly endDate)
    {
        int days = 0;
        for (DateOnly i = startDate; i < endDate; i = i.AddDays(1))
        {
            if (i.DayOfWeek == DayOfWeek.Saturday || i.DayOfWeek == DayOfWeek.Sunday)
            {
                days++;
            }
        }
        return days;
    }
}