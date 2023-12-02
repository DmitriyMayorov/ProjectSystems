using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;

namespace BLL.DTO
{
    public class MessageDTO
    {
        public int Id { get; set; }

        public string TextMessage { get; set; }

        public DateTime DateMessage { get; set; }

        public int IDTask { get; set; }

        public int IDWorker { get; set; }

        public MessageDTO() { }

        public MessageDTO(Message mess)
        {
            Id = mess.Id;
            TextMessage = mess.TextMessage;
            DateMessage = mess.DateMessage;
            IDWorker = mess.IDWorker;
            IDTask = mess.IDTask;
        }
    }
}
