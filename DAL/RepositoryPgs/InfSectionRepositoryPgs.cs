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
    public class InfSectionRepositoryPgs : IRepository<InfSection>
    {
        private ProjectSystemContext db;

        public InfSectionRepositoryPgs(ProjectSystemContext psc)
        {
            this.db = psc;
        }

        public List<InfSection> GetList()
        {
            return db.InfSections.ToList();
        }

        public InfSection GetItem(int id)
        {
            return db.InfSections.Find(id);
        }

        public void Create(InfSection infSection)
        {
            db.InfSections.Add(infSection);
        }

        public void Update(InfSection infSection)
        {
            db.Entry(infSection).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            InfSection inf = db.InfSections.Find(id);
            if (inf != null)
            {
                db.InfSections.Remove(inf);
                Save();
            }
        }

        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
