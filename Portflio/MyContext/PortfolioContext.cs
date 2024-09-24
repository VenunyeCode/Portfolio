namespace Portflio.MyContext;

public class PortfolioContext : DbContext
{
    public PortfolioContext(DbContextOptions<PortfolioContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Technology>().HasData(
            new Technology()
            {
                Id = 1,
                TechnoLabel = "HTML/CSS/JS"
            },
            new Technology()
            {
                Id = 2,
                TechnoLabel = "JAVA"
            },
            new Technology()
            {
                Id = 3,
                TechnoLabel = "CSharp"
            },
            new Technology()
            {
                Id = 4,
                TechnoLabel = ".NET"
            },
            new Technology()
            {
                Id = 5,
                TechnoLabel = "Bootstrap"
            },
            new Technology()
            {
                Id = 6,
                TechnoLabel = "React.JS"
            },
            new Technology()
            {
                Id = 7,
                TechnoLabel = "Angular"
            },
            new Technology()
            {
                Id = 8,
                TechnoLabel = "SQL Server"
            },
            new Technology()
            {
                Id = 9,
                TechnoLabel = "MySQL"
            },
            new Technology()
            {
                Id = 10,
                TechnoLabel = "PostgreSQL"
            }
        );

        modelBuilder
            .Entity<ProjectType>()
            .HasData(
                new ProjectType()
                {
                    Id = 1,
                    LabelType = "Mobile"
                },
                new ProjectType()
                {
                    Id = 2,
                    LabelType = "Web"
                }
                
            );
    }
    
    public DbSet<Technology> Technologies { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<ProjectType> ProjectTypes { get; set; }
}