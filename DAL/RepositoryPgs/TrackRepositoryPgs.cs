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
    public class TrackRepositoryPgs : IRepository<Track>
    {
        private ProjectSystemContext db;

        public TrackRepositoryPgs(ProjectSystemContext psc)
        {
            this.db = psc;
        }

        public List<Track> GetList()
        {
            return db.Tracks.ToList();
        }

        public Track GetItem(int id)
        {
            return db.Tracks.Find(id);
        }

        public void Create(Track track)
        {
            db.Tracks.Add(track);
        }

        public void Update(Track track)
        {
            db.Entry(track).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Track tk = db.Tracks.Find(id);
            if (tk != null)
            {
                db.Tracks.Remove(tk);
                Save();
            }
        }

        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
