using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Interfaces;

namespace DAL.RepositoryPgs
{
    public class ProjectRepositoryPgs : IRepository<Project>
    {
        private ProjectSystemContext db;

        public ProjectRepositoryPgs(ProjectSystemContext psc)
        {
            this.db = psc;
        }

        public List<Project> GetList()
        {
            return db.Projects.ToList();
        }

        public Project GetItem(int id)
        {
            return db.Projects.Find(id);
        }

        public void Create(Project project)
        {
            db.Projects.Add(project);
        }

        public void Update(Project project)
        {
            db.Entry(project).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Project pr = db.Projects.Find(id);
            if (pr != null)
            {
                db.Projects.Remove(pr);
                Save();
            }
        }

        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
