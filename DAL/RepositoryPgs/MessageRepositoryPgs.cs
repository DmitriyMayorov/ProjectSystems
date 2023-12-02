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
    public class MessageRepositoryPgs : IRepository<Message>
    {
        private ProjectSystemContext db;

        public MessageRepositoryPgs(ProjectSystemContext psc)
        {
            this.db = psc;
        }

        public List<Message> GetList()
        {
            return db.Messages.ToList();
        }

        public Message GetItem(int id)
        {
            return db.Messages.Find(id);
        }

        public void Create(Message message)
        {
            db.Messages.Add(message);
        }

        public void Update(Message message)
        {
            db.Entry(message).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Message ms = db.Messages.Find(id);
            if (ms != null)
            {
                db.Messages.Remove(ms);
                Save();
            }
        }

        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
