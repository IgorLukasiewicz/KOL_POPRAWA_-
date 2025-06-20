using Microsoft.AspNetCore.Mvc;
using WebApplication1.Exceptions;
using WebApplication1.Services;
namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;
    

    public ProjectsController(IProjectService dbService)
    {
        _projectService = dbService;
    }

    [HttpGet("{projectId}")]
    public async Task<IActionResult> GetProjectInfo(int projectId)
    {
        try
        {
            var project = await _projectService.GetProjectInfo(projectId);
            return Ok(project);
        }
        catch (NotFoundException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}