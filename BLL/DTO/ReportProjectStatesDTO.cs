using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.ReportForms;

namespace BLL.DTO
{
    public class ReportProjectStatesDTO
    {
        public string NameState;

        public int CountTasks;

        public ReportProjectStatesDTO() { }

        public ReportProjectStatesDTO(ReportProjectStates repproject) 
        {
            NameState = repproject.NameState;
            CountTasks = repproject.CountTasks;
        }
    }
}
