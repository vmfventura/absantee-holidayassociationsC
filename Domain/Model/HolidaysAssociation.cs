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
}