using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.DTO;

namespace WebApi.Controllers;

[ApiController]
[Route("api/holidayassociations")]
public class HolidayAssociationsController : Controller
{
    private readonly HolidaysAssociationsService _holidaysAssociationsService;

    List<string> _errorMessages = new List<string>();

    public HolidayAssociationsController(HolidaysAssociationsService holidaysAssociationsService)
    {
        _holidaysAssociationsService = holidaysAssociationsService;
    }
    
    [HttpGet("project/{projectId}/colab/{colabId}")]
    public async Task<ActionResult<object?>> GetProjectById(long projectId, long colabId)
    {

        var holidayDTO = await _holidaysAssociationsService.GetByProjectColab(projectId, colabId);

        if (holidayDTO is not null)
        {
            return Ok(holidayDTO);
        }

        return NotFound();
    }
    [HttpGet("project/{projectId}")]
    public async Task<ActionResult<int>> GetProjectById(long projectId)
    {
        // var projectDTO = await _holidaysAssociationsService.GetByProject(projectId);
        // if (projectDTO is not null)
        // {
        //     return Ok(projectDTO);
        // }
    
        return NotFound();
    }
}