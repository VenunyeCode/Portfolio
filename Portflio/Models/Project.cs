namespace Portflio.Models;

public class Project
{
    [Key]
    public int Id { get; set; }
    [Required]
    public String Name { get; set; } = String.Empty;
    [Required]
    public String Description { get; set; } = String.Empty;
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [NotMapped]
    public List<int>? Technologies { get; set; } // here we just stock id of each techno that has been used 
    [ForeignKey(nameof(Type))]
    public int ProjectTypeId { get; set; }
    [NotMapped]
    public ProjectType? ProjectType { get; set; }
    [ForeignKey(nameof(Platform))]
    public int PlatformId { get; set; }
    [NotMapped]
    public Platform PlatformUsed { get; set; }
}