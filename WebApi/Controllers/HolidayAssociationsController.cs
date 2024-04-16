using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.DTO;

namespace WebApi.Controllers;

[ApiController]
[Route("api/holidayassociations")]
public class HolidayAssociationsController : Controller
{
    private readonly HolidayAssociationsService _holidayAssociationsService;

    List<string> _errorMessages = new List<string>();

    public HolidayAssociationsController(HolidayAssociationsService holidayAssociationsService)
    {
        _holidayAssociationsService = holidayAssociationsService;
    }
    
    [HttpGet("project/{projectId}/colab/{colabIid}")]
    public async Task<ActionResult<HolidayAssociationsDTO>> GetProjectById(long projectId, long colabIid)
    {
        var projectDTO = await _holidayAssociationsService.GetByProjectColab(projectId,colabIid);
        if (projectDTO is not null)
        {
            return Ok(projectDTO);
        }
    
        return NotFound();
    }
    [HttpGet("project/{projectId}")]
    public async Task<ActionResult<HolidayAssociationsDTO>> GetProjectById(long projectId)
    {
        var projectDTO = await _holidayAssociationsService.GetByProject(projectId);
        if (projectDTO is not null)
        {
            return Ok(projectDTO);
        }
    
        return NotFound();
    }
}