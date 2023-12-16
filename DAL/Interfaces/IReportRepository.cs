using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.ReportForms;

namespace DAL.Interfaces
{
    public interface IReportRepository
    {
        List<ReportTasksForPerson> MakeTasksForPerson(int IDPerson);
        List<ReportStatisticByAllPerson> MakeDiagnosisReport(DateTime firstDate, DateTime secondTime);
        List<ReportProjectStates> MakeCountTasksForCurrentProjectByStates(int IDProject);
    }
}
