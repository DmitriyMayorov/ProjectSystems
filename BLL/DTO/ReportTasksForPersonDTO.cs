using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.ReportForms;

namespace BLL.DTO
{
    public class ReportTasksForPersonDTO
    {
        public string Nametask { get; set; }

        public string NameDiscriptionTask { get; set; }

        public DateTime? DeadlineTask { get; set; }

        public ReportTasksForPersonDTO() { }

        public ReportTasksForPersonDTO(ReportTasksForPerson rep)
        {
            Nametask = rep.Nametask;
            NameDiscriptionTask = rep.NameDiscriptionTask;
            DeadlineTask = rep.DeadlineTask;
        }
    }
}
