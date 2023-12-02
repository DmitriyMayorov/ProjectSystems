using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IProjectService
    {
        List<ProjectDTO> GetProjects();

        ProjectDTO GetProject(int id);

        void CreateProject(ProjectDTO project);
        void UpdateProject(ProjectDTO project);
        void DeleteProject(int id);
    }
}
