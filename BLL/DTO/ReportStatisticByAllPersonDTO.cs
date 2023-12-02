using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.ReportForms;

namespace BLL.DTO
{
    public class ReportStatisticByAllPersonDTO
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public long CountHours { get; set; }

        public long CountCompletedTasks { get; set; }

        public string Position { get; set; }

        public ReportStatisticByAllPersonDTO() { }

        public ReportStatisticByAllPersonDTO(ReportStatisticByAllPerson rep)
        {
            ID = rep.ID;
            Name = rep.Name;
            Surname = rep.Surname;
            CountHours = rep.CountHours;
            CountCompletedTasks = rep.CountCompletedTasks;
            Position = rep.Position;
        }

    }
}
