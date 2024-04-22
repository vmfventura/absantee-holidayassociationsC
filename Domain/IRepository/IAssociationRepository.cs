using Domain.Model;

namespace Domain.IRepository;

public interface IAssociationRepository : IGenericRepository<Association>
{
    Task<IEnumerable<Association>> GetAssociationsAsync();
    Task<Association> GetAssociationsByIdAsync(long id);
    Task<IEnumerable<Association>> GetAssociationsByColabIdAndProjectIdAsync(long projectId, long colaboratorId);
    Task<IEnumerable<Association>> GetAssociationsByColaborator(long colaboratorId);
    Task<IEnumerable<Association>> GetAssociationsByDates(DateOnly startDate, DateOnly endDate);

    Task<IEnumerable<Association>> GetAssociationsByColabIdAndProjectIdDateRangeAsync(long projectId,
        long colaboratorId, DateOnly startDate, DateOnly endDate);
    Task<Association> Add(Association association);
    Task<Association> Update(Association association, List<string> errorMessages);
    Task<bool> AssociationExists(long id);
    Task<IEnumerable<Association>> GetAssociationsByProject(long projectId);
}