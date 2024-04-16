using Newtonsoft.Json;

namespace Application.DTO;

public class HolidayGatewayDTO
{
    public long Id  {get; set;}
    public long _colabId {get; set;}

    public HolidayGatewayDTO()
    { }
        
    public HolidayGatewayDTO(long id, long colabId)
    {
        Id = id;
        _colabId = colabId;
    }

    public static string Serialize(HolidayDTO holidayDTO)
    {
        HolidayGatewayDTO projectGateway = new HolidayGatewayDTO(holidayDTO.Id, holidayDTO._colabId);
        var jsonMessage = JsonConvert.SerializeObject(projectGateway);
        return jsonMessage;
    }

    public static HolidayGatewayDTO Deserialize(string holidayDTOString)
    {
        return JsonConvert.DeserializeObject<HolidayGatewayDTO>(holidayDTOString);
    }

    public static HolidayDTO ToDTO(string holidayDTOString)
    {
        HolidayGatewayDTO holidayGatewayDTO = Deserialize(holidayDTOString);
        HolidayDTO holidayDTO = new HolidayDTO(holidayGatewayDTO._colabId, holidayGatewayDTO.Id);
        return holidayDTO;
    }
}