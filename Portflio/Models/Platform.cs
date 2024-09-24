namespace Portflio.Models;

public class Platform
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Please enter platform name")]
    public String PlatFormLabel { get; set; } = String.Empty;
    
    public List<Project> Projects { get; set; } = new List<Project>(); 
}