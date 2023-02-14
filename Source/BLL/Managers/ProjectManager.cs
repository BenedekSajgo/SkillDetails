using AutoMapper;
using BLL.Models;
using BLL.Models.Project;
using BLL.ValidatorInterfaces;
using DAL.Entities;
using DAL.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Managers
{
    public class ProjectManager
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly IProjectValidator _projectValidator;

        public ProjectManager(IProjectRepository projectRepository, IMapper mapper, IProjectValidator projectValidator)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _projectValidator = projectValidator;
        }

        public async Task<BaseResult<ProjectDto>> GetProjectAsync(int id, CancellationToken cancellation)
        {
            var projectEntity = await _projectRepository.GetProjectAsync(id, cancellation);
            if (projectEntity == null) 
                return new BaseResult<ProjectDto> { Errors = new List<string> { $"Project with Id {id} does not exist" } } ;

            var projectDto = _mapper.Map<ProjectDto>(projectEntity);
            return new BaseResult<ProjectDto> {  Data = projectDto };
        }

        public async Task<BaseResult<List<ProjectDto>>> GetProjectsAsync(CancellationToken cancellation)
        {
            var projectEntities = await _projectRepository.GetProjectsAsync(cancellation);

            var projectDtos = _mapper.Map<List<ProjectDto>>(projectEntities);
            return new BaseResult<List<ProjectDto>> { Data = projectDtos };
        }

        public async Task<BaseResult<List<ProjectDto>>> GetDeletedProjectsForPersonAsync(int personId, CancellationToken cancellation)
        {
            if (!await _projectRepository.PersonByIdExistsAsync(personId, cancellation))
                return new BaseResult<List<ProjectDto>> { Errors = new List<string> { $"Person with Id {personId} doesnt exist." } };

            var projectEntitites = await _projectRepository.GetDeletedProjectsForPersonAsync(personId, cancellation);

            var projectDtos = _mapper.Map<List<ProjectDto>>(projectEntitites);
            return new BaseResult<List<ProjectDto>> { Data = projectDtos };
        }

        public async Task<BaseResult<ProjectDto>> CreateProjectAsync(int personId, ProjectCreateAndUpdateDto projectCreateDto, CancellationToken cancellation)
        {
            if (!await _projectRepository.PersonByIdExistsAsync(personId, cancellation))
                return new BaseResult<ProjectDto> { Errors = new List<string> { $"Person with Id {personId} doesnt exist." } };

            var errors = _projectValidator.Validate(projectCreateDto);
            if (errors.Any())
                return new BaseResult<ProjectDto> { Errors = errors };

            var projectEntity = _mapper.Map<Project>(projectCreateDto);

            await _projectRepository.CreateProject(personId, projectEntity, cancellation);
            await _projectRepository.SaveChangesAsync(cancellation);

            var createdProject = _mapper.Map<ProjectDto>(projectEntity);
            return new BaseResult<ProjectDto> { Data = createdProject }; 
        }

        public async Task<BaseResult<ProjectDto>> UpdateProjectAsync(int id, ProjectCreateAndUpdateDto projectUpdateDto, CancellationToken cancellation)
        {
            var projectEntity = await _projectRepository.GetProjectAsync(id, cancellation);
            if (projectEntity == null)
                return new BaseResult<ProjectDto> { Errors = new List<string> { $"Project with Id {id} does not exist" } };

            var errors = _projectValidator.Validate(projectUpdateDto);
            if (errors.Any())
                return new BaseResult<ProjectDto> { Errors = errors };

            _mapper.Map(projectUpdateDto, projectEntity);
            await _projectRepository.SaveChangesAsync(cancellation);

            var updatedProject = _mapper.Map<ProjectDto>(projectEntity);
            return new BaseResult<ProjectDto> { Data = updatedProject }; 
        }

        // TODO checken of person weldegelijk dat project heeft?
        public async Task DeleteProjectAsync(int id, CancellationToken cancellation)
        {
            _projectRepository.DeleteProject(id);
            await _projectRepository.SaveChangesAsync(cancellation);
        }

        public async Task RetrieveProjectAsync(int id, CancellationToken cancellation)
        {
            _projectRepository.RetrieveProject(id);
            await _projectRepository.SaveChangesAsync(cancellation);
        }
    }
}
