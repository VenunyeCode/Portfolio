namespace Portflio.Models;

public class ProjectType
{
    public int Id { get; set; }
    public String LabelType { get; set; } = String.Empty;

    public List<Project> Projects { get; set; } = new List<Project>(); 
}