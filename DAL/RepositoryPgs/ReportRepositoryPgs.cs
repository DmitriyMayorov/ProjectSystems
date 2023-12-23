using DAL.EF;
using DAL.Interfaces;
using DAL.ReportForms;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
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

        public List<ReportCountCompletedTasks> MakeCompletedTasks()
        {
            List<Worker> wokerks = db.Workers.ToList();
            List<ReportCountCompletedTasks> result = new List<ReportCountCompletedTasks>();
            foreach (var worker in wokerks)
            {
                ReportCountCompletedTasks task = new ReportCountCompletedTasks() {
                                                                                  ID = worker.Id,
                                                                                  Person = worker.Person,
                                                                                  CountCompletedTasks = db.Tasks.ToList().
                                                                                                        Where(i => (i.State == "Ready" &&
                                                                                                                    (i.IDWorkerAnalyst == worker.Id || i.IDWorkerTester == worker.Id ||
                                                                                                                    i.IDWorkerCoder == worker.Id || i.IDWorkerMentor == worker.Id))).Count() 
                                                                                 };
                result.Add(task);
            }
            return result;
        }

        public List<ReportStatisticByAllPerson> MakeCountTrackingHours(DateTime firstDate, DateTime secondTime)
        {
            //Заглушка
            NpgsqlParameter param1 = new NpgsqlParameter("fd", firstDate);
            NpgsqlParameter param2 = new NpgsqlParameter("ld", secondTime);
            var result = db.Database.SqlQuery<ReportStatisticByAllPerson>("select * from get_statistic_for_workers(:fd::date,:ld::date);", new object[] { param1, param2 }).ToList();

            return result;
        }

        public List<ReportProjectStates> MakeCountTasksForCurrentProjectByStates(int IDProject)
        {
            var States = new List<string>() { "Plan", "InProgress", "Review", "Stage", "Test", "Ready" };
            var res = new List<ReportProjectStates>();
            foreach (var state in States)
            {
                ReportProjectStates el = new ReportProjectStates() { CountTasks = db.Tasks.ToList().Where(i => i.State == state && i.IDProject == IDProject).Count(), NameState = state };
                res.Add(el);
            }
            return res;
        }
    }
}
