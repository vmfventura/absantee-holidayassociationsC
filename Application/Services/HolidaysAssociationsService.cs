namespace Application.Services;

using Domain.Model;
using Application.DTO;

using Microsoft.EntityFrameworkCore;
using DataModel.Repository;
using Domain.IRepository;
using Domain.Factory;
using DataModel.Model;
using Gateway;

public class HolidaysAssociationsService
{
    private readonly IAssociationRepository _associationRepository;
    private readonly IHolidayRepository _holidayRepository;

    public HolidaysAssociationsService(IAssociationRepository associationRepository, IHolidayRepository holidayRepository)
    {
        _associationRepository = associationRepository;
        _holidayRepository = holidayRepository;
    }
    public async Task<object?> GetByProjectColab(long projectId, long colabId)
    {
        var associationList = await _associationRepository.GetAssociationsByColabIdAndProjectIdAsync(projectId, colabId);

        var holidayList = await _holidayRepository.GetHolidaysByColab(colabId);

        int daysOfColaborator = HolidaysAssociation.GetTest(associationList, holidayList);
        
        return daysOfColaborator;
    }

    public async Task<object?> GetByProject(long projectId)
    {
        var associationsList = await _associationRepository.GetAssociationsByProject(projectId);
        return associationsList;
    }
    
    public async Task<object?> GetByColabortor(long colabId)
    {
        var associationsList = await _associationRepository.GetAssociationsByColaborator(colabId);
        return associationsList;
    }
    
    public async Task<object?> GetAssociationsByDates(DateOnly startDate, DateOnly endDate)
    {
        var associationsList = await _associationRepository.GetAssociationsByDates(startDate, endDate);
        
        return associationsList;
    }

    public async Task<object> GetDaysHolidaysByColaboratorInRangePeriod(long projectId, long colabId, DateOnly startDate, DateOnly endDate)
    {
        var associationList = await _associationRepository.GetAssociationsByColabIdAndProjectIdDateRangeAsync(projectId, colabId, startDate, endDate);

        if (associationList.Count() > 0)
        {
            DateOnly startDateFiltered = (associationList.Min(a => a.StartDate) < startDate) ? startDate : associationList.Min(a => a.StartDate);
            DateOnly endDateFiltered = (associationList.Max(a => a.EndDate) > endDate) ? endDate : associationList.Max(a => a.EndDate);
            var holidayList = await _holidayRepository.GetHolidaysByColabWithDates(colabId, startDateFiltered, endDateFiltered);
            
            int numberOfDays = HolidaysAssociation.GetDaysHolidaysByColaboratorInRangePeriod(associationList, colabId, holidayList, startDateFiltered, endDateFiltered);
            return numberOfDays;
        }
        return 0;
    }
}