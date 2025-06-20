using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArtifactsController : ControllerBase
{
    private readonly IArtifactService _artifactService;

    public ArtifactsController(IArtifactService artifactService)
    {
        _artifactService = artifactService;
    }

    [HttpPost]
    public async Task<IActionResult> NewArtifactAndProject([FromBody] NewArtifactAndProjectDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var newId = await _artifactService.NewArtifactAndProject(dto);
            return CreatedAtAction(nameof(NewArtifactAndProject), new { id = newId }, new { id = newId });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}