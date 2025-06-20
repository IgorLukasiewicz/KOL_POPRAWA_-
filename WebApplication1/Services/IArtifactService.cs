using WebApplication1.DTOs;

namespace WebApplication1.Services;

public interface IArtifactService
{
    public Task<int> NewArtifactAndProject(NewArtifactAndProjectDTO input);
}