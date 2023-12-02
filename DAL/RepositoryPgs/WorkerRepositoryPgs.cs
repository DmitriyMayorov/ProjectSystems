using DAL.EF;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DAL.RepositoryPgs
{
    public class WorkerRepositoryPgs : IRepository<Worker>
    {
        private ProjectSystemContext db;

        public WorkerRepositoryPgs(ProjectSystemContext psc)
        {
            this.db = psc;
        }

        public List<Worker> GetList()
        {
            return db.Workers.ToList();
        }

        public Worker GetItem(int id)
        {
            return db.Workers.Find(id);
        }

        public void Create(Worker worker)
        {
            db.Workers.Add(worker);
        }

        public void Update(Worker worker)
        {
            db.Entry(worker).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Worker wk = db.Workers.Find(id);
            if (wk != null)
            {
                db.Workers.Remove(wk);
                Save();
            }
        }

        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
