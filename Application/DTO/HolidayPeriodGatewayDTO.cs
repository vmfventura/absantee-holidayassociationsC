namespace Application.DTO;

public class HolidayPeriodGatewayDTO
{
    public long Id { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}