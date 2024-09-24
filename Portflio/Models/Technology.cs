namespace Portflio.Models;

public class Technology
{
    [Key]
    public int Id { get; set; }
    [Required]
    public String TechnoLabel { get; set; } = String.Empty;
    
    public List<Project> Projects { get; set; } = new List<Project>(); 
}