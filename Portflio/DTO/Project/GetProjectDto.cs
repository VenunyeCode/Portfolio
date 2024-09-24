namespace Portflio.DTO.Project;

public class GetProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int ProjectTypeId { get; set; }
}