using DAL.EF;
using DAL.Interfaces;
using DAL.ReportForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.RepositoryPgs
{
    public class ReportRepositoryPgs : IReportRepository
    {
        private ProjectSystemContext db;

        public ReportRepositoryPgs(ProjectSystemContext psc)
        {
            this.db = psc;
        }

        public List<ReportTasksForPerson> MakeTasksForPerson(int IDPerson)
        {
            //Заглушка
            return new List<ReportTasksForPerson>();
        }

        public List<ReportStatisticByAllPerson> MakeDiagnosisReport(DateTime firstDate, DateTime secondTime)
        {
            //Заглушка
            return new List<ReportStatisticByAllPerson>();
        }
    }
}
