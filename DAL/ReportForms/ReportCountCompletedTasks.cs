using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ReportForms
{
    public class ReportCountCompletedTasks
    {
        public int ID { get; set; }

        public string Person { get; set; }

        public long CountCompletedTasks { get; set; }

        public ReportCountCompletedTasks() { }
    }
}
