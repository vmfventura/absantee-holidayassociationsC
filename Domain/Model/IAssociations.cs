namespace Domain.Model;

public interface IAssociations
{
    bool IsColaboratorInProjectDuringPeriod(long colaboratorId, long projectId, DateOnly startDate, DateOnly endDate);
}