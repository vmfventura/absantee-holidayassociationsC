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
        throw new NotImplementedException();
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
}