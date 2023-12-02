using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ReportForms
{
    public class ReportTasksForPerson
    {
        public string Nametask { get; set; }

        public string NameDiscriptionTask { get; set; }

        public DateTime? DeadlineTask { get; set; }

        public ReportTasksForPerson() { }
    }
}
