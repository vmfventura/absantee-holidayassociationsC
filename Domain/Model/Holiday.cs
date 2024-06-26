namespace Domain.Model;

using Domain.Factory;

public class Holiday : IHoliday
{

	public long Id {get; set;}
	private long colaboratorId{get; set;}

	private List<HolidayPeriod> _holidayPeriods = new List<HolidayPeriod>();


	public Holiday(long ColabId)
	{
		if (ColabId != null)
		{
			colaboratorId = ColabId;
		}
		else
			throw new ArgumentException("Invalid argument: colaboratorId must be non null");
	}

	public Holiday(long id, long ColabId)
	{
		if (ColabId != null)
		{
			colaboratorId = ColabId;
			Id = id;
		}
		else
			throw new ArgumentException("Invalid argument: colaboratorId must be non null");
	}

	public HolidayPeriod AddHolidayPeriod(IHolidayPeriodFactory hpFactory, DateOnly startDate, DateOnly endDate)
	{
		HolidayPeriod holidayPeriod = hpFactory.NewHolidayPeriod(startDate, endDate);
		_holidayPeriods.Add(holidayPeriod);
		return holidayPeriod;
	}

	public long GetColaborator()
	{
		return colaboratorId;
	}

	public List<HolidayPeriod> GetHolidayPeriodsDuring(DateOnly startDate, DateOnly endDate)
	{
		return _holidayPeriods.Where(hp => hp.EndDate > startDate && hp.StartDate < endDate)
								.ToList();
	}

	public List<HolidayPeriod> GetHolidayPeriods()
	{
		return _holidayPeriods;
	}

	public bool HasColaboratorAndHolidayPeriodsDuring(long colabId, DateOnly startDate, DateOnly endDate)
	{
			return colaboratorId == colabId && GetHolidayPeriodsDuring(startDate, endDate).Any();
	}

	

	public int GetHolidaysDaysWithMoreThanXDaysOff(int intDaysOff)
	{
		int numberOfDays = 0;

		foreach (HolidayPeriod hp in _holidayPeriods)
		{
			numberOfDays += hp.GetNumberOfDays();
		}
		if (numberOfDays > intDaysOff)
		{
			return numberOfDays;
		}
		else
		{
			return 0;
		}
	}

	public int GetNumberOfHolidayPeriodsDays()
	{
		return _holidayPeriods.Sum(hp => hp.GetNumberOfDays());
	}
	public int GetNumberOfHolidayPeriodsDays(DateOnly startDate, DateOnly endDate)
	{
		return _holidayPeriods.Sum(hp => hp.GetNumberOfDays(startDate, endDate));
	}

	public bool HasColaborador(long colabId)
	{
		return colaboratorId == colabId;
	}
}