using DAL.EF;
using DAL.Interfaces;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.RepositoryPgs
{
    public class TaskRepositoryPgs : IRepository<DAL.EF.Task>
    {
        private ProjectSystemContext db;

        public TaskRepositoryPgs(ProjectSystemContext psc)
        {
            this.db = psc;
        }

        public List<DAL.EF.Task> GetList()
        {
            return db.Tasks.ToList();
        }

        public DAL.EF.Task GetItem(int id)
        {
            return db.Tasks.Find(id);
        }

        public void Create(DAL.EF.Task task)
        {
            db.Tasks.Add(task);
        }

        public void Update(DAL.EF.Task task)
        {
            db.Entry(task).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            DAL.EF.Task tk = db.Tasks.Find(id);
            if (tk != null)
            {
                db.Tasks.Remove(tk);
                Save();
            }
        }

        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
