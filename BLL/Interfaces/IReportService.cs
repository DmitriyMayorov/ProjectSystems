using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IReportService
    {
        List<ReportCompletedTaskForWorkersDTO> GetReportCompletedTasksForWorkers();
        List<ReportStatisticByAllPersonDTO> GetStatisticByAllPerson(DateTime startDate, DateTime endDate);
        List<ReportProjectStatesDTO> MakeCountTasksForCurrentProjectByStates(ProjectDTO project);
    }
}
