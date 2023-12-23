using DAL.ReportForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class ReportCompletedTaskForWorkersDTO
    {
        public int Id;

        public string Person;

        public long CompletedTasks;

        public ReportCompletedTaskForWorkersDTO(ReportCountCompletedTasks ct)
        {
            Id = ct.ID;
            Person = ct.Person;
            CompletedTasks = ct.CountCompletedTasks;
        }
    }
}
