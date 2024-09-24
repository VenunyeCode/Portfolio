namespace Portflio.Controllers;

[ApiController]
[Route("api/v1/portfolio/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public ProjectController(IProjectService projectService, ILoggerManager logger, IMapper mapper)
    {
        _projectService = projectService;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectByIdAsync(int id)
    {
        try
        {
            var project = await _projectService.GetProjectById(id);
            if (project.IsNull())
            {
                _logger.LogError($"Project with id {id} not found");
                return NotFound($"Project with id {id} not found");
            }
            var projectDto = _mapper.Map<GetProjectDto>(project);
            _logger.LogInfo($"Project with id {id} found");
            return Ok(projectDto);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllProjectsAsync()
    {
        try
        {
            var projects = await _projectService.GetAllProjects();
            var projectsDto = _mapper.Map<IEnumerable<GetProjectDto>>(projects);

            _logger.LogInfo($"Projects list : Operation done successfully");
            return Ok(projectsDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost("add")]
    public async Task<IActionResult> Post([FromBody] AddProjectDto dto)
    {
        try
        {
            var project = _mapper.Map<Project>(dto);
            var projectCreated = await _projectService.AddProject(project);
            var projectDto = _mapper.Map<GetProjectDto>(projectCreated);
            _logger.LogInfo($"Project created with id {projectCreated.Id} successfully");
            return Ok(projectDto);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }
    
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] AddProjectDto dto)
    {
        try
        {
            var projectToBeUpdated = await _projectService.GetProjectById(id);
            if (projectToBeUpdated.IsNull())
            {
                _logger.LogError($"Project with id {id} not found");
                return NotFound($"Project with id {id} not found");
            }
            var project = _mapper.Map<Project>(dto);
            await _projectService.UpdateProject(projectToBeUpdated, project);
            _logger.LogInfo($"Project with id {id} updated successfully");
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var project = await _projectService.GetProjectById(id);
            if (project.IsNull())
            {
                _logger.LogError($"Project with id {id} not found");
                return NotFound($"Project with id {id} not found");
            } 
            await _projectService.DeleteProject(project);
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }
   
}