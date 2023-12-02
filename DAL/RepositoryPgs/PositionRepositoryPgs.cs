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
    public class PositionRepositoryPgs : IRepository<Position>
    {
        private ProjectSystemContext db;

        public PositionRepositoryPgs(ProjectSystemContext psc)
        {
            this.db = psc;
        }

        public List<Position> GetList()
        {
            return db.Positions.ToList();
        }

        public Position GetItem(int id)
        {
            return db.Positions.Find(id);
        }

        public void Create(Position position)
        {
            db.Positions.Add(position);
        }

        public void Update(Position position)
        {
            db.Entry(position).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Position pos = db.Positions.Find(id);
            if (pos != null)
            {
                db.Positions.Remove(pos);
                Save();
            }
        }

        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
