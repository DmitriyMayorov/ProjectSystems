using BLL.DTO;
using BLL.Interfaces;
using DAL.EF;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class MessageService : IMessageService
    {
        private IDbRepos db;
        public MessageService(IDbRepos db)
        {
            this.db = db;
        }

        public bool SaveChanges()
        {
            return (db.Save() > 0) ? true : false;
        }

        public List<MessageDTO> GetMessages()
        {
            return db.Messages.GetList().Select(i => new MessageDTO(i)).ToList();
        }

        public MessageDTO GetMessage(int id)
        {
            return new MessageDTO(db.Messages.GetItem(id));
        }

        public void CreateMessage(MessageDTO message)
        {
            db.Messages.Create(new Message() { /*Id = message.Id,*/ TextMessage = message.TextMessage, 
                                               DateMessage = message.DateMessage, IDTask = message.IDTask, 
                                               IDWorker = message.IDWorker});
            SaveChanges();
        }

        public void UpdateMessage(MessageDTO message)
        {
            Message mess = db.Messages.GetItem(message.Id);
            mess.TextMessage = message.TextMessage;
            mess.DateMessage = message.DateMessage;
            mess.IDTask = message.IDTask;
            mess.IDWorker = message.IDWorker;
            SaveChanges();
        }

        public void DeleteMessage(int id)
        {
            db.Messages.Delete(id);
        }

        public List<MessageDTO> GetMessagesForCurrentTask(int taskId)
        {
            return db.Messages.GetList().Where(i => i.IDTask == taskId).Select(i => new MessageDTO(i)).ToList();
        }
    }
}
