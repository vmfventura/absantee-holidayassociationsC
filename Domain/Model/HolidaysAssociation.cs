using Domain.Factory;

namespace Domain.Model;

public class HolidaysAssociation
{
    private IAssociation _associationsObj;
    private List<IHolidays> _holidays = new List<IHolidays>();
    private IHolidays _holidaysObj;
    private List<IAssociations> _associations = new List<IAssociations>();
    
    protected HolidaysAssociation()
    {
    }

    public HolidaysAssociation(IAssociation associations, IHolidays holidays)
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
    
    public HolidaysAssociation(List<IAssociations> associations, List<IHolidays> holidays)
    {
        if (associations is null || holidays is null)
        {
            throw new ArgumentException("Associations or holidays cannot be null");
        }

        this._associations = associations;
        this._holidays = holidays;
    }
    
    public int GetHolidaysDaysColaboratorInProjectDuringPeriodOfTime(long colaboratorId, long projectId, DateOnly startDate, DateOnly endDate)
    {
        bool projects = _associations.Where(p => p.IsColaboratorInProjectDuringPeriod(colaboratorId, projectId, startDate, endDate)).Any();
        if (projects)
        {
            return _holidaysObj.GetNumberOfHolidaysDaysForColaboratorDuringPeriod(colaboratorId, startDate, endDate);
        }
        return 0;
    }
}