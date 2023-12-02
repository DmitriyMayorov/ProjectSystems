using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ReportForms
{
    public class ReportStatisticByAllPerson
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public long CountHours { get; set; }

        public long CountCompletedTasks { get; set; }

        public string Position { get; set; }
    }
}
