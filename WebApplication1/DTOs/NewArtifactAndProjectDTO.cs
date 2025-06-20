using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class NewArtifactAndProjectDTO
{
    [Required]
    public AddArtifactDTO Artifact { get; set; }

    [Required]
    public AddProjectDTO Project { get; set; }
}

public class AddArtifactDTO
{
    [Required]
    public int ArtifactId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public DateTime OrginalDate { get; set; }

    [Required(ErrorMessage = "InstitutionId jest wymagane")]
    public int InstitutionId { get; set; }
}

public class AddProjectDTO
{
    [Required]
    public int ProjectId { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Cel projektu musi mieć od 3 do 255 znaków")]
    public string Objective { get; set; }

    [Required]
    public DateTime StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
}