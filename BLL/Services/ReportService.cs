﻿using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ReportService : IReportService
    {

        private IDbRepos db;
        public ReportService(IDbRepos db)
        {
            this.db = db;
        }

        public List<ReportStatisticByAllPersonDTO> GetStatisticByAllPerson(DateTime startDate, DateTime endDate)
        {
            var result = db.Reports.MakeDiagnosisReport(startDate, endDate);
            return result.Select(i => new ReportStatisticByAllPersonDTO(i)).ToList();
        }

        public List<ReportTasksForPersonDTO> GetTasksForPerson(string Person)
        {
            throw new NotImplementedException();
        }
    }
}
