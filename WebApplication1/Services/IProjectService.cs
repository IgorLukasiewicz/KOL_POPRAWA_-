using WebApplication1.DTOs;

namespace WebApplication1.Services;

public interface IProjectService
{
    public Task<ProjectInfoDTO> GetProjectInfo(int projectId);
}