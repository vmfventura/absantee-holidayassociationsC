using Domain.Factory;

namespace Domain.Model;

public class HolidaysAssociation
{
    private IAssociation _associationsObj;
    private IEnumerable<IHoliday> _holidays;
    private IHoliday _holidaysObj;
    private IEnumerable<IAssociation> _associations;
    
    protected HolidaysAssociation()
    {
    }

    public HolidaysAssociation(IAssociation associations, IHoliday holidays)
    {
        if (associations is not null && holidays is not null)
        {
            _associationsObj = associations;
            _holidaysObj = holidays;
        }
        else
        {
            throw new ArgumentException("Invalid argument: associationFactory must be non null");
        }
    }
    
    public HolidaysAssociation(IEnumerable<IAssociation> associations, IEnumerable<IHoliday> holidays)
    {
        if (associations is null || holidays is null)
        {
            throw new ArgumentException("Associations or holidays cannot be null");
        }

        this._associations = associations;
        this._holidays = holidays;
    }
    

    // public int GetHolidaysDaysColaboratorInProjectDuringPeriodOfTime(long colaboratorId, long projectId, DateOnly startDate, DateOnly endDate)
    // {
    //     bool projects = _associations.Where(p => p.IsColaboratorInProjectDuringPeriod(colaboratorId, projectId, startDate, endDate)).Any();
    //     if (projects)
    //     {
    //         return _holidaysObj.GetNumberOfHolidaysDaysForColaboratorDuringPeriod(colaboratorId, startDate, endDate);
    //     }
    //     return 0;
    // }

    public static int GetTest(IEnumerable<Association> associations, IEnumerable<Holiday> holidays)
    {
        HolidaysAssociation holidaysAssociation = new HolidaysAssociation(associations, holidays);
        
        return 23;
    }
    public static IEnumerable<IHoliday> getListHolidayFilterByColaborator(long colaboratorId, IEnumerable<Holiday> holidayList, DateOnly startDate, DateOnly endDate)
    {
        IEnumerable<IHoliday> holidayListFiltered =
            holidayList.Where(h => h.HasColaboratorAndHolidayPeriodsDuring(colaboratorId, startDate, endDate));

        if (!holidayList.Any())
        {
            throw new ArgumentException("No holiday found for this colaborator");
        }

        return holidayListFiltered;
    }

    public static int GetDaysHolidaysByColaboratorInRangePeriod(IEnumerable<Association> associationList, long colabId, IEnumerable<Holiday> holidayList, DateOnly startDate, DateOnly endDate)
    {
        if (associationList is not null && holidayList is not null)
        {
            
            IEnumerable<IHoliday> holidayListFiltered = getListHolidayFilterByColaborator(colabId, holidayList, startDate, endDate);
            int totalDaysOff = holidayList
                .Sum(holiday => holiday.GetNumberOfHolidayPeriodsDays());

            return totalDaysOff;
            // return _holidaysObj.HasColaboratorAndHolidayPeriodsDuring()
        }
        return 0;
        
        // throw new NotImplementedException();
    }
}