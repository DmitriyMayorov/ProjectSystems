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

        public string Person {  get; set; }

        public long CountHours { get; set; }

        public string Position { get; set; }

        public ReportStatisticByAllPersonDTO() { }

        public ReportStatisticByAllPersonDTO(ReportStatisticByAllPerson rep)
        {
            ID = rep.ID;
            Person = rep.Person;
            CountHours = rep.CountHours;
            Position = rep.Position;
        }

    }
}
