using BLL.DTO;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ReportService : IReportService
    {
        public List<ReportStatisticByAllPersonDTO> GetStatisticByAllPerson(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public List<ReportTasksForPersonDTO> GetTasksForPerson(string Person)
        {
            throw new NotImplementedException();
        }
    }
}
