namespace Application.Services;

using Domain.Model;
using Application.DTO;

using Microsoft.EntityFrameworkCore;
using DataModel.Repository;
using Domain.IRepository;
using Domain.Factory;
using DataModel.Model;
using Gateway;

public class HolidayAssociationsService
{
    
    public async Task<object?> GetByProjectColab(long projectId, long colabIid)
    {
        throw new NotImplementedException();
    }

    public async Task<object?> GetByProject(long projectId)
    {
        throw new NotImplementedException();
    }
}