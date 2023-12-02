using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMessageService
    {
        List<MessageDTO> GetMessages();

        MessageDTO GetMessage(int id);

        void CreateMessage(MessageDTO message);
        void UpdateMessage(MessageDTO message);
        void DeleteMessage(int id);
    }
}
