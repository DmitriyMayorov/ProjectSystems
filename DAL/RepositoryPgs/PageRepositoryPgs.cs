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
    public class PageRepositoryPgs : IRepository<Page>
    {
        private ProjectSystemContext db;

        public PageRepositoryPgs(ProjectSystemContext psc)
        {
            this.db = psc;
        }

        public List<Page> GetList()
        {
            return db.Pages.ToList();
        }

        public Page GetItem(int id)
        {
            return db.Pages.Find(id);
        }

        public void Create(Page page)
        {
            db.Pages.Add(page);
        }

        public void Update(Page page)
        {
            db.Entry(page).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Page pg = db.Pages.Find(id);
            if (pg != null)
            {
                db.Pages.Remove(pg);
                Save();
            }
        }

        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
