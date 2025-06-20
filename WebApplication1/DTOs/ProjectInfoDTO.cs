namespace WebApplication1.DTOs;

public class ProjectInfoDTO
{
    public int ProjectId { get; set; }
    public string Objective { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ArtifactInfoDto Artifact { get; set; }
    public List<StaffInfoDTO> StaffAssignments { get; set; }
}

public class ArtifactInfoDto
{
    public string Name { get; set; }
    public DateTime OrginDate { get; set; }
    public InstitutionInfoDTO Institution { get; set; }
    
}

public class InstitutionInfoDTO
{
    public int InstitutionId { get; set; }
    public string Name { get; set; }
    public int FoundedYear { get; set; }

}

public class StaffInfoDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime HireDate { get; set; }
    public string Role { get; set; }
}