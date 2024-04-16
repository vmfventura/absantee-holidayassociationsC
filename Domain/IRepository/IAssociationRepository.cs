using Domain.Model;

namespace Domain.IRepository;

public interface IAssociationRepository : IGenericRepository<Association>
{
    Task<IEnumerable<Association>> GetAssociationsAsync();
    Task<Association> GetAssociationsByIdAsync(long id);
    Task<Association> Add(Association association);
    Task<Association> Update(Association association, List<string> errorMessages);
    Task<bool> AssociationExists(long id);
}