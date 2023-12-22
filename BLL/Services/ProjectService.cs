using BLL.DTO;
using BLL.Interfaces;
using DAL.EF;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProjectService : IProjectService
    {
        private IDbRepos db;
        public ProjectService(IDbRepos db)
        {
            this.db = db;
        }

        public bool SaveChanges()
        {
            return (db.Save() > 0) ? true : false;
        }

        public List<ProjectDTO> GetProjects()
        {
            return db.Projects.GetList().Select(i => new ProjectDTO(i)).ToList();
        }

        public ProjectDTO GetProject(int id)
        {
            return new ProjectDTO(db.Projects.GetItem(id));
        }

        public void CreateProject(ProjectDTO project)
        {
            db.Projects.Create(new Project() { Name = project.Name, DeadLine = project.DeadLine });
            SaveChanges();
        }

        public void UpdateProject(ProjectDTO project)
        {
            Project proj = db.Projects.GetItem(project.Id);
            proj.Name = project.Name;
            proj.DeadLine = project.DeadLine;
            SaveChanges();
        }

        public void DeleteProject(int id)
        {
            db.Projects.Delete(id);
        }
    }
}
