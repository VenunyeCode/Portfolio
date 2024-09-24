using Portflio.DTO.Platform;
using Portflio.DTO.ProjectType;
using Portflio.DTO.Technology;

namespace Portflio.Profile;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        #region Project
            CreateMap<Project, AddProjectDto>();
            CreateMap<AddProjectDto, Project>();
            CreateMap<GetProjectDto, Project>();
            CreateMap<Project, GetProjectDto>();
        #endregion
        
        #region Platform
        CreateMap<Platform, AddPlatformDto>();
        CreateMap<AddPlatformDto, Platform>();
        CreateMap<GetPlatformDto, Platform>();
        CreateMap<Platform, GetPlatformDto>();
        #endregion
        
        #region Technology
        CreateMap<Technology, AddTechnologyDto>();
        CreateMap<AddTechnologyDto, Technology>();
        CreateMap<GetTechnologyDto, Technology>();
        CreateMap<Technology, GetTechnologyDto>();
        #endregion
        
        #region ProjectType
        CreateMap<ProjectType, AddProjectTypeDto>();
        CreateMap<AddProjectTypeDto, ProjectType>();
        CreateMap<GetProjectTypeDto, ProjectType>();
        CreateMap<ProjectType, GetProjectTypeDto>();
        #endregion
        
    }
}